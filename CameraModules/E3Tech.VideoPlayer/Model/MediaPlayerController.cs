using System;
using System.Windows.Controls;

namespace E3Tech.VideoPlayer.Model
{
    public class MediaPlayerController : IMediaPlayerController
    {
        private bool isPlaying = false;

        public MediaPlayerController()
        {

        }

        public void FastForward()
        {
            MediaElement.Position = MediaElement.Position.Add(TimeSpan.FromSeconds(10));
        }

        public void OpenMedia(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                MediaElement.Source = new Uri(url);
            }
        }

        public void PlayPause()
        {
            if (!isPlaying && MediaElement.Source != null)
            {
                MediaElement.Play();
                isPlaying = true;
            }
            else if (MediaElement.CanPause)
            {
                isPlaying = false;
                MediaElement.Pause();
            }
            else
            {
                isPlaying = false;
            }
        }

        public void Rewind()
        {
            MediaElement.Position = MediaElement.Position.Add(TimeSpan.FromSeconds(-10));
        }

        public void Stop()
        {
            isPlaying = false;
            MediaElement.Stop();
        }

        public MediaElement MediaElement { get; set; }

    }
}
