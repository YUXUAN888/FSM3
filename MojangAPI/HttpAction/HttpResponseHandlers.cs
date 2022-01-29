using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//#nullable disable

namespace HttpAction
{
    public class HttpResponseHandlers
    {
        public static Func<HttpResponseMessage, Task<T>> GetDefaultResponseHandler<T>()
        {
            if (typeof(T) == typeof(bool))
                return GetSuccessCodeResponseHandler() as Func<HttpResponseMessage, Task<T>>;
            if (typeof(T) == typeof(int))
                return GetStatusCodeResponseHandler()  as Func<HttpResponseMessage, Task<T>>;
            if (typeof(T) == typeof(string))
                return GetStringResponseHandler()      as Func<HttpResponseMessage, Task<T>>;
            if (typeof(T) == typeof(Stream))
                return GetStreamResponseHandler()      as Func<HttpResponseMessage, Task<T>>;

            return GetJsonHandler<T>();
        }

        public static Func<HttpResponseMessage, Task<bool>> GetSuccessCodeResponseHandler() =>
            (response) =>
            {
                return Task.FromResult(response.IsSuccessStatusCode);
            };

        public static Func<HttpResponseMessage, Task<bool>> GetSuccessCodeResponseHandler(int successCode) =>
            (response) =>
            {
                bool result = (int)response.StatusCode == successCode;
                return Task.FromResult(result);
            };

        public static Func<HttpResponseMessage, Task<int>> GetStatusCodeResponseHandler() =>
            (response) =>
            {
                return Task.FromResult((int)response.StatusCode);
            };

        public static Func<HttpResponseMessage, Task<string>> GetStringResponseHandler() =>
            (response) =>
            {
                return response.Content.ReadAsStringAsync();
            };

        public static Func<HttpResponseMessage, Task<Stream>> GetStreamResponseHandler() =>
            (response) =>
            {
                return response.Content.ReadAsStreamAsync();
            };

        public static Func<HttpResponseMessage, Task<T>> GetSuccessCodeResponseHandler<T>(T returnObj) =>
            (response) =>
            {
                if (response.IsSuccessStatusCode)
                    return Task.FromResult(returnObj);
                else
                    throw new Exception();
            };

        public static Func<HttpResponseMessage, Exception, Task<T>> GetJsonErrorHandler<T>() =>
            (response, ex) =>
            GetJsonHandler<T>().Invoke(response);

        public static Func<HttpResponseMessage, Task<T>> GetJsonHandler<T>() =>
            async (response) =>
            {
                string res = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(res))
                    res = "{}";
                return JsonConvert.DeserializeObject<T>(res, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            };

        public static Func<HttpResponseMessage, Exception, Task<T>> GetDefaultErrorHandler<T>() =>
            (response, ex) =>
            {
                if (response != null)
                    response.EnsureSuccessStatusCode();
                if (ex != null)
                    throw ex;

                return Task.FromResult(default(T));
            };

        private static JsonSerializer defaultSerializer = new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static Func<HttpResponseMessage, Task<T[]>> GetJsonArrayHandler<T>() =>
            async (response) =>
            {
                string res = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(res))
                    res = "[]";
                JArray jarr = JArray.Parse(res);
                return jarr.Select(x => x.ToObject<T>(defaultSerializer)).ToArray();
            };
    }
}
