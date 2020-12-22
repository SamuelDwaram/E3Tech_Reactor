using Microsoft.Win32;
using System;

namespace E3Tech.IO.FileAccess
{
    public class DefaultFileBrowser : IFileBrowser
    {
        public string OpenFile(string allowedExtentions)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = allowedExtentions;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() != true)
                return null;
            return dialog.FileName;
        }

        public string SaveFile(string fileName, string extention)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = fileName; 
            dialog.DefaultExt = extention;
            dialog.AddExtension = true;
            bool? dialogresult = dialog.ShowDialog();
            if (dialogresult != true)
            {
                return null;
            }
            return dialog.FileName;
        }
    }
}
