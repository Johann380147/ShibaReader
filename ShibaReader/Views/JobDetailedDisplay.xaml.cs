using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShibaReader.Views
{
    /// <summary>
    /// Interaction logic for JobDetailed.xaml
    /// </summary>
    public partial class JobDetailedDisplay : UserControl
    {
        public static readonly RoutedEvent LinkedJobClickEvent =
        EventManager.RegisterRoutedEvent("LinkedJobClickEvent", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(Button));

        public static readonly RoutedEvent ExcludeCalClickEvent =
        EventManager.RegisterRoutedEvent("ExcludeCalClickEvent", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(Button));

        public static readonly RoutedEvent ScheduleClickEvent =
        EventManager.RegisterRoutedEvent("ScheduleClickEvent", RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(Button));

        public JobDetailedDisplay()
        {
            InitializeComponent();
        }

        private void ListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.C)
            {
                Clipboard.SetText(((ListBox)sender).SelectedItem.ToString());
            }
            else
            {
                e.Handled = false;
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

        public event RoutedEventHandler LinkedJobBtnClicked
        {
            add { AddHandler(LinkedJobClickEvent, value); }
            remove { RemoveHandler(LinkedJobClickEvent, value); }
        }

        public event RoutedEventHandler ExcludeCalBtnClicked
        {
            add { AddHandler(ExcludeCalClickEvent, value); }
            remove { RemoveHandler(ExcludeCalClickEvent, value); }
        }

        public event RoutedEventHandler ScheduleBtnClicked
        {
            add { AddHandler(ScheduleClickEvent, value); }
            remove { RemoveHandler(ScheduleClickEvent, value); }
        }

        private void LinkedJobBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(JobDetailedDisplay.LinkedJobClickEvent, e.Source));
        }

        private void ExcludeCalBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(JobDetailedDisplay.ExcludeCalClickEvent, e.Source));
        }
        
        private void ScheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(JobDetailedDisplay.ScheduleClickEvent, e.Source));
        }
    }
}
