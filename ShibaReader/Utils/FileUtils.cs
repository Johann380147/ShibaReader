using Microsoft.Win32;
using System;
using System.IO;

namespace ShibaReader.Utils
{
    static class FileUtils
    {
        public static string OpenFileChooser(string filter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = filter;
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            bool? result = fileDialog.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                return fileDialog.FileName;
            }
            return null;
        }
        
        public static int GetLineCount(string filePath)
        {
            try
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    int i = 0;
                    while (r.ReadLine() != null) { i++; }
                    return i;
                }
            }
            catch (DirectoryNotFoundException)
            {
                return -1;
            }
            catch (FileNotFoundException)
            {
                return -1;
            }
        }
    }
}
