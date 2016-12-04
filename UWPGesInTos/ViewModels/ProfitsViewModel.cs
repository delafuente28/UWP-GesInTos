using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPGesInTos.Model;
using System.IO;
using UWPGesInTos.Delegates;
using SQLite.Net;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UWPGesInTos.ViewModels
{
    /// <summary>
    /// ProfitsViewModel
    /// </summary>
    public class ProfitsViewModel : INotifyPropertyChanged
    {
        #region DELEGATES

        private Delegates.Delegates _delegate;
        Helpers.Helper helper = new Helpers.Helper();

        #endregion DELEGATES

        #region FIELDS

        private ObservableCollection<Profits> _ListProfit;
        private ObservableCollection<KeyValuePair<string, int>> _profits;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion FIELDS

        #region PROPERTIES

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
        /// Gets or sets the profits.
        /// </summary>
        /// <value>
        /// The profits.
        /// </value>
        public ObservableCollection<KeyValuePair<string, int>> Profits
        {
            get { return _profits; }
            set
            {
                this._profits = value;
                OnPropertyChanged("Profits");
            }
        }

        /// <summary>
        /// Gets or sets the list profit.
        /// </summary>
        /// <value>
        /// The list profit.
        /// </value>
        public ObservableCollection<Profits> ListProfit
        {
            get { return _ListProfit; }
            set
            {
                this._ListProfit = value;
                OnPropertyChanged("ListProfit");
            }
        }

        #endregion PROPERTIES

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfitsViewModel"/> class.
        /// </summary>
        public ProfitsViewModel()
        {
            Initialize();
        }

        #endregion CONSTRUCTOR

        #region PRIVATE METHODS

        /// <summary>
        /// Loads the column chart data.
        /// </summary>
        private void LoadColumnChartData()
        {
            Profits = new ObservableCollection<KeyValuePair<string, int>>();

            foreach (var item in ListProfit)
            {
                Profits.Add(new KeyValuePair<string, int>(item.Month, (int)item.Amount));
            }

        }

        /// <summary>
        /// Gets the actual month amount.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        private void GetActualMonthAmount()
        {
            var conn = helper.DefaultDatabaseConnection();

            //conn.DeleteAll<Profits>();

            conn.CreateTable<Profits>();

            var x = (conn.Table<Profits>().Where(t => t.Ano == DateTime.Now.Year));

            if (x.Count() != 12)
            {
                #region List of months for inserting

                conn.Insert(new Profits
                {
                    Month = "Jan",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Feb",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Mar",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Apr",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "May",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Jun",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Jul",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Aug",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Sep",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Oct",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Nov",
                    Ano = 2016,
                    Amount = 0
                });

                conn.Insert(new Profits
                {
                    Month = "Dec",
                    Ano = 2016,
                    Amount = 0
                });

                #endregion List of months for inserting
            }
            
            var _listProfit = conn.Table<Profits>().ToList();
            ListProfit = new ObservableCollection<Profits>();

            foreach (var item in _listProfit)
            {
                ListProfit.Add(item);
            }

        }

        #endregion PRIVATE METHODS

        #region PUBLIC METHODS

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            Delegates = new Delegates.Delegates();
            GetActualMonthAmount();
            LoadColumnChartData();
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
