using E3Tech.Camera.BaslerCamera.Model;
using E3Tech.IO.FileAccess;
using E3Tech.VideoRecorder.StreamSource;
using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PylonC.NETSupportLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace E3Tech.Camera.BaslerCamera.ViewModels
{
    public class PylonViewerModel : BindableBase, IDisposable
    {
        private Dispatcher dispatcher;
        private IBaslerViewer pylonViewer;
        private IFileBrowser fileBrowser;
        private Bitmap currentFrame;
        private Timer updateDeviceListTimer;
        IEventAggregator eventAggregator;

        public PylonViewerModel(IBaslerViewer pylonViewer, IFileBrowser fileBrowser, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.pylonViewer = pylonViewer;
            this.fileBrowser = fileBrowser;
            TakeSnapshot = new DelegateCommand<object>(new Action<object>(ExecuteTakeSnapshot), new Func<object, bool>(GetTakeSnapshotCommandCanExecute));
            CaptureVideo = new DelegateCommand<object>(new Action<object>(ExecuteCaptureVideo), new Func<object, bool>(GetTakeSnapshotCommandCanExecute));
            CameraDevices = new List<CameraDevice>();
        }

        internal void Initialize(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            updateDeviceListTimer = new Timer(5000);
            updateDeviceListTimer.Elapsed += UpdateDeviceListTimer_Elapsed;
            pylonViewer.NewImageReady += PylonViewer_NewImageReady;
            UpdateDeviceList();
            SelectFirstCameraIfFound();
        }

        /* Timer callback used for periodically checking whether displayed devices are still attached to the PC. */
        private void UpdateDeviceListTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateDeviceList();
        }

        private void UpdateDeviceList()
        {
            CameraDevices = DeviceEnumerator.EnumerateDevices();
        }

        private void SelectFirstCameraIfFound()
        {
            if (CameraDevices.Count > 0)
            {
                SelectedCameraIndex = 0;

                pylonViewer.OneShot(); /* Starts the grabbing of one image. */
            }
        }

        private void PylonViewer_NewImageReady(object sender, NewImageReadyEventArgs e)
        {
            if (!this.dispatcher.CheckAccess())
            {
                // the method has been called by Pylon SDK thread which is different than the UI thread.
                // we need to dispatch the event so the else code block gets executed on UI thread.
                this.dispatcher.BeginInvoke(new NewImageReadyEventHandler(PylonViewer_NewImageReady), sender, e);
            }
            else
            {
                // event has been dispatched, safe to update the UI.
                currentFrame = TrytoProduceBitmapClone(e.Bitmap);
                Source = ConvertBitmap(currentFrame);

                if (currentFrame == default)
                {
                    //Video Feed has stopped Restart the video feed
                    if (GetTakeSnapshotCommandCanExecute(null))
                    {
                        ExecuteCaptureVideo(null);
                    }
                }
            }
        }

        private Bitmap TrytoProduceBitmapClone(Bitmap bitmap)
        {
            try
            {
                var bitmapClone = bitmap.Clone() as Bitmap;
                return bitmapClone;
            }
            catch (Exception)
            {
                return default;
            }
        }

        private void ExecuteCaptureVideo(object obj)
        {
            pylonViewer.ContinuousShot();
        }

        private bool GetTakeSnapshotCommandCanExecute(object obj)
        {
            return cameraDevices.Count > 0 && pylonViewer.IsOpen;
        }

        private void ExecuteTakeSnapshot(object obj)
        {
            string fileName = fileBrowser.SaveFile("Snapshot1.jpg", ".jpg");
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                pylonViewer.OneShot();
                currentFrame.Save(fileName, ImageFormat.Jpeg);
                eventAggregator.GetEvent<SaveCapturedImageToDatabase>()
                    .Publish(new CapturedImageArgs { DeviceId = "Reactor_4", ImageBitmap = currentFrame });
            }
        }

        private BitmapImage ConvertBitmap(Bitmap src)
        {
            if (src != null)
            {
                MemoryStream ms = new MemoryStream();
                src.Save(ms, ImageFormat.Bmp);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }

            return default;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PylonViewerModel()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dispatcher = null;
                pylonViewer = null;
                fileBrowser = null;
                currentFrame = null;
            }
            updateDeviceListTimer.Dispose();
        }

        private List<CameraDevice> cameraDevices;
        public List<CameraDevice> CameraDevices
        {
            get => cameraDevices;
            private set
            {
                cameraDevices = value;
                RaisePropertyChanged("CameraDevices");
            }
        }

        private uint selectedCameraIndex = uint.MinValue;
        public uint SelectedCameraIndex
        {
            get => selectedCameraIndex;
            set
            {
                selectedCameraIndex = value;
                pylonViewer.SelectDevice(value);
                RaisePropertyChanged("SelectedCameraIndex");
            }
        }


        private ICommand takeSnapshot;
        public ICommand TakeSnapshot
        {
            get => takeSnapshot;
            private set
            {
                takeSnapshot = value;
                RaisePropertyChanged("TakeSnapshot");
            }
        }

        private ICommand captureVideo;
        public ICommand CaptureVideo
        {
            get => captureVideo;
            private set
            {
                captureVideo = value;
                RaisePropertyChanged("CaptureVideo");
            }
        }

        private BitmapSource source;
        public BitmapSource Source
        {
            get => source;
            set
            {
                source = value;
                RaisePropertyChanged("Source");
            }
        }
    }
}
