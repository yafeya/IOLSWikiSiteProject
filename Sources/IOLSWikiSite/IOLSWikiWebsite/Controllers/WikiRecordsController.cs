using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using IOLSWikiWebsite.Utilities;
using WikiRecordsLib;

namespace IOLSWikiWebsite.Controllers
{
    public class WikiRecordsController : ApiController
    {
        [Route("api/WikiRecords/IsWikiNameExisted")]
        [HttpGet]
        public IHttpActionResult IsWikiNameExisted(string wikiName)
        {
            var preserver = WikiRecordsPreserverFactory.CreatePreserver();
            var isExisted = preserver.IsWikiNameExisted(wikiName);
            IHttpActionResult result;
            HttpStatusCode code = isExisted.HasValue ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            if (code == HttpStatusCode.OK)
            {
                result = new InteractiveResult(code, Request, isExisted.Value);
            }
            else
            {
                result = new InteractiveResult(code, Request, null);
            }

            return result;
        }

        [Route("api/WikiRecords/AddWikiRecord")]
        [HttpPost]
        public IHttpActionResult AddWikiRecord([FromBody]WikiRecord record)
        {
            var preserver = WikiRecordsPreserverFactory.CreatePreserver();
            var isSuccessfully = preserver.CreateWikiRecord(record);
            HttpStatusCode code = isSuccessfully ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var result = new InteractiveResult(code, Request, null);
            return result;
        }

        [Route("api/WikiRecords/GetWikiRecords")]
        [HttpGet]
        public IHttpActionResult GetWikiRecords(string input)
        {
            var keywords = input.Split(new[] { " " }, StringSplitOptions.None);
            var recordDic = new Dictionary<int, WikiRecord>();
            var preserver = WikiRecordsPreserverFactory.CreatePreserver();
            foreach (var keyword in keywords)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    IEnumerable<WikiRecord> records;
                    if (preserver.QueryWikiRecords(keyword, out records))
                    {
                        foreach (var record in records)
                        {
                            if (!recordDic.ContainsKey(record.ID))
                            {
                                recordDic.Add(record.ID, record);
                            }
                        }
                    }
                }
            }
            var result = new InteractiveResult(HttpStatusCode.OK, Request, recordDic.Values);
            return result;
        }

        [Route("api/WikiRecords/GetRecordByID")]
        [HttpGet]
        public IHttpActionResult GetRecordByID(int id)
        {
            var preserver = WikiRecordsPreserverFactory.CreatePreserver();
            WikiRecord record;
            HttpStatusCode code = preserver.TryGetWikiRecord(id, out record) ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            var result = new InteractiveResult(HttpStatusCode.OK, Request, record);
            return result;
        }

        [Route("api/WikiRecords/EditWikiRecord")]
        [HttpPost]
        public IHttpActionResult EditWikiRecord([FromBody]WikiRecord record)
        {
            HttpStatusCode code = HttpStatusCode.ExpectationFailed;
            bool isSuccessfully = false;
            if (record != null)
            {
                var preserver = WikiRecordsPreserverFactory.CreatePreserver();
                isSuccessfully = preserver.UpdateWikiRecord(record);
                code = HttpStatusCode.OK;
            }
            var result = new InteractiveResult(code, Request, isSuccessfully);
            return result;
        }
    }
}