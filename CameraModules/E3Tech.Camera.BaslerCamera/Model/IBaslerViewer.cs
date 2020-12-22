using E3Tech.VideoRecorder.StreamSource;

namespace E3Tech.Camera.BaslerCamera.Model
{
    public interface IBaslerViewer : IVideoStreamSource
    {
        bool IsOpen { get; }

        void SelectDevice(uint value);

        void ContinuousShot();

        void OneShot();
    }

}
