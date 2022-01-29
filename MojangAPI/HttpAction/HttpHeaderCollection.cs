using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace HttpAction
{
    public class HttpHeaderCollection : IEnumerable<KeyValuePair<string, string>>
    {
        private Dictionary<string, List<string>> Headers = new Dictionary<string, List<string>>();

        public void Add(string key, string value)
        {
            if (Headers.ContainsKey(key))
                Headers[key].Add(value);
            else
            {
                List<string> values = new List<string>(1);
                values.Add(value);
                Headers[key] = values;
            }
        }

        public void AddToHeader(HttpRequestHeaders header)
        {
            foreach (var item in this)
            {
                header.Add(item.Key, item.Value);
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach (var item in Headers)
            {
                foreach (var value in item.Value)
                {
                    yield return new KeyValuePair<string, string>(item.Key, value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
