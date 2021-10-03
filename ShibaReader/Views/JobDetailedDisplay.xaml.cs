﻿using System;
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
    public partial class JobDetailed : UserControl
    {
        public JobDetailed()
        {
            InitializeComponent();
        }

        private void ListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.C)
            {
                Clipboard.SetText(((ListBox)sender).SelectedItem.ToString());
            }
        }
    }
}