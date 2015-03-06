using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiRecordsLib;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            var preserver = new SQLWikiRecordsPreserver(connectionString);

            //var record1 = new WikiRecord { Name = "wiki1", Keyword1 = "keyword1", Keyword2 = string.Empty, Keyword3 = string.Empty, Description = "Wiki1 Description", Author = "Frank Yang" };
            //var record2 = new WikiRecord { Name = "wiki2", Keyword1 = "tag1", Keyword2 = "tag2", Keyword3 = string.Empty, Description = "Tag", Author = "Frank Yang" };
            //preserver.CreateWikiRecord(record1);
            //preserver.CreateWikiRecord(record2);

            //var isExisted = preserver.IsWikiNameExisted("wiki1");
            //var recirds = preserver.QueryWikiRecords("ta");

            //var record1 = new WikiRecord { ID = 1 };
            //var record2 = new WikiRecord { ID = 2 };
            //preserver.DeleteWikiRecrod(record1);
            //preserver.DeleteWikiRecrod(record2);

            Console.ReadKey();
        }
    }
}
