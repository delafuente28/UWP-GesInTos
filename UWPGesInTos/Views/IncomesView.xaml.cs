﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPGesInTos.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IncomesView : Page
    {
        public IncomesView()
        {
            this.InitializeComponent();
        }

        private void IIncomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IIncomes.Visibility == Visibility.Collapsed)
            {
                IIncomes.Visibility = Visibility.Visible;
                DetailIncome.Visibility = Visibility.Collapsed;
            }
            else
            {
                IIncomes.Visibility = Visibility.Collapsed;
                DetailIncome.Visibility = Visibility.Visible;
            }
        }
    }
}
