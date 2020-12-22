using System;
using System.Drawing;

namespace E3Tech.VideoRecorder.StreamSource
{
    public interface IVideoStreamSource
    {
        int FrameWidth { get; }
        int FrameHeight { get; }

        event NewImageReadyEventHandler NewImageReady;
        event EventHandler FeedStopped;
        
        void Terminate();
    }

    public delegate void NewImageReadyEventHandler(object sender, NewImageReadyEventArgs e);

    public class NewImageReadyEventArgs:EventArgs
    {
        public Bitmap Bitmap { get; set; }
    }

    public delegate void VideoStreamSourceFailedEventHandler(object sender, VideoStreamSourceFailedEventArgs videoStreamSourceFailedEventArgs);

    public class VideoStreamSourceFailedEventArgs : EventArgs
    {
        public string ErrorCode { get; set; }

        public string FailureMessage { get; set; }

        public string ResolutionMessageIfAny { get; set; }
    }
}
