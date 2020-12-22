namespace E3.ReactorManager.IO.FileAccess
{
    public interface IFileBrowser
    {
        string SaveFile(string fileName, string extention);

        string OpenFile(string allowedExtentions);
    }
}
