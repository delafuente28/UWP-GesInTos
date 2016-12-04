using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPGesInTos.Model.Enums;

namespace UWPGesInTos.Model
{
    /// <summary>
    /// Income
    /// </summary>
    public class Income
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public virtual string Month { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the concept.
        /// </summary>
        /// <value>
        /// The concept.
        /// </value>
        public virtual string Concept { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public virtual DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public virtual decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the type expense.
        /// </summary>
        /// <value>
        /// The type expense.
        /// </value>
        public virtual eType TypeIncome { get; set; }
    }
}
