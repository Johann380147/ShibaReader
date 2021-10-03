using ShibaReader.Controllers;
using ShibaReader.Events;
using ShibaReader.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShibaReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver<int>
    {
        private MainWindowController controller;
        private IDisposable cancellation;
        public AutoSysJob AutoSysJob { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            controller = new MainWindowController();
            Subscribe(UpdateImportJobEvent.getInstance());
        }

        private void JILImportBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.importJILFile();
        }

        private void CALImportBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.importCALFile();
        }

        private void SearchText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AutoSysJob = controller.searchJob(SearchText.Text);
                DataContext = AutoSysJob;
            }
        }

        public void Subscribe(UpdateImportJobEvent provider)
        {
            cancellation = provider.Subscribe(this);
        }
        public void Unsubscribe()
        {
            cancellation.Dispose();
        }
        public void OnCompleted()
        {
            progressBar.Value = 100;
        }

        public void OnError(Exception error)
        {
            // Not implemented
        }

        public void OnNext(int value)
        {
            progressBar.Value = value;
        }
    }
}
