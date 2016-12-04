using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using UWPGesInTos.Base;
using UWPGesInTos.Helpers;
using UWPGesInTos.Model.Enums;
using UWPGesInTos.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPGesInTos.ContentDialogs
{
    /// <summary>
    /// NewExpenseViewModel
    /// </summary>
    /// <seealso cref="UWPGesInTos.Base.ViewModelBase" />
    public class NewExpenseContentDialogViewModel : ViewModelBase
    {
        #region HELPER

        Helper _helper = new Helper();

        #endregion HELPER

        #region ATTRIBUTES

        private DateTimeOffset _date;
        private string _description;
        private string _month;
        private int _amount;
        private string _concept;
        private ObservableCollection<string> _expenseTypes;
        private string _selectedTypeExpense;

        #endregion ATTRIBUTES

        #region PROPERTIES

        /// <summary>
        /// Gets the new item.
        /// </summary>
        /// <value>
        /// The new item.
        /// </value>
        public DelegateCommand<ContentDialog> NewItemCommand { get; private set; }

        /// <summary>
        /// Gets the close command.
        /// </summary>
        /// <value>
        /// The close command.
        /// </value>
        public DelegateCommand<ContentDialog> CloseCommand { get; private set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTimeOffset Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public string Month
        {
            get
            {
                return _month;
            }

            set
            {
                _month = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public int Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;
            }
        }

        /// <summary>
        /// Gets or sets the concept.
        /// </summary>
        /// <value>
        /// The concept.
        /// </value>
        public string Concept
        {
            get
            {
                return _concept;
            }

            set
            {
                _concept = value;
            }
        }

        /// <summary>
        /// Gets or sets the expense types.
        /// </summary>
        /// <value>
        /// The expense types.
        /// </value>
        public ObservableCollection<string> ExpenseTypes
        {
            get { return _expenseTypes; }
            set { _expenseTypes = value; }
        }

        /// <summary>
        /// Gets or sets the type expense.
        /// </summary>
        /// <value>
        /// The type expense.
        /// </value>
        public string SelectedTypeExpense
        {
            get
            {
                return _selectedTypeExpense;
            }

            set
            {
                _selectedTypeExpense = value;
            }
        }

        #endregion PROPERTIES

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="NewExpenseContentDialogViewModel"/> class.
        /// </summary>
        public NewExpenseContentDialogViewModel()
        {
            NewItemCommand = new DelegateCommand<ContentDialog>(NewItem);
            CloseCommand = new DelegateCommand<ContentDialog>(Close);
            FillExpenseTypes();
            Date = DateTime.Today;
        }

        #endregion CONSTRUCTOR

        #region PRIVATE METHODS

        private void FillExpenseTypes()
        {
            ExpenseTypes = new ObservableCollection<string>();

            var _descriptions = _helper.GetDescritpion();

            foreach (var item in _descriptions)
            {
                if(item != "Extra salary")
                {
                    ExpenseTypes.Add(item);
                }
            }
        }

        private void NewItem(ContentDialog contentDialog)
        {
            Helper _helper = new Helper();

            decimal cantidad = Convert.ToDecimal(Amount);
            string month = Date.ToString("MMM");
            string _description = Description;

            var selectedTypeEnum = (eType)Enum.Parse(typeof(eType), SelectedTypeExpense.ToUpper());
            
            _helper.NewExpense(Concept, Date, Amount, month, selectedTypeEnum, Description);
            Close(contentDialog);
            ExpensesView();
        }

        private void ExpensesView()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ExpensesView));
        }

        private void Close(ContentDialog contentDialog)
        {
            contentDialog.Hide();
        }

        #endregion PRIVATE METHODS
        
    }
}
