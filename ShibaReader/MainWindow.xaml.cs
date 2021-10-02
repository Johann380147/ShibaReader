using ShibaReader.Controllers;
using System.Windows;

namespace ShibaReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowController controller;
        public MainWindow()
        {
            InitializeComponent();
            controller = new MainWindowController();

        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.importJILFile();
        }

        

    }
}
