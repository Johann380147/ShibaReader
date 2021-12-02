using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace ShibaReader.Utils
{
    static class FileUtils
    {
        public static FileInfo[] GetMatchingFiles(string directory, string fileNamePattern)
        {
            try
            {
                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(directory);
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(fileNamePattern);
                return filesInDir;
            } catch (DirectoryNotFoundException)
            {
                return null;
            }
        }

        public static string OpenDirectoryChooser()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyDocuments;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }
        public static string OpenFileChooser(string filter)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
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
