using ShibaReader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Controllers
{
    class SettingsController
    {
        public string BrowseDirectories()
        {
            string chosenDir = FileUtils.OpenDirectoryChooser();
            if (chosenDir != null)
            {
                ApplicationProperties.SetProperty(ApplicationProperties.DefaultFolderProperty, chosenDir);
            }
            return chosenDir;    
        }
    }
}
