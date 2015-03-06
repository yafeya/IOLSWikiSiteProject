using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiRecordsLib
{
    public abstract class WikiRecordsPreserver
    {
        public abstract bool CreateWikiRecord(WikiRecord record);
        public abstract bool? IsWikiNameExisted(string name);
        public abstract bool UpdateWikiRecord(WikiRecord record);
        public abstract bool DeleteWikiRecrod(WikiRecord record);
        public abstract bool QueryWikiRecords(string keyword, out IEnumerable<WikiRecord> records);
        public abstract bool TryGetWikiRecord(int id, out WikiRecord record);
    }
}
