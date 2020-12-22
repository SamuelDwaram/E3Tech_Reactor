using System.Windows.Controls;

namespace E3Tech.VideoPlayer.Model
{
    public interface IMediaPlayerController
    {
        MediaElement MediaElement { get; set; }

        void OpenMedia(string url);

        void FastForward();

        void Rewind();

        void PlayPause();

        void Stop();


    }
}
