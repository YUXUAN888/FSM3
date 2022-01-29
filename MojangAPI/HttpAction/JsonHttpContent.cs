using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpAction
{
    public class JsonHttpContent : StringContent
    {
        private const string DefaultMediaType = "application/json";

        public JsonHttpContent(object obj)
            : this(obj, null)
        {

        }

        public JsonHttpContent(object obj, Encoding encoding)
            : this(obj, encoding, DefaultMediaType)
        {

        }

        public JsonHttpContent(object obj, Encoding encoding, string mediaType)
            : base(serialize(obj), encoding, mediaType)
        {
            
        }

        public static string serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
