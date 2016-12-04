using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWPGesInTos.Model;
using UWPGesInTos.Model.Enums;
using Windows.UI.Popups;

namespace UWPGesInTos.Helpers
{
    /// <summary>
    /// Helper
    /// </summary>
    public class Helper
    {
        #region Database

        /// <summary>
        /// Defaults database connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection DefaultDatabaseConnection()
        {
            string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "UWPGesInTos.sqlite");
            SQLiteConnection conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            return conn;
        }

        /// <summary>
        /// Connect with specify Database.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection DatabaseConnection(string databaseName)
        {
            string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, databaseName + ".sqlite");
            SQLiteConnection conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            return conn;
        }

        /// <summary>
        /// Totals the expenses.
        /// </summary>
        /// <returns></returns>
        public int TotalExpenses()
        {
            var conn = DefaultDatabaseConnection();
            int numberRows = conn.Table<Expense>().Count<Expense>();

            return numberRows;

        }

        /// <summary>
        /// Totals the incomes.
        /// </summary>
        /// <returns></returns>
        public int TotalIncomes()
        {
            var conn = DefaultDatabaseConnection();
            int numberRows = conn.Table<Income>().Count<Income>();

            return numberRows;

        }

        #endregion Database

        #region Add Expense/Income

        public async void UpdateProfitsExpense(decimal newAmount, string selectedMonth)
        {
            var conn = DefaultDatabaseConnection();

            conn.CreateTable<Profits>();

            var months = conn.Table<Profits>().Where(c => c.Month == selectedMonth).FirstOrDefault();
            var ano = conn.Table<Profits>().Where(c => c.Ano == DateTime.Now.Year).FirstOrDefault();

            if (months == null || ano == null)
            {
                MessageDialog dialog = new MessageDialog("Not exist the list of benefits for selected month. Would you like to create it?", "Information");

                dialog.Commands.Add(new UICommand("Yes") { Id = 0 });
                dialog.Commands.Add(new UICommand("No") { Id = 1 });

                var result = await dialog.ShowAsync();

                if (result.Id.Equals(0))
                {
                    conn.CreateTable<Profits>();

                    DateTime date = new DateTime();
                    date = DateTime.Now;

                    conn.Insert(new Profits
                    {
                        Month = date.ToString("MMMM").Substring(0, 3),
                        Ano = date.Year,
                        Amount = newAmount
                    });
                }
                else
                {
                    MessageDialog dialog2 = new MessageDialog("Not exist the list of benefits for selected month. Would you like to add the expense or income?", "Information");

                    dialog2.Commands.Add(new UICommand("Yes") { Id = 0 });
                    dialog2.Commands.Add(new UICommand("No") { Id = 1 });

                    var result2 = await dialog2.ShowAsync();

                    if (result2.Equals(0))
                    {
                        //Get profits from selected month
                        var x = conn.Table<Profits>().Where(c => c.Month == selectedMonth).FirstOrDefault();
                        conn.BeginTransaction();

                        decimal currentAmount = x.Amount;
                        //For current amount is subtracted the amount added.
                        decimal result_amount = currentAmount - newAmount;
                        x.Amount = result_amount;

                        conn.Update(x);
                        conn.Commit();
                    }

                }

            }
            else
            {
                //Get Profits from current month
                var x = conn.Table<Profits>().Where(c => c.Month == selectedMonth).FirstOrDefault();
                conn.BeginTransaction();

                decimal currentAmount = x.Amount;
                //For current amount is subtracted the amount added.
                decimal result = currentAmount - newAmount;
                x.Amount = result;

                conn.Update(x);
                conn.Commit();
            }
        }

        /// <summary>
        /// Updates the profits income.
        /// </summary>
        /// <param name="newAmount">The new amount.</param>
        /// <param name="selectedMonth">The selected month.</param>
        public async void UpdateProfitsIncome(decimal newAmount, string selectedMonth)
        {
            var conn = DefaultDatabaseConnection();

            conn.CreateTable<Profits>();

            var months = conn.Table<Profits>().Where(c => c.Month == selectedMonth).FirstOrDefault();
            var ano = conn.Table<Profits>().Where(c => c.Ano == DateTime.Now.Year).FirstOrDefault();

            if (months == null || ano == null)
            {
                MessageDialog dialog = new MessageDialog("Not exist the list of benefits for selected month. Would you like to create it?", "Information");

                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

                var result = await dialog.ShowAsync();

                if (result.Id.Equals(0))
                {
                    conn.CreateTable<Profits>();

                    DateTime date = new DateTime();
                    date = DateTime.Now;

                    conn.Insert(new Profits
                    {
                        Month = date.ToString("MMMM").Substring(0, 3),
                        Ano = date.Year,
                        Amount = newAmount
                    });
                }
                else
                {
                    MessageDialog dialog2 = new MessageDialog("Not exist the list of benefits for selected month. Would you like to add the expense or income?", "Information");

                    dialog2.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
                    dialog2.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

                    var result2 = await dialog2.ShowAsync();

                    if (result2.Equals(0))
                    {
                        //Obtener Profits del mes actual
                        var x = conn.Table<Profits>().Where(c => c.Month == selectedMonth).FirstOrDefault();
                        conn.BeginTransaction();

                        decimal currentAmount = x.Amount;
                        //Para la cantidad actual se suma la cantidad insertada.
                        decimal result_amount = currentAmount + newAmount;
                        x.Amount = result_amount;

                        conn.Update(x);
                        conn.Commit();
                        }

                    }

                }
            else
            {
                    //Obtener Profits del mes actual
                    var x = conn.Table<Profits>().Where(c => c.Month == selectedMonth).FirstOrDefault();
                    conn.BeginTransaction();

                    decimal currentAmount = x.Amount;
                    //Para la cantidad actual se resta la cantidad insertada.
                    decimal result = currentAmount + newAmount;
                    x.Amount = result;

                    conn.Update(x);
                    conn.Commit();


                }
        }

        /// <summary>
        /// News the expense.
        /// </summary>
        public void NewExpense(string concept, DateTimeOffset date, decimal amount, string month, eType expenseType, string description)
        {
            var conn = DefaultDatabaseConnection();

            if (expenseType == eType.FIXED)
            {
                conn.CreateTable<Expense>();

                conn.Insert(new Expense()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeExpense = Model.Enums.eType.FIXED,
                    Description = description
                });
            }

            if (expenseType == eType.FUEL)
            {

                conn.CreateTable<Expense>();

                conn.Insert(new Expense()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeExpense = Model.Enums.eType.FUEL,
                    Description = description
                });
            }

            if (expenseType == eType.FOOD)
            {

                conn.CreateTable<Expense>();

                conn.Insert(new Expense()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeExpense = Model.Enums.eType.FOOD,
                    Description = description
                });
            }

            if (expenseType == eType.EXTRAORDINARY)
            {

                conn.CreateTable<Expense>();

                conn.Insert(new Expense()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeExpense = Model.Enums.eType.EXTRAORDINARY,
                    Description = description
                });
            }

            //Selected month for the expense.
            string selectedMonth = date.ToString("MMMM").Substring(0, 3);
            UpdateProfitsExpense(Convert.ToDecimal(amount), selectedMonth);

        }

        /// <summary>
        /// News the income.
        /// </summary>
        public void NewIncome(string concept, DateTimeOffset date, decimal amount, string month, eType incomeType, string description)
        {
            var conn = DefaultDatabaseConnection();

            if (incomeType == eType.FIXED)
            {

                conn.CreateTable<Income>();

                conn.Insert(new Income()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeIncome = Model.Enums.eType.FIXED,
                    Description = description
                });
            }

            if (incomeType == eType.FUEL)
            {

                conn.CreateTable<Income>();

                conn.Insert(new Income()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeIncome = Model.Enums.eType.FUEL,
                    Description = description
                });
            }

            if (incomeType == eType.EXTRAORDINARY)
            {

                conn.CreateTable<Income>();

                conn.Insert(new Income()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeIncome = Model.Enums.eType.EXTRAORDINARY,
                    Description = description
                });
            }

            if (incomeType == eType.EXTRA_SALARY)
            {

                conn.CreateTable<Income>();

                conn.Insert(new Income()
                {
                    Concept = concept,
                    Date = date,
                    Amount = amount,
                    Month = month,
                    TypeIncome = Model.Enums.eType.EXTRA_SALARY,
                    Description = description
                });
            }
            
            string selectedMonth = date.ToString("MMMM").Substring(0, 3);
            UpdateProfitsIncome(Convert.ToDecimal(amount), selectedMonth);

        }

        #endregion Add Expense/Income

        #region Delete

        /// <summary>
        /// Deletes the default database.
        /// </summary>
        public void DeleteExpensesInDefaultDatabase()
        {
            var conn = DefaultDatabaseConnection();
            conn.DeleteAll<Expense>();
        }

        /// <summary>
        /// Deletes the incomes in default database.
        /// </summary>
        public void DeleteIncomesInDefaultDatabase()
        {
            var conn = DefaultDatabaseConnection();
            conn.DeleteAll<Income>();
        }

        /// <summary>
        /// Deletes the profits in default database.
        /// </summary>
        public void DeleteProfitsInDefaultDatabase()
        {
            var conn = DefaultDatabaseConnection();
            conn.DeleteAll<Profits>();
        }

        /// <summary>
        /// Deletes all default database.
        /// </summary>
        public void DeleteAllDefaultDatabase()
        {
            var conn = DefaultDatabaseConnection();

            conn.CreateTable<Profits>();
            conn.CreateTable<Expense>();
            conn.CreateTable<Income>();

            conn.DeleteAll<Profits>();
            conn.DeleteAll<Expense>();
            conn.DeleteAll<Income>();


        }

        /// <summary>
        /// Deletes the selected expense.
        /// </summary>
        public void DeleteSelectedExpense(Expense expense)
        {
            var conn = DefaultDatabaseConnection();

            conn.Delete<Expense>(expense.Id);
        }

        /// <summary>
        /// Deletes the selected income.
        /// </summary>
        /// <param name="income">The income.</param>
        public void DeleteSelectedIncome(Income income)
        {
            var conn = DefaultDatabaseConnection();

            conn.Delete<Income>(income.Id);
        }

        /// <summary>
        /// Deletes the profit identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteProfitId(int id)
        {
            var conn = DefaultDatabaseConnection();

            conn.Delete<Profits>(id);
        }

        ///// <summary>
        ///// Deletes the selected expense.
        ///// </summary>
        ///// <param name="_id">The _id.</param>
        //public void DeleteSelecExpense(int _id)
        //{
        //    DeleteSelectedExpense(_id);
        //}

        #endregion Delete

        #region Description

        public List<string> GetDescritpion()
        {
            var s_list = typeof(eType).GetFields().ToList();

            List<string> _descriptionsList = new List<string>();

            for (int i = 1; i < s_list.Count; i++)
            {
                var x = s_list[i].CustomAttributes.ToList();

                AssemblyDescriptionAttribute h = new AssemblyDescriptionAttribute(x[0].ToString());

                Char[] jjj = new Char[] { '"', '/' };

                var jj = h.Description.Split(jjj);

                _descriptionsList.Add(jj[1].ToString());               
            }

            return _descriptionsList;
        }

        public string GetSpecificDescription(eType type)
        {
            string _description = "";
            var s_list = typeof(eType).GetFields().ToList();

            for (int i = 1; i < s_list.Count; i++)
            {
                var x = s_list[i].CustomAttributes.ToList();

                var xs = s_list[i].Name;

                if (xs == type.ToString())
                {
                    AssemblyDescriptionAttribute h = new AssemblyDescriptionAttribute(x[0].ToString());

                    Char[] jjj = new Char[] { '"', '/' };

                    var jj = h.Description.Split(jjj);

                    _description = jj[1].ToString();
                }
            }

            return _description;
        }

        #endregion Description

    }
}
