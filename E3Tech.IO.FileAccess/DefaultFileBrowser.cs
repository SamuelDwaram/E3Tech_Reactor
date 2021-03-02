using Microsoft.Win32;
using System;

namespace E3Tech.IO.FileAccess
{
    public class DefaultFileBrowser : IFileBrowser
    {
        public string OpenFile(string allowedExtentions)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = allowedExtentions,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };
            if (dialog.ShowDialog() != true)
                return null;
            return dialog.FileName;
        }

        public string SaveFile(string fileName, string extention)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                FileName = fileName,
                DefaultExt = extention,
                AddExtension = true
            };
            bool? dialogresult = dialog.ShowDialog();
            if (dialogresult != true)
            {
                return null;
            }
            return dialog.FileName;
        }
    }
}
