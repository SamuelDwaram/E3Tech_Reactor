using Accord.Video.FFMPEG;
using E3Tech.VideoRecorder.StreamSource;
using System;
using System.Drawing;
using Unity;

namespace E3Tech.VideoRecorder.Model
{
    public class VideoRecorder : IVideoRecorder, IDisposable
    {
        private DateTime? firstFrameTime;
        private VideoFileWriter videoFileWriter;
        private bool isRecording = false;

        private IVideoStreamSource streamSource;

        public VideoRecorder(IUnityContainer container)
        {
            streamSource = container.Resolve<IVideoStreamSource>();
            streamSource.NewImageReady += StreamSource_NewImageReady;
            streamSource.FeedStopped += StreamSource_FeedStopped;
        }

        private void StreamSource_FeedStopped(object sender, EventArgs e)
        {
            StopRecording();
        }

        private void StreamSource_NewImageReady(object sender, NewImageReadyEventArgs e)
        {
            AddFrame(e.Bitmap);
        }

        private void AddFrame(Bitmap bitmap)
        {
            if (!isRecording)
            {
                return;
            }

            if (firstFrameTime != null)
            {
                videoFileWriter.WriteVideoFrame(bitmap.Clone() as Bitmap, DateTime.Now - firstFrameTime.Value);
            }
            else
            {
                videoFileWriter.WriteVideoFrame(bitmap.Clone() as Bitmap);
                firstFrameTime = DateTime.Now;
            }
        }

        public void StartRecording(string fileName)
        {
            if (!isRecording)
            {
                firstFrameTime = null;
                videoFileWriter = new VideoFileWriter();
                videoFileWriter.Open(fileName, streamSource.FrameWidth, streamSource.FrameHeight);
                isRecording = true;
            }
        }

        public void StopRecording()
        {
            if (isRecording)
            {
                isRecording = false;
                videoFileWriter.Close();
                videoFileWriter.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are.
        ~VideoRecorder()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                streamSource = null;
            }
            // free native resources.
            if (videoFileWriter != null)
            {
                videoFileWriter.Dispose();
            }
        }

        public IVideoStreamSource StreamSource => streamSource;
    }
}
