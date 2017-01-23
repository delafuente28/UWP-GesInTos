using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPGesInTos.Helpers;

namespace UWPGesInTos.ViewModels
{
    /// <summary>
    /// ConfigViewModel
    /// </summary>
    public class ConfigViewModel
    {

        #region FIELDS

        private string _databaseName;

        #endregion FIELDS

        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        #endregion PUBLIC PROPERTIES

        #region DELEGATES

        private Helper _helper = new Helper();
        private Delegates.Delegates _delegates;

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
        /// Gets the change database command.
        /// </summary>
        /// <value>
        /// The change database command.
        /// </value>
        public DelegateCommand<string> ChangeDatabaseCommand { get; private set; }

        #endregion DELEGATES

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigViewModel"/> class.
        /// </summary>
        public ConfigViewModel()
        {
            _delegates = new Delegates.Delegates();
            GetDatabaseInformation();
            ChangeDatabaseCommand = new DelegateCommand<string>(ChangeDatabase);
        }

        #endregion CONSTRUCTOR

        #region PRIVATE METHODS

        private void GetDatabaseInformation()
        {
            var connection = _helper.DefaultDatabaseConnection();

            string _information = connection.DatabasePath;
            var d = _information.Split('\\');

            DatabaseName = d.LastOrDefault();
        }

        private void ChangeDatabase(string database)
        {
            var connection = _helper.DatabaseConnection(database);
        }

        #endregion PRIVATE METHODS

    }
}
