using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DownloadMusic.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using NAudio.Wave;
using static DownloadMusic.Utility;
using static DownloadMusic.YtModel;
using System.Text;

// &#xe037

namespace YTPlayer.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        #region General elements
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
        string downloadLocation;
        string ytdlpLocation;
        string ffmpegLocation;
        string rootPath;
        bool isPlaying;
        bool isPaused;
        bool isDownloadStarted;
        WasapiOut outputDevice;
        LinkedList<QueueStruct> playList;
        LinkedListNode<QueueStruct> currentNode;
        event EventHandler OnMusicFinished;
        #endregion

        #region MVVM elements
        [ObservableProperty]
        double progress;
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
            outputDevice = new();
            linksDetails = new();
            playList = new();
            isPlaying = false;
            isPaused = true;
            IsButtonActive = true;
            IsTextActive = true;
            isDownloadStarted = false;
            DesiredGlyph = "\ue037";
            Link = "https://www.youtube.com/playlist?list=PL6KyLivlcdp5yGPBKCKvn4suAguD7heMT";

            OnMusicFinished += MainPageViewModel_OnMusicFinished;

            //outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
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
        async Task PlayMusic()
        {
            isPaused = !isPaused;

            // if paused button has been pressed and the state of device is playing then pause it
            if (isPaused && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                DesiredGlyph = "\ue034";
                outputDevice.Pause();
            }

            // start downloading everything 
            if (!isDownloadStarted)
                Task.Run(StartDownloading);

            // then play it
            if (playList is not null && !isPlaying)
                Task.Run(Play);

            isPlaying = !isPlaying;
            isDownloadStarted = true;
        }

        [RelayCommand]
        async Task PlayNext()
        {
            if (currentNode is not null)
            {
                if (currentNode.Next is not null)
                {
                    currentNode = currentNode.Next;
                    if (currentNode.Value.isDownloaded)
                    {
                        if (outputDevice.PlaybackState == PlaybackState.Playing)
                            outputDevice.Stop();
                        outputDevice.Dispose();
                        outputDevice = new();
                        //outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
                        using var audioFile = new AudioFileReader(Path.Combine(downloadLocation, $"{currentNode.Value.videoID}.mp3"));
                        audioFile.CurrentTime = new TimeSpan(0, 1, 30);
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        await Task.Run(() =>
                        {
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                            }
                        });
                        OnMusicFinished?.Invoke(this, EventArgs.Empty);
                    }
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
                    if (currentNode.Value.isDownloaded)
                    {
                        if (outputDevice.PlaybackState == PlaybackState.Playing)
                            outputDevice.Stop();
                        outputDevice.Dispose();
                        outputDevice = new();
                        using var audioFile = new AudioFileReader(Path.Combine(downloadLocation, $"{currentNode.Value.videoID}.mp3"));
                        audioFile.CurrentTime = new TimeSpan(0, 1, 30);
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        await Task.Run(() =>
                        {
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                            }
                        });
                        OnMusicFinished?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        private async void MainPageViewModel_OnMusicFinished(object sender, EventArgs e)
        {
            await Task.Run(PlayNext);
        }

        //       https://www.youtube.com/playlist?list=PL6KyLivlcdp5yGPBKCKvn4suAguD7heMT
        private void MainPageViewModel_MusicFinished()
        {
            var g = 6;
            //Task.Run();
        }

        [RelayCommand]
        async Task StartDownloading()
        {
            // check if these items exist logic
            // grab all files from a folder
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
        async Task Play()
        {
            if (currentNode is not null)
            {
                while (true)
                {
                    if (currentNode.Value.isDownloaded)
                    {
                        using var audioFile = new AudioFileReader(Path.Combine(downloadLocation, $"{currentNode.Value.videoID}.mp3"));
                        audioFile.CurrentTime = new TimeSpan(0, 1, 30);
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        await Task.Run(() =>
                        {
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                            }
                        });
                        if (currentNode.Next is not null)
                        {
                            currentNode = currentNode.Next;
                        }
                        OnMusicFinished?.Invoke(this, EventArgs.Empty);
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

                    int mainIndex = responseContent.IndexOf("\"webResponseContextExtensionData\"");
                    int contentIndex = responseContent.IndexOf("\"contents\"", mainIndex);
                    int headerIndex = responseContent.IndexOf("\"header\"", contentIndex);

                    string theActualJson = GetTheBody(responseContent, contentIndex, headerIndex);

                    var root = JsonSerializer.Deserialize<Root>(theActualJson);

                    foreach (var video in VideoFinalData(root))
                    {
                        playList.AddLast(new QueueStruct { videoID = video.playlistVideoRenderer.videoId });
                        if (playList.Count == 1)
                            currentNode = playList.First;
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

/// TO DO:
/// CHANGE THE WAY CURRENTNODE WORKS TO REF THE PLAYLIST LL
