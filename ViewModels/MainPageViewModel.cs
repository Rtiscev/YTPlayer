using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DownloadMusic.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NAudio.Wave;
using static DownloadMusic.Utility;
using System.Text;
using NAudio.Utils;
using QuickType;


// &#xe037

namespace YTPlayer.ViewModels
{
	public partial class MainPageViewModel : ObservableObject
	{
		#region General elements

		enum CommandState
		{
			Play, Pause, Next, Previous, Stop
		}
		struct QueueStruct
		{
			public string videoID { get; set; }
			public bool isDownloaded { get; set; }

			public QueueStruct()
			{
				isDownloaded = false;
			}
		}
		public event EventHandler<StringArgs> WorkCompleted;
		public event EventHandler<StringArgs> SetUpFinished;
		string downloadLocation;
		string ytdlpLocation;
		string ffmpegLocation;
		string rootPath;
		bool isPlaying;
		bool isPaused;
		bool isDownloadStarted;
		bool isFirstLaunch;
		WasapiOut outputDevice;
		LinkedList<QueueStruct> playList;
		LinkedListNode<QueueStruct> currentNode;
		event EventHandler OnMusicFinished;
		CancellationTokenSource cts;
		CancellationTokenSource mainCts;
		CommandState commandState;

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
		#endregion

		#region Constructor 
		public MainPageViewModel()
		{
			//outputDevice = new();
			linksDetails = new();
			playList = new();
			mainCts = new();
			isFirstLaunch = true;
			isPlaying = false;
			isPaused = true;
			IsButtonActive = true;
			IsTextActive = true;
			isDownloadStarted = false;
			commandState = CommandState.Play;
			DesiredGlyph = "\ue037";
			Link = "https://www.youtube.com/playlist?list=PL6KyLivlcdp5yGPBKCKvn4suAguD7heMT";

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
				StringArgs stringArgs = new StringArgs { Title = "Text is empty" };
				WorkCompleted?.Invoke(this, stringArgs);
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
			outputDevice.Play();
		}

		[RelayCommand]
		async Task Pause()
		{
			StringArgs stringArgs = new() { Title = "Resume" };
			SetUpFinished?.Invoke(this, stringArgs);
			commandState = CommandState.Pause;
		}

		[RelayCommand]
		async Task PlayMusic()
		{
			if (isFirstLaunch)
			{
				StringArgs stringArgs = new() { Title = "Pause" };
				SetUpFinished?.Invoke(this, stringArgs);
				isFirstLaunch = false;
				Task.Run(StartDownloading);
				Task.Run(MusicControl);
			}
		}

		[RelayCommand]
		async Task PlayNext()
		{
			if (currentNode is not null)
			{
				if (currentNode.Next is not null)
				{
					currentNode = currentNode.Next;
					commandState = CommandState.Next;
					OnMusicFinished?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		[RelayCommand]
		async Task PlayPrevious()
		{
			if (currentNode is not null)
			{
				if (currentNode.Previous is not null)
				{
					currentNode = currentNode.Previous;
					commandState = CommandState.Previous;
					OnMusicFinished?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		async void MainPageViewModel_OnMusicFinished(object sender, EventArgs e)
		{
			cts.Cancel();
		}

		[RelayCommand]
		async Task StartDownloading()
		{
			// check if these items exist
			// grab all files from a cache folder
			DirectoryInfo folderInfo = new DirectoryInfo(downloadLocation);
			HashSet<String> audioFiles = new(folderInfo.GetFiles()
				.Select(file => Path.GetFileNameWithoutExtension(file.Name))
				.ToList());

			for (var tempNode = playList.First; tempNode != null; tempNode = tempNode.Next)
			{
				// if it's already downloaded go next
				if (audioFiles.Contains(tempNode.Value.videoID))
				{
					ChangeNodeValue(ref tempNode);
					continue;
				}

				// if it doesnt exist then download the file
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
			}
		}
		#endregion

		#region Helper Methods
		async Task MusicControl()
		{
			while (true)
			{
				if (!isPlaying || outputDevice?.PlaybackState == PlaybackState.Playing)
				{
					if (outputDevice is not null)
					{
						outputDevice.Dispose();
						outputDevice = null;
					}

					cts = new();
					commandState = CommandState.Play;
					if (currentNode is not null && currentNode.Value.isDownloaded)
					{
						if (outputDevice is null)
							outputDevice = new();

						using var audioFile = new AudioFileReader(Path.Combine(downloadLocation, $"{currentNode.Value.videoID}.mp3"));
						outputDevice.Init(audioFile);
						outputDevice.Play();
						TimeSpan audioTimeSpan = audioFile.TotalTime;

						await Task.Run(() =>
						{
							while (!cts.IsCancellationRequested)
							{
								TimeSpan currentTimeSpan = outputDevice.GetPositionTimeSpan();
								if (currentTimeSpan >= audioTimeSpan)
								{
									break;
								}

								if (commandState == CommandState.Pause)
								{
									outputDevice.Pause();
								}
							}

							//isPlaying = true;
							if (cts.Token.IsCancellationRequested)
							{
								// just for debugging purposes
							}
						}, cts.Token);

						if (currentNode.Next is not null && commandState == CommandState.Play)
						{
							currentNode = currentNode.Next;
						}
					}
				}
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

					//int mainIndex = responseContent.IndexOf("\"webResponseContextExtensionData\"");
					//int contentIndex = responseContent.IndexOf("\"contents\"", mainIndex);
					//int headerIndex = responseContent.IndexOf("\"header\"", contentIndex);
					//string theActualJson = GetTheBody(responseContent, contentIndex, headerIndex);

					string patternWeb = "\"webResponseContextExtensionData\"";
					int mainIndex = responseContent.IndexOf(patternWeb);
					string patternContent = "\"contents\"";
					int contentIndex = responseContent.IndexOf(patternContent, mainIndex);
					string patternHeader = "\"header\"";
					int patternIndex = responseContent.IndexOf(patternHeader, contentIndex);
					string theActualJson = GetTheBody(responseContent, contentIndex, patternIndex);

					//var root = JsonSerializer.Deserialize<Root>(theActualJson);
					var videos = Videos.FromJson(theActualJson);


					foreach (var video in VideoFinalData(videos))
					{
						if (video.PlaylistVideoRenderer is not null)
						{
							playList.AddLast(new QueueStruct { videoID = video.PlaylistVideoRenderer.VideoId });
							if (playList.Count == 1)
								currentNode = playList.First;
						}
					}
				}
			}
			catch (Exception e)
			{
				StringArgs stringArgs1 = new() { Title = "Not a valid playlist link" };
				WorkCompleted?.Invoke(this, stringArgs1);
			}

			Link = "";
		}

		void ChangeNodeValue(ref LinkedListNode<QueueStruct> tempNode)
		{
			var reference = tempNode.ValueRef;
			reference = new QueueStruct { videoID = tempNode.Value.videoID, isDownloaded = true };
			tempNode.ValueRef = reference;
		}

		#endregion
	}
}