﻿using BL.Servers.CR.Files.CSV_Helpers;
using BL.Servers.CR.Files.CSV_Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Servers.CR.Files.CSV_Logic
{
    internal class Event_Categories : Data
    {
        internal Event_Categories(Row _Row, DataTable _DataTable) : base(_Row, _DataTable)
        {
            Load(_Row);
        }

        // NOTE: This was generated from the event_categories.csv using gen_csv_properties.py script.

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets C s v files.
        /// </summary>
        public string CSVFiles { get; set; }

        /// <summary>
        /// Gets or sets C s v rows.
        /// </summary>
        public string CSVRows { get; set; }

        /// <summary>
        /// Gets or sets Custom names.
        /// </summary>
        public string CustomNames { get; set; }
    }
}