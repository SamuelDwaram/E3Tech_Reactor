using E3Tech.Camera.BaslerCamera.Model;
using E3Tech.Pylon.Wpf.SDK.Helpers;
using E3Tech.VideoRecorder.StreamSource;
using NLogger;
using PylonC.NETSupportLibrary;
using System;
using System.Drawing;
using System.Linq;

namespace E3Tech.Pylon.Wpf.SDK
{
    internal class BaslerViewer : IBaslerViewer
    {
        private ImageProvider m_imageProvider = new ImageProvider(); /* Create one image provider. */
        private Bitmap m_bitmap = null; /* The bitmap is used for displaying the image. */
        private readonly ILogger logger;
        private PylonNodeValueHandler pylonWidthNodeValueHandler;
        private PylonNodeValueHandler pylonHeightNodeValueHandler;
        private int frameWidth;
        private int frameHeight;

        public BaslerViewer(ILogger logger)
        {
            this.logger = logger;

#if DEBUG
            /* This is a special debug setting needed only for GigE cameras.
                See 'Building Applications with pylon' in the Programmer's Guide. */
            Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "300000" /*ms*/);
#endif
            PylonC.NET.Pylon.Initialize();

            /* Register for the events of the image provider needed for proper operation. */
            m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
            m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
            m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
            m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
            m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
            m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
            m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);

            pylonHeightNodeValueHandler = new PylonNodeValueHandler();
            pylonWidthNodeValueHandler = new PylonNodeValueHandler();
        }

        private void ShowException(Exception e, string additionalErrorMessage)
        {
            string more = "\n\nLast error message (may not belong to the exception):\n" + additionalErrorMessage;
            logger.LogError("Exception caught:\n" + e.Message + (additionalErrorMessage.Length > 0 ? more : ""));
        }

        public void Terminate()
        {
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();
            PylonC.NET.Pylon.Terminate();
        }

        /* Handles the event related to the occurrence of an error while grabbing proceeds. */
        private void OnGrabErrorEventCallback(object sender, GrabErrorEventArgs e)
        {
            ShowException(e.GrabException, e.AdditionalErrorMessage);
        }

        /* Handles the event related to the removal of a currently open device. */
        private void OnDeviceRemovedEventCallback(object sender, EventArgs e)
        {
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();
            /* Since one device is gone, the list needs to be updated. */
            // raise new event
        }

        /* Handles the event related to a device being open. */
        private void OnDeviceOpenedEventCallback(object sender, EventArgs e)
        {

        }

        /* Handles the event related to a device being closed. */
        private void OnDeviceClosedEventCallback(object sender, EventArgs e)
        {

        }

        /* Handles the event related to the image provider executing grabbing. */
        private void OnGrabbingStartedEventCallback(object sender, EventArgs e)
        {
            /* Do not update device list while grabbing to avoid jitter because the GUI-Thread is blocked for a short time when enumerating. */
            // raise new event.
        }

        /* Handles the event related to an image having been taken and waiting for processing. */
        private void OnImageReadyEventCallback(object sender, EventArgs e)
        {
            try
            {
                /* We have to dispose the bitmap after assigning the new one to the display control. */
                if (m_bitmap != null)
                {
                    /* Dispose the bitmap. */
                    m_bitmap.Dispose();
                }

                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = m_imageProvider.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image != null)
                {
                    BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
                    BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);

                    /* Provide the display control with the new bitmap. This action automatically updates the display. */
                    NewImageReady?.Invoke(this, new NewImageReadyEventArgs() {  Bitmap = m_bitmap });

                    /* The processing of the image is done. Release the image buffer. */
                    m_imageProvider.ReleaseImage();
                    /* The buffer can be used for the next image grabs. */
                }
            }
            catch (Exception ex)
            {
                ShowException(ex, m_imageProvider.GetLastErrorMessage());
            }
        }


        /* Handles the event related to the image provider having stopped grabbing. */
        private void OnGrabbingStoppedEventCallback(object sender, EventArgs e)
        {

        }

        /* Stops the image provider and handles exceptions. */
        private void Stop()
        {
            /* Stop the grabbing. */
            try
            {
                m_imageProvider.Stop();
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
            finally
            {
                FeedStopped?.Invoke(this, new EventArgs());
            }
        }

        /* Closes the image provider and handles exceptions. */
        private void CloseTheImageProvider()
        {
            /* Close the image provider. */
            try
            {
                m_imageProvider.Close();
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Starts the grabbing of one image and handles exceptions. */
        private void OneShot()
        {
            try
            {
                m_imageProvider.OneShot(); /* Starts the grabbing of one image. */
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Starts the grabbing of images until the grabbing is stopped and handles exceptions. */
        private void ContinuousShot()
        {
            try
            {
                m_imageProvider.ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Handles the selection of cameras from the list box. The currently open device is closed and the first
          selected device is opened. */
        public void SelectDevice(uint deviceIndex)
        {
            /* Close the currently open image provider. */
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();

            try
            {
                /* Open the image provider using the index from the device data. */
                m_imageProvider.Open(deviceIndex);
                SetCameraResolution(CameraResolutionEnum.Max);/* Here you can change the Camera Resolution */
                //SetAcquistionFrameRateOfCameraAndImageROI();
                m_imageProvider.OneShot();
            }
            catch (Exception ex)
            {
                ShowException(ex, m_imageProvider.GetLastErrorMessage());
            }
        }

        private void SetAcquistionFrameRateOfCameraAndImageROI()
        {
            m_imageProvider.SetAcquistionRateOfCameraAndImageROI(30.0);
        }

        public CameraResolutionEnum[] CameraResolutions => Enum.GetValues(typeof(CameraResolutionEnum)).Cast<CameraResolutionEnum>().ToArray();

        public void SetCameraResolution(CameraResolutionEnum cameraResolution)
        {
            bool isLiveFeed = m_imageProvider.IsGrabbingVideo();
            if (isLiveFeed)
            {
                m_imageProvider.Stop();
            }

            CameraResolutionProvider.GetCameraResolution(cameraResolution, out frameWidth, out frameHeight);
            pylonWidthNodeValueHandler.SetNodeValue(frameWidth);
            pylonHeightNodeValueHandler.SetNodeValue(frameHeight);
            if (isLiveFeed)
            {
                m_imageProvider.ContinuousShot();
            }
        }

        void IBaslerViewer.ContinuousShot()
        {
            bool isLiveFeed = m_imageProvider.IsGrabbingVideo();
            if (isLiveFeed)
            {
                Stop();
            }
            else
            {
                m_imageProvider.ContinuousShot();
            }
        }

        void IBaslerViewer.OneShot()
        {
            m_imageProvider.OneShot();
        }

        public bool IsOpen => m_imageProvider.IsOpen;

        public int FrameWidth => frameWidth;

        public int FrameHeight => frameHeight;

        public event NewImageReadyEventHandler NewImageReady;
        public event EventHandler FeedStopped;
    }
}
