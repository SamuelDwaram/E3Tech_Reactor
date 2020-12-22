using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using WebEye.Controls.Wpf;

namespace E3.ReactorManager.VideoCaptureDevicesHandler.Model
{
    public interface IVideoCaptureDevicesHandler
    {
        IEnumerable<WebCameraId> GetCaptureDevicesList();

        void StartCapturing(WebCameraId cameraId);

        void CaptureImage(string fieldDeviceIdentifier);

        void StopCapturing();

        event LiveImageEventHandler NewImageReady;
    }

    public class LiveImageEventArgs : EventArgs
    {
        public BitmapSource ImageSource { get; set; }
        public bool ImageCaptureStatus { get; set; } = true;
    }

    public delegate void LiveImageEventHandler(object sender, LiveImageEventArgs e);
}
