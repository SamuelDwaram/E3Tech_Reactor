using E3.ReactorManager.VideoCaptureDevicesHandler.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Unity;
using WebEye.Controls.Wpf;

namespace E3.ReactorManager.VideoCaptureDevicesHandler.ViewModels
{
    public class SelectedCaptureDeviceViewModel : BindableBase
    {
        private readonly IVideoCaptureDevicesHandler videoCaptureDevicesHandler;

        public SelectedCaptureDeviceViewModel(IUnityContainer containerProvider, IVideoCaptureDevicesHandler videoCaptureDevicesHandler)
        {
            this.videoCaptureDevicesHandler = videoCaptureDevicesHandler;
            videoCaptureDevicesHandler.NewImageReady += VideoCaptureDevicesHandler_NewImageReady;
            UpdateCaptureDevices();
            SelectFirstCameraIfFound();
        }

        private void VideoCaptureDevicesHandler_NewImageReady(object sender, LiveImageEventArgs e)
        {
            LiveImage = e.ImageSource;
            FailedToGetImage = !e.ImageCaptureStatus;
        }

        private void SelectFirstCameraIfFound()
        {
            if (CaptureDevicesList.Count() > 0)
            {
                SelectedDevice = CaptureDevicesList.ElementAt(0);
            }
        }

        private void UpdateCaptureDevices()
        {
            CaptureDevicesList = videoCaptureDevicesHandler.GetCaptureDevicesList();
        }

        private void StartCaptureDevice(WebCameraId cameraId)
        {
            if (SelectedDevice != null)
            {
                FailedToGetImage = false;
                videoCaptureDevicesHandler.StartCapturing(cameraId);
            }
        }

        private void StopCaptureDevice()
        {
            videoCaptureDevicesHandler.StopCapturing();

            LiveImage = default;
        }

        private void CaptureImage()
        {
            if (!string.IsNullOrWhiteSpace(DeviceId))
            {
                videoCaptureDevicesHandler.CaptureImage(DeviceId);
            }
        }

        private void PageLoaded(string deviceId)
        {
            DeviceId = deviceId;
        }

        #region Commands

        private ICommand _startCaptureDeviceCommand;
        public ICommand StartCaptureDeviceCommand
        {
            get => _startCaptureDeviceCommand ?? (_startCaptureDeviceCommand = new DelegateCommand<WebCameraId>(new Action<WebCameraId>(StartCaptureDevice)));
            set => _startCaptureDeviceCommand = value;
        }

        private ICommand _captureImageCommand;
        public ICommand CaptureImageCommand
        {
            get => _captureImageCommand ?? (_captureImageCommand = new DelegateCommand(new Action(CaptureImage)));
            set => _captureImageCommand = value;
        }

        private ICommand _stopCaptureDeviceCommand;
        public ICommand StopCaptureDeviceCommand
        {
            get => _stopCaptureDeviceCommand ?? (_stopCaptureDeviceCommand = new DelegateCommand(new Action(StopCaptureDevice)));
            set => _stopCaptureDeviceCommand = value;
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand<string>(new Action<string>(PageLoaded)));
            set => _loadedCommand = value;
        }
        
        #endregion

        #region Properties
        public string DeviceId { get; set; }

        private IEnumerable<WebCameraId> _captureDevicesList;
        public IEnumerable<WebCameraId> CaptureDevicesList
        {
            get => _captureDevicesList ?? (_captureDevicesList = new List<WebCameraId>());
            set
            {
                _captureDevicesList = value;
                RaisePropertyChanged();
            }
        }

        private WebCameraId _selectedDevice;
        public WebCameraId SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                RaisePropertyChanged();
            }
        }

        private BitmapSource _liveImage;
        public BitmapSource LiveImage
        {
            get => _liveImage;
            set
            {
                _liveImage = value;
                RaisePropertyChanged();
            }
        }

        private bool _failedToGetImage = false;
        public bool FailedToGetImage
        {
            get { return _failedToGetImage; }
            set { SetProperty(ref _failedToGetImage, value); }
        }
        #endregion
    }
}
