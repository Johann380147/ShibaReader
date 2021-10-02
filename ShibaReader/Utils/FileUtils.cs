using Microsoft.Win32;
using System;

namespace ShibaReader.Utils
{
    static class FileUtils
    {
        public static string openFileChooser()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Jil files (*.jil)|*.jil|All files (*.*)|*.*";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            bool? result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                return fileDialog.FileName;
            }
            return null;
        }
    }
}
