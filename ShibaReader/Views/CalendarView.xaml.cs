using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ShibaReader.Models;

namespace ShibaReader.Views
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : Window
    {
        private bool appClosed = false;
        public CalendarView()
        {
            InitializeComponent();
        }

        public CalendarView(Models.Calendar calendar)
        {
            InitializeComponent();
            fillCalendar(calendar);
        }

        public void SetCalendar(Models.Calendar calendar)
        {
            fillCalendar(calendar);
        }

        private void fillCalendar(Models.Calendar calendar)
        {
            if (calendar != null)
            {
                calNameText.Text = calendar.Name;
                datesList.ItemsSource = calendar.DateTimes;
                Title = "Calendar View (" + calendar.Name + ")";
            }
            else
            {
                calNameText.Text = "";
                datesList.ItemsSource = null;
                Title = "Calendar View";
            }
        }

        private void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        public void Exit()
        {
            this.appClosed = true;
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!appClosed)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
