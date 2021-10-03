using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShibaReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ApplicationProperties.InitializeDefaultProperties(this);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            ApplicationProperties.RetrieveProperties(this);
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            ApplicationProperties.SaveProperties(this);
        }
    }
}
