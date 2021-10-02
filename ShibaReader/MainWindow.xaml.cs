using System.Windows;
using ShibaReader.Utils;
using ShibaReader.Processors;

namespace ShibaReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImportBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = FileUtils.openFileChooser();
            if (fileName == null) return;

            JILProcessor jilProc = new JILProcessor(fileName);
            jilProc.processJILFile();
        }

        

    }
}
