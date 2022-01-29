using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpAction
{
    public class HttpAction<T> : HttpRequestMessage
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public HttpQueryCollection Queries { get; set; }
        public HttpHeaderCollection RequestHeaders { get; set; }

        public Func<HttpAction<T>, string> CheckValidation { get; set; }

        public Func<HttpResponseMessage, Task<T>> ResponseHandler { get; set; }
            = HttpResponseHandlers.GetDefaultResponseHandler<T>();

        public Func<HttpResponseMessage, Exception, Task<T>> ErrorHandler { get; set; }
            = null;

        public virtual Uri CreateUri()
        {
            if (this.Host == null)
                throw new ArgumentNullException("Host");

            var ubuilder = new UriBuilder(this.Host);
            ubuilder.Path = this.Path;
            ubuilder.Query = this.Queries?.BuildQuery();
            return ubuilder.Uri;
        }
    }
}
