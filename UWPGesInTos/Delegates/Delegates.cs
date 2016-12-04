using Prism.Commands;
using System;
using System.IO;
using System.Threading.Tasks;
using UWPGesInTos.ContentDialogs;
using UWPGesInTos.Model;
using UWPGesInTos.Views;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPGesInTos.Helpers;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace UWPGesInTos.Delegates
{
    /// <summary>
    /// Delegates
    /// </summary>
    public class Delegates
    {
        #region HELPER

        private readonly Helper _helper = new Helper();

        #endregion HELPER

        #region DELEGATES

        /// <summary>
        /// Gets the expenses command.
        /// </summary>
        /// <value>
        /// The expenses command.
        /// </value>
        public DelegateCommand ExpensesCommand { get; private set; }
        /// <summary>
        /// Gets the incomes command.
        /// </summary>
        /// <value>
        /// The incomes command.
        /// </value>
        public DelegateCommand IncomesCommand { get; private set; }
        /// <summary>
        /// Gets the profits command.
        /// </summary>
        /// <value>
        /// The profits command.
        /// </value>
        public DelegateCommand ProfitsCommand { get; private set; }
        /// <summary>
        /// Gets the statistics command.
        /// </summary>
        /// <value>
        /// The statistics command.
        /// </value>
        public DelegateCommand StatisticsCommand { get; private set; }
        /// <summary>
        /// Gets the expenses popup command.
        /// </summary>
        /// <value>
        /// The expenses popup command.
        /// </value>
        public DelegateCommand ExpensesPopupCommand { get; private set; }
        /// <summary>
        /// Gets the incomes popup command.
        /// </summary>
        /// <value>
        /// The incomes popup command.
        /// </value>
        public DelegateCommand IncomesPopupCommand { get; private set; }
        /// <summary>
        /// Gets the new expense command.
        /// </summary>
        /// <value>
        /// The new expense command.
        /// </value>
        public DelegateCommand NewExpenseCommand { get; private set; }
        /// <summary>
        /// Gets the configuration command.
        /// </summary>
        /// <value>
        /// The configuration command.
        /// </value>
        public DelegateCommand ConfigCommand { get; private set; }
        /// <summary>
        /// Gets the back to expense command.
        /// </summary>
        /// <value>
        /// The back to expense command.
        /// </value>
        public DelegateCommand BackToExpenseCommand { get; private set; }
        /// <summary>
        /// Gets the back to income command.
        /// </summary>
        /// <value>
        /// The back to income command.
        /// </value>
        public DelegateCommand BackToIncomeCommand { get; private set; }
        /// <summary>
        /// Gets the delete database command.
        /// </summary>
        /// <value>
        /// The delete database command.
        /// </value>
        public DelegateCommand DeleteDatabaseCommand { get; private set; }
        /// <summary>
        /// Gets the main view command.
        /// </summary>
        /// <value>
        /// The main view command.
        /// </value>
        public DelegateCommand MainViewCommand { get; private set; }

        #endregion DELEGATES

        #region PRIVATE METHODS

        /// <summary>
        /// Mains the view.
        /// </summary>
        private void MainView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Expenseses the view.
        /// </summary>
        private void ExpensesView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ExpensesView));
        }

        /// <summary>
        /// Incomeses the view.
        /// </summary>
        private void IncomesView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(IncomesView));
        }

        /// <summary>
        /// Profitses the view.
        /// </summary>
        private void ProfitsView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ProfitsView));
        }

        /// <summary>
        /// Statisticses the view.
        /// </summary>
        private void StatisticsView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(StatisticsView));
        }

        /// <summary>
        /// Configurations the view.
        /// </summary>
        private void ConfigView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ConfigView));
        }

        /// <summary>
        /// Expenseses the popup.
        /// </summary>
        private async void ExpensesPopup()
        {

            var dialog = new NewExpenseContentDialog()
            {
            };


            var result = await dialog.ShowAsync();

        }

        /// <summary>
        /// Incomes the popup.
        /// </summary>
        private async void IncomesPopup()
        {

            var dialog = new NewIncomeContentDialog()
            {
            };


            var result = await dialog.ShowAsync();

        }

        /// <summary>
        /// Deletes the database.
        /// </summary>
        private async void DeleteDatabase()
        {
            MessageDialog deleteDialog = new MessageDialog("The database is going to deleted. Are your sure?", "Alert");
            MessageDialog backupDialog = new MessageDialog("Would you like to make a backup of the database?", "Alert");

            deleteDialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            deleteDialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            var deleteResult = await deleteDialog.ShowAsync();

            if (deleteResult.Id.Equals(0))
            {
                backupDialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
                backupDialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

                var backupResult = await backupDialog.ShowAsync();

                if (backupResult.Id.Equals(0))
                {
                    BackupDatabase();
                }

                _helper.DeleteAllDefaultDatabase();
            }
        }

        /// <summary>
        /// Exports the database.
        /// </summary>
        private async void BackupDatabase()
        {
            var conn = _helper.DefaultDatabaseConnection();

            var x = conn.CreateDatabaseBackup(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT());

            //string destFile = @"C:\\Prueba\\";

            //StorageFolder folder = ApplicationData.Current.LocalFolder;

            ////StorageFile file = await folder.CreateFileAsync("UWPGesInTos.sqlite");

            //StorageFile file = await folder.GetFileAsync("UWPGesInTos.sqlite");


            //StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(destFile);

            //await file.CopyAsync(localFolder);


        }

        #endregion PRIVATE METHODS

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="Delegates"/> class.
        /// </summary>
        public Delegates()
        {
            MainViewCommand = new DelegateCommand(MainView);
            ExpensesCommand = new DelegateCommand(ExpensesView);
            IncomesCommand = new DelegateCommand(IncomesView);
            ProfitsCommand = new DelegateCommand(ProfitsView);
            StatisticsCommand = new DelegateCommand(StatisticsView);
            ExpensesPopupCommand = new DelegateCommand(ExpensesPopup);
            IncomesPopupCommand = new DelegateCommand(IncomesPopup);
            ConfigCommand = new DelegateCommand(ConfigView);
            BackToExpenseCommand = new DelegateCommand(ExpensesView);
            BackToIncomeCommand = new DelegateCommand(IncomesView);
            DeleteDatabaseCommand = new DelegateCommand(DeleteDatabase);
        }

        #endregion CONSTRUCTOR

    }
}
