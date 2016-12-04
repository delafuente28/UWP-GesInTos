using UWPGesInTos.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UWPGesInTos.ContentDialogs;
using UWPGesInTos.Model.Enums;
using UWPGesInTos.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Prism.Events;
using UWPGesInTos.Events;
using Windows.UI.Popups;
using MyToolkit.Collections;

namespace UWPGesInTos.ViewModels
{
    /// <summary>
    /// ExpensesViewModel
    /// </summary>
    public class ExpensesViewModel : INotifyPropertyChanged
    {
        #region DELEGATES

        private Delegates.Delegates _delegates;
        Helpers.Helper _helper = new Helpers.Helper();

        /// <summary>
        /// Gets the delete selected expense command.
        /// </summary>
        /// <value>
        /// The delete selected expense command.
        /// </value>
        public DelegateCommand DeleteSelectedExpenseCommand { get; private set; }
        
        #endregion DELEGATES

        #region FIELDS

        private ObservableCollection<Expense> _gFijos;
        private string _searching;

        /// <summary>
        /// Tiene lugar cuando cambia un valor de propiedad.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private Expense _selectedItem;
        private string _descriptionSelectedExpense;

        #endregion FIELDS

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpensesViewModel"/> class.
        /// </summary>
        public ExpensesViewModel()
        {
            OnInitialazing();
        }

        #endregion CONSTRUCTOR

        #region PUBLIC PROPERTIES
        
        /// <summary>
        /// Gets or sets the selected gfijos.
        /// </summary>
        /// <value>
        /// The selected gfijos.
        /// </value>
        public Expense SelectedGfijos
        {
            get { return this._selectedItem; }
            set
            {
                this._selectedItem = value;
                DescriptionSelectedExpense = _helper.GetSpecificDescription(SelectedGfijos.TypeExpense);
                OnPropertyChanged("SelectedGfijos");
            }
        }

        /// <summary>
        /// Called when [initialazing].
        /// </summary>
        public void OnInitialazing()
        {
            GetGFijos();
            Delegates = new Delegates.Delegates();
            DeleteSelectedExpenseCommand = new DelegateCommand(DeleteExpense);
        }

        /// <summary>
        /// Gets or sets the delegates.
        /// </summary>
        /// <value>
        /// The delegates.
        /// </value>
        public Delegates.Delegates Delegates
        {
            get { return _delegates; }
            set { this._delegates = value; }
        }

        /// <summary>
        /// Gets or sets the g fijos.
        /// </summary>
        /// <value>
        /// The g fijos.
        /// </value>
        public ObservableCollection<Expense> GFijos
        {
            get
            {
                return _gFijos;
            }

            set
            {
                _gFijos = value;
                OnPropertyChanged("GFijos");
            }
        }

        /// <summary>
        /// Gets or sets the searching.
        /// </summary>
        /// <value>
        /// The searching.
        /// </value>
        public string Searching
        {
            get { return _searching; }
            set
            {
                _searching = value;
                ExpensesSearching();
            }
        }

        /// <summary>
        /// Gets or sets the description selected expense.
        /// </summary>
        /// <value>
        /// The description selected expense.
        /// </value>
        public string DescriptionSelectedExpense
        {
            get { return _descriptionSelectedExpense; }
            set
            {
                _descriptionSelectedExpense = value;
                OnPropertyChanged("DescriptionSelectedExpense");
            }
        }
        
        #endregion PUBLIC PROPERTIES

        #region PRIVATE METHODS

        /// <summary>
        /// Gets the g fijos.
        /// </summary>
        private void GetGFijos()
        {
            var conn = _helper.DefaultDatabaseConnection();
            conn.CreateTable<Expense>();

            var listGFijos = conn.Table<Expense>().OrderByDescending(t => t.Date).ToList();

            GFijos = new ObservableCollection<Expense>();

            if (listGFijos != null)
                foreach (Expense item in listGFijos)
                {
                    GFijos.Add(item);
                }
        }

        private async void DeleteExpense()
        {
            Helpers.Helper helper = new Helpers.Helper();

            MessageDialog deleteDialog = new MessageDialog("The expense is going to deleted. Are your sure?", "Alert");

            deleteDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            deleteDialog.Commands.Add(new UICommand("No") { Id = 1 });

            var deleteResult = await deleteDialog.ShowAsync();

            if (deleteResult.Id.Equals(0))
            {
                helper.DeleteSelectedExpense(SelectedGfijos);
                _helper.UpdateProfitsIncome(SelectedGfijos.Amount, SelectedGfijos.Month);
                ExpensesView();
            }
        }

        private void ExpensesView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ExpensesView));
        }

        private void ExpensesSearching()
        {
            var x = GFijos.Where(item => (item.Concept.ToUpperInvariant().Contains(Searching.ToUpperInvariant())) ||
                                         (item.TypeExpense.ToString().ToUpperInvariant().Contains(Searching.ToUpperInvariant())) ||
                                         (item.Month.ToUpperInvariant().Contains(Searching.ToUpperInvariant()))
                                         ).ToList();

            GFijos.Clear();
            foreach (var item in x)
                GFijos.Add(item);

            if(string.IsNullOrEmpty(Searching))
                GetGFijos();
        }

        #endregion PRIVATE METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion PUBLIC METHODS
    }
}
