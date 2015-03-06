using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IOLSWikiWebsite.Utilities
{
    public class InteractiveResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private HttpStatusCode _status;
        private object _content;

        public InteractiveResult(HttpStatusCode status, HttpRequestMessage request, object content)
        {
            _status = status;
            _request = request;
            _content = content;
        }

        #region IHttpActionResult Members

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var responseMessage = _request.CreateResponse(_status);
            if (_content != null)
            {
                responseMessage.Content = new ObjectContent(_content.GetType(), _content, new JsonMediaTypeFormatter());
            }

            responseMessage.RequestMessage = _request;
            var task = Task.FromResult(responseMessage);
            return task;
        }

        #endregion
    }
}