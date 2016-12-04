using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UWPGesInTos.Model.Enums
{
    /// <summary>
    /// eType
    /// </summary>
    public enum eType
    {
        /// <summary>
        /// The fixed 
        /// </summary>
        [XmlEnum("Fixed")]
        FIXED = 0,
        /// <summary>
        /// The fuel 
        /// </summary>
        [XmlEnum("Fuel")]
        FUEL = 1,
        /// <summary>
        /// The food 
        /// </summary>
        [XmlEnum("Food")]
        FOOD = 2,
        /// <summary>
        /// The extraordinary 
        /// </summary>
        [XmlEnum("Extraordinary")]
        EXTRAORDINARY = 3,
        /// <summary>
        /// The extr a_ salary
        /// </summary>
        [XmlEnum("Extra salary")]
        EXTRA_SALARY = 4

    }
}
