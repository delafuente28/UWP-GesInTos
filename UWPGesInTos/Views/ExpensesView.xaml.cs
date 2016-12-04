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
    public sealed partial class ExpensesView : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpensesView"/> class.
        /// </summary>
        public ExpensesView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the Gfijos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void Gfijos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(GridExpense.Visibility == Visibility.Collapsed)
            {
                GridExpense.Visibility = Visibility.Visible;
                DetailExpense.Visibility = Visibility.Collapsed;
            }
            else
            {
                GridExpense.Visibility = Visibility.Collapsed;
                DetailExpense.Visibility = Visibility.Visible;
            }
        }

        private void Gfijos_Holding(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}
