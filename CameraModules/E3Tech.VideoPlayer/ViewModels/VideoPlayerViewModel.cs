using E3Tech.IO.FileAccess;
using E3Tech.VideoPlayer.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace E3Tech.VideoPlayer.ViewModels
{
    public class VideoPlayerViewModel : BindableBase
    {
        private readonly IFileBrowser fileBrowser;
        private readonly IMediaPlayerController mediaPlayerController;

        public VideoPlayerViewModel(IFileBrowser fileBrowser, IMediaPlayerController mediaPlayerController)
        {
            OpenMediaFileCommand = new DelegateCommand(new Action(Open));
            PlayPauseVideoCommand = new DelegateCommand(new Action(PlayPause));
            StopVideoCommand = new DelegateCommand(new Action(Stop));
            FastForwardCommand = new DelegateCommand(new Action(FastForward));
            RewindCommand = new DelegateCommand(new Action(Rewind));
            this.fileBrowser = fileBrowser;
            this.mediaPlayerController = mediaPlayerController;
        }

        private void Stop()
        {
            mediaPlayerController.Stop();
        }

        private void Rewind()
        {
            mediaPlayerController.Rewind();
        }

        private void FastForward()
        {
            mediaPlayerController.FastForward();
        }

        private void PlayPause()
        {
            mediaPlayerController.PlayPause();
        }

        private ICommand openMediaFileCommand;
        public ICommand OpenMediaFileCommand
        {
            get => openMediaFileCommand;
            set => openMediaFileCommand = value;
        }

        private ICommand playPauseVideoCommand;
        public ICommand PlayPauseVideoCommand
        {
            get => playPauseVideoCommand;
            set => playPauseVideoCommand = value;
        }

        private ICommand stopVideoCommand;
        public ICommand StopVideoCommand
        {
            get => stopVideoCommand;
            set => stopVideoCommand = value;
        }

        private ICommand fastForwardCommand;
        public ICommand FastForwardCommand
        {
            get => fastForwardCommand;
            set => fastForwardCommand = value;
        }

        private ICommand rewindCommand;
        public ICommand RewindCommand
        {
            get => rewindCommand;
            set => rewindCommand = value;
        }

        private void Open()
        {
            string value = fileBrowser.OpenFile(".mkv");
            mediaPlayerController.OpenMedia(value);
            mediaPlayerController.PlayPause();
        }

        private string fileName;
        public string FileName
        {
            get => fileName;
            set => SetProperty<string>(ref fileName, value);
        }

        public IMediaPlayerController MediaPlayerController => mediaPlayerController;
    }
}
