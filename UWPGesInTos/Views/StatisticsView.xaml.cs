using System;
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

namespace UWPGesInTos.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class StatisticsView : Page
    {
        public StatisticsView()
        {
            this.InitializeComponent();
            ExpenseSelected.IsSelected = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ExpenseSelected.IsSelected)
            {
                ExpensesPieSeries.Visibility = Visibility.Visible;
                IncomesPieSeries.Visibility = Visibility.Collapsed;
            }
            else
            {
                ExpensesPieSeries.Visibility = Visibility.Collapsed;
                IncomesPieSeries.Visibility = Visibility.Visible;
            }
        }
    }
}
