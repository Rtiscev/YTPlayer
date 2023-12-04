using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DownloadMusic.Models;
using NAudio.Utils;
using NAudio.Wave;
using CodeBeautify;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using static DownloadMusic.Utility;

namespace YTPlayer.ViewModels
{
	public partial class MainPageViewModel : ObservableObject
	{
		#region General elements
		enum CommandState
		{
			Play, Pause, Next, Previous, Stop, ChangeTime
		}
		struct QueueStruct
		{
			public int ID { get; set; }
			public string videoID { get; set; }
			public bool isDownloaded { get; set; }

			public QueueStruct()
			{
				isDownloaded = false;
			}
		}
		struct TaskDetails
		{
			public LinkedListNode<QueueStruct> node { get; set; }
			public int ID { get; set; }
		}
		public event EventHandler<StringArgs> WorkCompleted;
		public event EventHandler<StringArgs> SetUpFinished;

		string downloadLocation;
		TimeSpan globalTime;
		string ytdlpLocation;
		string ffmpegLocation;
		string rootPath;
		bool isPlaying;
		bool isFirstLaunch;
		bool isFinished;
		bool isTimeDraggin = false;
		bool isVolumeDragging = false;

		WasapiOut outputDevice;
		LinkedList<QueueStruct> playList;
		LinkedListNode<QueueStruct> currentNode;
		event EventHandler OnMusicFinished;
		CancellationTokenSource cts;
		CancellationTokenSource mainCts;
		SemaphoreSlim semaphore = new(3);
		Queue<TaskDetails> DownloadTasks;
		CommandState commandState;
		int _playingTime;
		TimeSpan sliderTimeCopy;
		#endregion

		#region MVVM elements
		[ObservableProperty]
		bool isTextActive;
		[ObservableProperty]
		bool isButtonActive;
		[ObservableProperty]
		string link;
		[ObservableProperty]
		string breakpointStart;
		[ObservableProperty]
		string breakpointEnd;
		[ObservableProperty]
		string desiredGlyph;
		[ObservableProperty]
		Color desiredColor;
		[ObservableProperty]
		ObservableCollection<Links> linksDetails;
		[ObservableProperty]
		int currentPosition;
		[ObservableProperty]
		int totalDuration;
		[ObservableProperty]
		int sliderTime;
		[ObservableProperty]
		int totalSongs;
		[ObservableProperty]
		string nowPlaying;
		[ObservableProperty]
		string currentImage;
		[ObservableProperty]
		string currentTime;
		[ObservableProperty]
		int volume;
		[ObservableProperty]
		int playingTime;
		[ObservableProperty]
		string totalTime;
		#endregion

		#region Constructor 
		public MainPageViewModel()
		{
			DownloadTasks = new();
			linksDetails = new();
			playList = new();
			mainCts = new();
			SliderTime = 0;
			Volume = 50;
			sliderTimeCopy = TimeSpan.Zero;

			isFirstLaunch = IsTextActive = IsButtonActive = true;
			isFinished = isPlaying = false;
			CurrentPosition = PlayingTime = totalSongs = 0;

			commandState = CommandState.Play;
			DesiredGlyph = "\ue037";
			TotalTime = CurrentTime = "00:00";


			//https://www.youtube.com/playlist?list=PLpwXRl6e3zt_G2Rl-mrLHoDavYpy2Iiwi
			Link = "https://www.youtube.com/playlist?list=PLpwXRl6e3zt_G2Rl-mrLHoDavYpy2Iiwi";
			//Link = "https://www.youtube.com/playlist?list=PL6KyLivlcdp5yGPBKCKvn4suAguD7heMT";

			OnMusicFinished += MainPageViewModel_OnMusicFinished;

			rootPath = Environment.ProcessPath;
#if DEBUG
			for (int i = 0; i < 6; i++)
			{
				rootPath = Directory.GetParent(rootPath).FullName;
			}
#endif
			string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			downloadLocation = Path.Combine(appDataPath, "YTPlayer\\");

			if (!Directory.Exists(downloadLocation))
				Directory.CreateDirectory(downloadLocation);

			ytdlpLocation = Path.Combine(rootPath, "tools\\yt-dlp.exe");
			ffmpegLocation = Path.Combine(rootPath, "tools\\ffmpeg\\bin");
		}
		#endregion

		#region Binding Commands
		[RelayCommand]
		async Task AddToQueue()
		{
			// if link is not empty continue
			if (Link is not null)
			{
				// if it contains playlist link
				if (Link.Contains("https://www.youtube.com/playlist?list="))
				{
					// Validate playlist link
					await AddPlaylist();
					return;
				}
				// if it doesnt contain playlist, but contains vid URL and is valid
				else if (Link.Contains("www.youtube.com/watch?v=") && await ValidateLinkAsync(Link))
				{
					int firstIndex = Link.IndexOf("www.youtube.com/watch?v=");
					StringBuilder sb = new();
					for (int i = firstIndex + "www.youtube.com/watch?v=".Length; i < firstIndex + "www.youtube.com/watch?v=".Length + 11; i++)
						sb.Append(Link[i]);
					playList.AddLast(new QueueStruct { videoID = sb.ToString() });
					if (playList.Count == 1)
						currentNode = playList.First;
				}
				Link = "";
			}
			else
			{
				StringArgs stringArgs = new() { Title = "Text is empty" };
				WorkCompleted?.Invoke(this, stringArgs);
			}
		}

		[RelayCommand]
		void ChangeToThis(Links entry)
		{
			CommandState cmdState = CommandState.Play;
			var tempNode = currentNode;

			while (tempNode.Value.ID != entry.ID)
			{
				// start going right	
				if (CurrentPosition > entry.ID)
				{
					cmdState = CommandState.Previous;
					tempNode = tempNode.Previous;
				}
				// start going left
				else if (CurrentPosition < entry.ID)
				{
					cmdState = CommandState.Next;
					tempNode = tempNode.Next;
				}
				else if (CurrentPosition == entry.ID)
				{
					break;
				}
			}
			if (tempNode.Value.isDownloaded)
			{
				currentNode = tempNode;
				CurrentPosition = entry.ID;
				PlaySpecific(cmdState);
			}
		}

		[RelayCommand]
		async Task Stop()
		{
			commandState = CommandState.Pause;
			OnMusicFinished?.Invoke(this, EventArgs.Empty);
		}

		[RelayCommand]
		async Task Resume()
		{
			StringArgs stringArgs = new() { Title = "Pause" };
			SetUpFinished?.Invoke(this, stringArgs);
			commandState = CommandState.Play;
			outputDevice?.Play();
			DesiredGlyph = "\ue034";
		}

		[RelayCommand]
		async Task Pause()
		{
			StringArgs stringArgs = new() { Title = "Resume" };
			SetUpFinished?.Invoke(this, stringArgs);
			commandState = CommandState.Pause;
			outputDevice?.Pause();
			DesiredGlyph = "\ue037";
		}

		[RelayCommand]
		async Task PlayMusic()
		{
			if (isFirstLaunch)
			{
				StringArgs stringArgs = new() { Title = "Pause" };
				SetUpFinished?.Invoke(this, stringArgs);
				isFirstLaunch = false;
				Task.Run(MusicControl);
				DesiredGlyph = "\ue034";

				TotalDuration = CalculateTime(LinksDetails[CurrentPosition].video.Length);
				TotalTime = LinksDetails[CurrentPosition].video.Length;
			}
		}

		[RelayCommand]
		async Task PlayNext()
		{
			if (currentNode is not null)
			{
				if (currentNode.Next is not null && currentNode.Value.isDownloaded)
				{
					currentNode = currentNode.Next;
					if (commandState != CommandState.Pause)
					{
						commandState = CommandState.Next;
					}

					CurrentPosition++;
					if (!isFinished)
					{
						OnMusicFinished?.Invoke(this, EventArgs.Empty);
					}
					isFinished = isTimeDraggin = false;

					// time setup
					CurrentTime = "00:00";
					sliderTimeCopy = TimeSpan.Zero;
					TotalDuration = CalculateTime(LinksDetails[CurrentPosition].video.Length);
					SliderTime = 0;
					TotalTime = LinksDetails[CurrentPosition].video.Length;

					CurrentImage = LinksDetails[CurrentPosition].video.Thumbnails[1];
				}
				else if (!currentNode.Value.isDownloaded)
				{
					StringArgs stringArgs1 = new() { Title = "Video is not yet downloaded!" };
					WorkCompleted?.Invoke(this, stringArgs1);
				}
			}
		}

		[RelayCommand]
		async Task PlayPrevious()
		{
			if (currentNode is not null)
			{
				if (currentNode.Previous is not null && currentNode.Value.isDownloaded)
				{
					if (commandState != CommandState.Pause)
					{
						commandState = CommandState.Previous;
					}
					currentNode = currentNode.Previous;
					CurrentPosition--;

					OnMusicFinished?.Invoke(this, EventArgs.Empty);


					isTimeDraggin = false;
					// time setup
					CurrentTime = "00:00";
					TotalDuration = CalculateTime(LinksDetails[CurrentPosition].video.Length);
					SliderTime = 0;
					TotalTime = LinksDetails[CurrentPosition].video.Length;

					CurrentImage = LinksDetails[CurrentPosition].video.Thumbnails[1];
				}
				else if (!currentNode.Value.isDownloaded)
				{
					StringArgs stringArgs1 = new() { Title = "Video is not yet downloaded!" };
					WorkCompleted?.Invoke(this, stringArgs1);
				}
			}
		}

		[RelayCommand]
		async Task TimeDragCompleted()
		{
			if (commandState != CommandState.Pause)
			{
				OnMusicFinished?.Invoke(this, EventArgs.Empty);
			}

			sliderTimeCopy = TimeSpan.FromSeconds(SliderTime);
			commandState = CommandState.ChangeTime;
		}

		[RelayCommand]
		async Task TimeDragStarted()
		{
			isTimeDraggin = true;
		}

		[RelayCommand]
		async Task VolumeDragStarted()
		{
			isVolumeDragging = true;
		}

		[RelayCommand]
		async Task VolumeDragCompleted()
		{
			//outputDevice?.Pause();
			//outputDevice?.Play();
			sliderTimeCopy = globalTime;

			OnMusicFinished?.Invoke(this, EventArgs.Empty);
		}

		async Task DownloadSong(LinkedListNode<QueueStruct> tempNode, int ID)
		{
			await semaphore.WaitAsync();
			try
			{
				// check if these items exist
				// grab all files from a cache folder
				DirectoryInfo folderInfo = new(downloadLocation);
				HashSet<String> audioFiles = new(folderInfo.GetFiles()
					.Select(file => Path.GetFileNameWithoutExtension(file.Name))
					.ToList());

				// if it's already downloaded go next
				if (audioFiles.Contains(tempNode.Value.videoID))
				{
					ChangeNodeValue(ref tempNode);
				}
				// if it doesnt exist then download the file
				else
				{
					using (Process dlProcess = new())
					{
						// set up process properties
						dlProcess.StartInfo.FileName = ytdlpLocation;
						dlProcess.StartInfo.CreateNoWindow = true;
						dlProcess.StartInfo.UseShellExecute = false;
						dlProcess.StartInfo.RedirectStandardOutput = true;

						dlProcess.StartInfo.Arguments = $"-P {downloadLocation} -x --extract-audio --audio-format mp3 --audio-quality 320K {tempNode.Value.videoID} --ffmpeg-location \"{ffmpegLocation}\" --no-playlist -o \"%(id)s.%(ext)s\"";

						dlProcess.Start();
						await dlProcess.WaitForExitAsync();
					}
					// mark this item as downloaded
					ChangeNodeValue(ref tempNode);

					// update UI
					//Image doneImg = new()
					//{
					//	Source = new FontImageSource() { FontFamily = "Material", Glyph = "\ue876", Size = 36, Color = Colors.Chocolate }
					//};

				}
				ImageButton imageButton = new()
				{
					Command = ChangeToThisCommand,
					CommandParameter = LinksDetails[ID],
					Source = new FontImageSource { FontFamily = "MaterialOutlined", Glyph = "\ue037" }
				};

				var holder2 = LinksDetails[ID];
				holder2.Img = imageButton;
				LinksDetails[ID] = holder2;
			}
			finally
			{
				semaphore.Release();
			}
		}

		async Task PlaySpecific(CommandState _commandState)
		{
			if (currentNode is not null)
			{
				commandState = _commandState;
				CurrentImage = LinksDetails[CurrentPosition].video.Thumbnails[1];
				OnMusicFinished?.Invoke(this, EventArgs.Empty);
			}
		}

		async void MainPageViewModel_OnMusicFinished(object sender, EventArgs e)
		{
			//if (isFinished)
			cts?.Cancel();
		}
		#endregion

		#region Comands
		async Task MusicControl()
		{
			while (true)
			{
				if ((!isPlaying || outputDevice?.PlaybackState == PlaybackState.Playing) && commandState != CommandState.Pause)
				{
					if (outputDevice is not null)
					{
						outputDevice.Dispose();
						outputDevice = null;
					}

					cts = new();

					if (commandState == CommandState.ChangeTime)
					{
						isTimeDraggin = false;
					}

					isFinished = false;
					commandState = CommandState.Play;
					if (currentNode is not null && currentNode.Value.isDownloaded)
					{
						outputDevice ??= new();

						using var audioFile = new AudioFileReader(Path.Combine(downloadLocation, $"{currentNode.Value.videoID}.mp3"));

						audioFile.CurrentTime = sliderTimeCopy;
						outputDevice.Init(audioFile);
						audioFile.Volume = Volume / 100f;
						outputDevice.Play();
						//outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;

						await Task.Run(() =>
						{
							while (!cts.IsCancellationRequested && outputDevice.PlaybackState == PlaybackState.Playing)
							{
								TimeSpan currentTimeSpan = outputDevice.GetPositionTimeSpan() + sliderTimeCopy;
								globalTime = currentTimeSpan;
								if (commandState == CommandState.Pause)
								{
									sliderTimeCopy = currentTimeSpan;
									outputDevice?.Pause();
									break;
								}

								string minutes = (currentTimeSpan.Minutes <= 9) ? "0" + currentTimeSpan.Minutes.ToString() : currentTimeSpan.Minutes.ToString();
								string seconds = (currentTimeSpan.Seconds <= 9) ? "0" + currentTimeSpan.Seconds.ToString() : currentTimeSpan.Seconds.ToString();

								CurrentTime = $"{minutes}:{seconds}";

								if (!isTimeDraggin)
								{
									SliderTime = currentTimeSpan.Minutes * 60 + currentTimeSpan.Seconds;
								}

								//_playingTime = CalculateTime(CurrentTime);
								//Link = (TotalDuration - _playingTime).ToString();
							}
							if (cts.Token.IsCancellationRequested)
							{
								// just for debugging purposes
							}

						}, cts.Token);

						if (currentNode.Next is not null)
						{
							//if (commandState == CommandState.Play && isFinished)
							if (commandState == CommandState.Play && outputDevice.PlaybackState == PlaybackState.Stopped)
							{
								//currentNode = currentNode.Next;
								CurrentTime = "00:00";
								SliderTime = 0;
								PlayNext();
							}
						}
					}
				}
			}
		}

		async Task DownloadControl()
		{
			while (true)
			{
				if (DownloadTasks.Count > 2)
				{
					// make a list of tasks
					var tasks = new List<Task>();
					for (int i = 0; i < 3; i++)
					{
						var item = DownloadTasks.Dequeue();
						tasks.Add(DownloadSong(item.node, item.ID));
					}

					await Task.WhenAll(tasks);
				}
			}
		}

		private void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
		{
			// calculate time
			int _playingTime = CalculateTime(CurrentTime);
			if (TotalDuration - _playingTime <= 0)
			{
				cts?.Cancel();
				//if (!isTimeDraggin)
				//{
				//}
			}
		}

		async Task AddPlaylist()
		{
			// fetch songs from playlist
			try
			{
				using (HttpClient client = new())
				{
					string responseContent = RequestFunc(client, Link);

					string patternWeb = "\"webResponseContextExtensionData\"";
					int mainIndex = responseContent.IndexOf(patternWeb);
					string patternContent = "\"contents\"";
					int contentIndex = responseContent.IndexOf(patternContent, mainIndex);
					string patternHeader = "\"header\"";
					int patternIndex = responseContent.IndexOf(patternHeader, contentIndex);
					string theActualJson = GetTheBody(responseContent, contentIndex, patternIndex);


					var videos = Welcome10.FromJson(theActualJson);
					//var videos = Videos.FromJson(theActualJson);

					int ID = -1;

					//Stopwatch stopwatch = new();
					//stopwatch.Start();

					foreach (var video in VideoFinalData(videos))
					{
						if (video.PlaylistVideoRenderer is not null)
						{
							ID++;

							var videoData = video.PlaylistVideoRenderer;

							string[] thumbnails = new string[2];
							thumbnails[0] = videoData.Thumbnail.Thumbnails[2].Url.AbsoluteUri;
							string replace = "maxresdefault.jpg";
							string[] copy = thumbnails[0].Split('/');
							if (copy.Length > 4)
							{
								copy[5] = replace;
							}

							StringBuilder sb = new();
							sb.Append("https://i.ytimg.com/vi/");
							sb.Append(copy[4]);
							sb.Append('/');
							sb.Append(replace);

							HttpClient _client = new();
							var _request = new HttpRequestMessage(HttpMethod.Get, sb.ToString());
							using var response = await _client.SendAsync(_request);

							thumbnails[1] = (response.IsSuccessStatusCode) ? sb.ToString() : thumbnails[0];

							Video vd = new()
							{
								Author = videoData.ShortBylineText.Runs[0].Text,
								Title = videoData.Title.Runs[0].Text,
								Length = videoData.LengthText.SimpleText,
								Views = videoData.VideoInfo.Runs[0].Text,
								When = videoData.VideoInfo.Runs[2].Text,
								Thumbnails = thumbnails,
								Color = Colors.Red
							};

							ActivityIndicator activityIndicator = new() { IsRunning = true, Color = Colors.BlueViolet };

							LinksDetails.Add(new() { ID = ID, Url = videoData.VideoId, video = vd, Img = activityIndicator });
							playList.AddLast(new QueueStruct { ID = ID, videoID = videoData.VideoId });

							if (playList.Count == 1)
							{
								CurrentImage = LinksDetails.First().video.Thumbnails[1];
								currentNode = playList.First;
							}

							//downloadTasks.Push(downloadSongTask);
							DownloadTasks.Enqueue(new() { node = playList.Last, ID = ID });
							DownloadSong(playList.Last, ID);
						}
					}
					//stopwatch.Stop();
					//var totalTime = stopwatch.ElapsedMilliseconds;
				}
			}
			catch (Exception e)
			{
				StringArgs stringArgs1 = new() { Title = "Error!" };
				WorkCompleted?.Invoke(this, stringArgs1);
			}

			Link = "Enter a new link!";
			TotalSongs = LinksDetails.Count;
		}

		void ChangeNodeValue(ref LinkedListNode<QueueStruct> tempNode)
		{
			var reference = tempNode.ValueRef;
			reference = new QueueStruct { ID = tempNode.Value.ID, videoID = tempNode.Value.videoID, isDownloaded = true };
			tempNode.ValueRef = reference;
		}

		int CalculateTime(string link)
		{
			string[] numbers = link.Split(":");
			int minutes = Int32.Parse(numbers[0]);
			int seconds = Int32.Parse(numbers[1]);

			return minutes * 60 + seconds;
		}
		#endregion
	}
}







