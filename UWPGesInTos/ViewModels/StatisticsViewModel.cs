using UWPGesInTos.Model;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWPGesInTos.Model;
using Windows.UI.Xaml;
using UWPGesInTos.Helpers;
using UWPGesInTos.Model.Enums;
using WinRTXamlToolkit.IO.Serialization;

namespace UWPGesInTos.ViewModels
{
    /// <summary>
    /// StatisticsViewModel
    /// </summary>
    public class StatisticsViewModel
    {
        #region DELEGATES

        private Delegates.Delegates _delegate;
        private Helper _helper = new Helper();

        #endregion DELEGATES

        #region FIELDS

        private IList<Expense> _ListProfit;
        private List<KeyValuePair<string, int>> _expenseStatistics;
        private List<KeyValuePair<string, int>> _incomeStatistics;

        #endregion FIELDS

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsViewModel"/> class.
        /// </summary>
        public StatisticsViewModel()
        {
            Delegates = new Delegates.Delegates();

            LoadStatistics();
        }

        #endregion CONSTRUCTOR

        #region PUBLIC METHODS

        /// <summary>
        /// Gets or sets the delegates.
        /// </summary>
        /// <value>
        /// The delegates.
        /// </value>
        public Delegates.Delegates Delegates
        {
            get { return _delegate; }
            set { this._delegate = value; }
        }

        /// <summary>
        /// Gets or sets the list profit.
        /// </summary>
        /// <value>
        /// The list profit.
        /// </value>
        public IList<Expense> ListProfit
        {
            get { return _ListProfit; }
            set { this._ListProfit = value; }
        }

        /// <summary>
        /// Gets or sets the expense statistics.
        /// </summary>
        /// <value>
        /// The expense statistics.
        /// </value>
        public List<KeyValuePair<string, int>> ExpenseStatistics
        {
            get { return this._expenseStatistics; }
            set { this._expenseStatistics = value; }
        }

        /// <summary>
        /// Gets or sets the income statistics.
        /// </summary>
        /// <value>
        /// The income statistics.
        /// </value>
        public List<KeyValuePair<string, int>> IncomeStatistics
        {
            get { return this._incomeStatistics; }
            set { this._incomeStatistics = value; }
        }
        
        #endregion PUBLIC METHODS

        #region PRIVATE METHODS

        /// <summary>
        /// Loads the statistics.
        /// </summary>
        private void LoadStatistics()
        {
            var conn = _helper.DefaultDatabaseConnection();

            #region EXPENSES LIST

            conn.CreateTable<Expense>();

            var listFixedExpense = conn.Table<Expense>().Where(c => c.TypeExpense == Model.Enums.eType.FIXED).ToList();
            int resultFixedExpenses = 0;

            if (listFixedExpense != null)
                for (int i = 0; i < listFixedExpense.Count; i++)
                {
                    resultFixedExpenses = resultFixedExpenses + (int)listFixedExpense[i].Amount;
                }

            var listFuelExpense = conn.Table<Expense>().Where(c => c.TypeExpense == Model.Enums.eType.FUEL).ToList();
            int resultFuelExpenses = 0;

            if (listFuelExpense != null)
                for (int i = 0; i < listFuelExpense.Count; i++)
                {
                    resultFuelExpenses = resultFuelExpenses + (int)listFuelExpense[i].Amount;
                }

            var listFoodExpense = conn.Table<Expense>().Where(c => c.TypeExpense == Model.Enums.eType.FOOD).ToList();
            int resultFoodExpenses = 0;

            if (listFoodExpense != null)
                for (int i = 0; i < listFoodExpense.Count; i++)
                {
                    resultFoodExpenses = resultFoodExpenses + (int)listFoodExpense[i].Amount;
                }

            var listExtraordinaryExpense = conn.Table<Expense>().Where(c => c.TypeExpense == Model.Enums.eType.EXTRAORDINARY).ToList();
            int resultExtraordinaryExpenses = 0;

            if (listExtraordinaryExpense != null)
                for (int i = 0; i < listExtraordinaryExpense.Count; i++)
                {
                    resultExtraordinaryExpenses = resultExtraordinaryExpenses + (int)listExtraordinaryExpense[i].Amount;
                }

            #endregion EXPENSES LIST

            var x = _helper.GetDescritpion();

            //EXPENSES
            ExpenseStatistics = new List<KeyValuePair<string, int>>();

            ExpenseStatistics.Add(new KeyValuePair<string, int>(x[0], resultFixedExpenses));
            ExpenseStatistics.Add(new KeyValuePair<string, int>(x[1], resultFuelExpenses));
            ExpenseStatistics.Add(new KeyValuePair<string, int>(x[2], resultFoodExpenses));
            ExpenseStatistics.Add(new KeyValuePair<string, int>(x[3], resultExtraordinaryExpenses));

            #region INCOMES LIST

            conn.CreateTable<Income>();

            var listFixedIncome = conn.Table<Income>().Where(c => c.TypeIncome == Model.Enums.eType.FIXED).ToList();
            int resultFixedIncomes = 0;

            if (listFixedIncome != null)
                for (int i = 0; i < listFixedIncome.Count; i++)
                {
                    resultFixedIncomes = resultFixedIncomes + (int)listFixedIncome[i].Amount;
                }

            var listFuelIncome = conn.Table<Income>().Where(c => c.TypeIncome == Model.Enums.eType.FUEL).ToList();
            int resultFuelIncomes = 0;

            if (listFuelIncome != null)
                for (int i = 0; i < listFuelIncome.Count; i++)
                {
                    resultFuelIncomes = resultFuelIncomes + (int)listFuelIncome[i].Amount;
                }

            var listExtraordinaryIncome = conn.Table<Income>().Where(c => c.TypeIncome == Model.Enums.eType.EXTRAORDINARY).ToList();
            int resultExtraordinaryIncomes = 0;

            if (listExtraordinaryIncome != null)
                for (int i = 0; i < listExtraordinaryIncome.Count; i++)
                {
                    resultExtraordinaryIncomes = resultExtraordinaryIncomes + (int)listExtraordinaryIncome[i].Amount;
                }

            var listExtraSalaryIncome = conn.Table<Income>().Where(c => c.TypeIncome == Model.Enums.eType.EXTRA_SALARY).ToList();
            int resultExtraSalaryIncomes = 0;

            if (listExtraSalaryIncome != null)
                for (int i = 0; i < listExtraSalaryIncome.Count; i++)
                {
                    resultExtraSalaryIncomes = resultExtraSalaryIncomes + (int)listExtraSalaryIncome[i].Amount;
                }

            #endregion INCOMES LIST

            //INCOMES
            IncomeStatistics = new List<KeyValuePair<string, int>>();

            IncomeStatistics.Add(new KeyValuePair<string, int>(x[0], resultFixedIncomes));
            IncomeStatistics.Add(new KeyValuePair<string, int>(x[1], resultFuelIncomes));
            IncomeStatistics.Add(new KeyValuePair<string, int>(x[3], resultExtraordinaryIncomes));
            IncomeStatistics.Add(new KeyValuePair<string, int>(x[4], resultExtraSalaryIncomes));
        }

        #endregion PRIVATE METHODS


    }
}