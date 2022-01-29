using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace HttpAction
{
    public class HttpQueryCollection : IEnumerable<string>
    {
        private Dictionary<string, List<string>> Queries = new Dictionary<string, List<string>>();

        public void Add(string key, string value)
        {
            if (Queries.ContainsKey(key))
                Queries[key].Add(value);
            else
            {
                List<string> values = new List<string>(1);
                values.Add(value);
                Queries[key] = values;
            }
        }

        public string BuildQuery()
        {
            return "?" + string.Join("&", this);
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var item in Queries)
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    foreach (var value in item.Value)
                    {
                        yield return HttpUtility.UrlEncode(value);
                    }
                }
                else
                {
                    foreach (var value in item.Value)
                    {
                        yield return $"{HttpUtility.UrlEncode(item.Key)}={HttpUtility.UrlEncode(value)}";
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
