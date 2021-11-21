using ShibaReader.Controllers;
using System.Windows;

namespace ShibaReader.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        private SettingsController controller;
        public SettingsView()
        {
            InitializeComponent();
            controller = new();
            ChosenDirectoryText.Text = ApplicationProperties.GetPropertyValue(ApplicationProperties.DefaultFolderProperty).ToString();
        }

        private void BrowseDirectoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            string chosenDir = controller.BrowseDirectories();
            if (chosenDir != null)
            {
                ChosenDirectoryText.Text = chosenDir;
            }
        }
    }
}
