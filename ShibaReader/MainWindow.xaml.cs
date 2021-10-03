using ShibaReader.Controllers;
using ShibaReader.Events;
using ShibaReader.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShibaReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver<int>, INotifyPropertyChanged
    {
        private MainWindowController controller;
        private IDisposable cancellation;

        public event PropertyChangedEventHandler PropertyChanged;

        private string prevSearchText = "";
        private int numOfJobs = 0;
        private int numOfCalendars = 0;
        private AutoSysJob autoSysJob = null;
        private string currentPositionText = "0 of 0";
        public AutoSysJob AutoSysJob
        {
            get => autoSysJob; 
            set
            {
                autoSysJob = value;
                NotifyPropertyChanged();
            }
        }

        public int NumOfJobs 
        { 
            get => numOfJobs;
            set
            {
                numOfJobs = value;
                NotifyPropertyChanged();
            }
        }

        public int NumOfCalendars
        {
            get => numOfCalendars;
            set
            {
                numOfCalendars = value;
                NotifyPropertyChanged();
            }
        }

        public string CurrentPositionText
        {
            get => currentPositionText;
            set
            {
                currentPositionText = value;
                NotifyPropertyChanged();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            controller = new MainWindowController();
            Subscribe(UpdateImportJobEvent.getInstance());
            NumOfJobs = controller.GetJobCount();
            NumOfCalendars = controller.GetCalendarCount();
        }

        private void JILImportBtn_Click(object sender, RoutedEventArgs e)
        {
            NumOfJobs = controller.ImportJILFile();
            UpdateUI();
        }

        private void CALImportBtn_Click(object sender, RoutedEventArgs e)
        {
            NumOfCalendars = controller.ImportCALFile();
        }

        private void PrevJobBtn_Click(object sender, RoutedEventArgs e)
        {
            AutoSysJob = controller.GetPrevJob();
            UpdateUI();
        }

        private void NextJobBtn_Click(object sender, RoutedEventArgs e)
        {
            AutoSysJob = controller.GetNextJob();
            UpdateUI();
        }

        private void SearchText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (prevSearchText == SearchText.Text && !controller.IsLastElement)
                {
                    NextJobBtn_Click(null, null);
                }
                else
                {
                    AutoSysJob = controller.SearchJob(SearchText.Text);
                    UpdateUI();
                }
                prevSearchText = SearchText.Text;
            }
        }

        private void JobDetailed_LinkedJobBtnClicked(object sender, RoutedEventArgs e)
        {
            Button source = (Button)e.OriginalSource;
            AutoSysJob = controller.SearchJob(source.Content.ToString());
            UpdateUI();
        }

        private void JobDetailedDisplay_ScheduleBtnClicked(object sender, RoutedEventArgs e)
        {
            Button source = (Button)e.OriginalSource;
            controller.GetCalendar(source.Content.ToString());
        }

        private void JobDetailedDisplay_ExcludeCalBtnClicked(object sender, RoutedEventArgs e)
        {
            Button source = (Button)e.OriginalSource;
            controller.GetCalendar(source.Content.ToString());
        }

        private void UpdateUI()
        {
            EnableDisablePrevNextJobBtns();
            UpdateSearchPostionText();
        }

        private void UpdateSearchPostionText()
        {
            if (controller.MatchedJobsCount > 0)
                CurrentPositionText = controller.CurrentIndex + 1 + " of " + controller.MatchedJobsCount;
            else
                CurrentPositionText = controller.CurrentIndex + " of " + controller.MatchedJobsCount;
        }

        private void EnableDisablePrevNextJobBtns()
        {
            PrevJobBtn.IsEnabled = controller.IsFirstElement ? false : true;
            NextJobBtn.IsEnabled = controller.IsLastElement ? false : true;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
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

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            controller.Dispose();
        }
    }
}
