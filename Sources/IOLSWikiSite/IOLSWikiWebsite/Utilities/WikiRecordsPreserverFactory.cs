using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WikiRecordsLib;

namespace IOLSWikiWebsite.Utilities
{
    public class WikiRecordsPreserverFactory
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        public static WikiRecordsPreserver CreatePreserver()
        {
            var preserver = new SQLWikiRecordsPreserver(_connectionString);
            return preserver;
        }
    }
}