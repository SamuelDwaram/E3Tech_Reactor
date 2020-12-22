namespace E3Tech.Pylon.Wpf.SDK.Helpers
{
    public enum CameraResolutionEnum
    {
        Max,
        FullHD,
        WSXGA,
        HD,
        XGA,
        SVGA,
        VGA,
    }

    public sealed class CameraResolutionProvider
    {

        public static void GetCameraResolution(CameraResolutionEnum key, out int width, out int height)
        {
            switch (key)
            {
                case CameraResolutionEnum.FullHD:
                    width = 1920;
                    height = 1080;
                    break;
                case CameraResolutionEnum.WSXGA:
                    width = 1440;
                    height = 900;
                    break;
                case CameraResolutionEnum.HD:
                    width = 1366;
                    height = 768;
                    break;
                case CameraResolutionEnum.XGA:
                    width = 1024;
                    height = 768;
                    break;
                case CameraResolutionEnum.SVGA:
                    width = 800;
                    height = 600;
                    break;
                case CameraResolutionEnum.Max:
                default:
                    width = 2590;
                    height = 1942;
                    break;
            }
        }
    }
}
