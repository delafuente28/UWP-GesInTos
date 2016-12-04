using UWPGesInTos.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPGesInTos.Model;
using System.ComponentModel;
using UWPGesInTos.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using UWPGesInTos.Model.Enums;
using System.Collections.ObjectModel;

namespace UWPGesInTos.ViewModels
{
    /// <summary>
    /// IncomesViewModel
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class IncomesViewModel : INotifyPropertyChanged
    {
        #region

        private Delegates.Delegates _delegates;

        /// <summary>
        /// Gets the delete selected income command.
        /// </summary>
        /// <value>
        /// The delete selected income command.
        /// </value>
        public DelegateCommand DeleteSelectedIncomeCommand { get; private set; }
        Helpers.Helper _helper = new Helpers.Helper();

        #endregion

        #region PROPERTIES

        private ObservableCollection<Income> _iIncomes;
        private Income _selectedIncome;
        private string _descriptionSelectedIncome;
        private string _searching;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion PROPERTIES

        #region PUBLIC PROPERTIES

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
        public ObservableCollection<Income> IIncomes
        {
            get
            {
                return _iIncomes;
            }

            set
            {
                _iIncomes = value;
                OnPropertyChanged("IIncomes");
            }
        }

        /// <summary>
        /// Gets or sets the selected income.
        /// </summary>
        /// <value>
        /// The selected income.
        /// </value>
        public Income SelectedIncome
        {
            get
            {
                return _selectedIncome;
            }

            set
            {
                _selectedIncome = value;
                DescriptionSelectedIncome = _helper.GetSpecificDescription(SelectedIncome.TypeIncome);
                OnPropertyChanged("SelectedIncome");
            }
        }

        /// <summary>
        /// Gets or sets the description selected income.
        /// </summary>
        /// <value>
        /// The description selected income.
        /// </value>
        public string DescriptionSelectedIncome
        {
            get { return _descriptionSelectedIncome; }
            set
            {
                _descriptionSelectedIncome = value;
                OnPropertyChanged("DescriptionSelectedIncome");
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
                IncomesSearching();
            }
        }

        #endregion PUBLIC PROPERTIES

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="IncomesViewModel"/> class.
        /// </summary>
        public IncomesViewModel()
        {
            OnInitialazing();
        }

        #endregion CONSTRUCTOR

        #region PRIVATE METHODS

        /// <summary>
        /// Gets the i fijos.
        /// </summary>
        private void GetIncomes()
        {
            var conn = _helper.DefaultDatabaseConnection();

            conn.CreateTable<Income>();

            var IncomesList = conn.Table<Income>().OrderByDescending(t => t.Date).ToList();
            IIncomes = new ObservableCollection<Income>();

            if (IncomesList != null)
                foreach (Income item in IncomesList)
                {
                    IIncomes.Add(item);
                }
        }

        /// <summary>
        /// Deletes the income.
        /// </summary>
        private async void DeleteIncome()
        {
            Helpers.Helper helper = new Helpers.Helper();

            MessageDialog deleteDialog = new MessageDialog("The income is going to deleted. Are your sure?", "Alert");

            deleteDialog.Commands.Add(new UICommand("Yes") { Id = 0 });
            deleteDialog.Commands.Add(new UICommand("No") { Id = 1 });

            var deleteResult = await deleteDialog.ShowAsync();

            if (deleteResult.Id.Equals(0))
            {
                helper.DeleteSelectedIncome(SelectedIncome);
                _helper.UpdateProfitsExpense(SelectedIncome.Amount, SelectedIncome.Month);
                IncomeView();
            }
        }

        private void IncomeView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(IncomesView));
        }

        private void IncomesSearching()
        {
            var x = IIncomes.Where(item => (item.Concept.ToUpperInvariant().Contains(Searching.ToUpperInvariant())) ||
                                         (item.TypeIncome.ToString().ToUpperInvariant().Contains(Searching.ToUpperInvariant())) ||
                                         (item.Month.ToUpperInvariant().Contains(Searching.ToUpperInvariant()))
                                         ).ToList();

            IIncomes.Clear();
            foreach (var item in x)
                IIncomes.Add(item);

            if (string.IsNullOrEmpty(Searching))
                GetIncomes();
        }

        #endregion PRIVATE METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// Called when [initialazing].
        /// </summary>
        public void OnInitialazing()
        {
            Delegates = new Delegates.Delegates();
            GetIncomes();
            DeleteSelectedIncomeCommand = new DelegateCommand(DeleteIncome);
        }

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