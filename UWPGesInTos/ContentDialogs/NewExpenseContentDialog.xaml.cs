using UWPGesInTos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWPGesInTos.Helpers;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPGesInTos.ContentDialogs
{
    /// <summary>
    /// NewExpenseContentDialog
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class NewExpenseContentDialog : ContentDialog
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NewExpenseContentDialog"/> class.
        /// </summary>
        public NewExpenseContentDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Contents the dialog_ primary button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        public void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Contents the dialog_ secondary button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Handles the KeyDown event of the txt_Amount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        public async void txt_Amount_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            //e.Handled = !Char.IsDigit((char)e.Key);

            //if (e.Handled == true)
            //{
            //    MessageDialog s = new MessageDialog("Only numbers is allowed.");

            //    var result = await s.ShowAsync();
            //}
        }
    }
}
