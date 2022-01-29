using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace HttpAction
{
    public static class HttpActionClient
    {
        public static Task<T> SendActionAsync<T>(this HttpClient client, HttpAction<T> httpAction) =>
            SendActionAsync(client, httpAction, HttpCompletionOption.ResponseContentRead, CancellationToken.None);

        public static Task<T> SendActionAsync<T>(this HttpClient client, HttpAction<T> httpAction, CancellationToken cancellationToken) =>
            SendActionAsync(client, httpAction, HttpCompletionOption.ResponseContentRead, cancellationToken);

        public static Task<T> SendActionAsync<T>(this HttpClient client, HttpAction<T> httpAction, HttpCompletionOption httpCompletionOption) =>
            SendActionAsync(client, httpAction, httpCompletionOption, CancellationToken.None);

        public static async Task<T> SendActionAsync<T>(this HttpClient client, HttpAction<T> httpAction, HttpCompletionOption httpCompletionOption, CancellationToken cancellationToken)
        {
            try
            {
                if (httpAction.ResponseHandler == null)
                    throw new ArgumentNullException("httpAction.ResponseHandler");

                if (httpAction.CheckValidation != null)
                {
                    string valid = httpAction.CheckValidation(httpAction);
                    if (!string.IsNullOrEmpty(valid))
                        throw new ArgumentException("Invalid Request : " + valid);
                }

                if (httpAction.RequestUri == null)
                    httpAction.RequestUri = httpAction.CreateUri();

                if (httpAction.RequestHeaders != null)
                    httpAction.RequestHeaders.AddToHeader(httpAction.Headers);

                HttpResponseMessage response
                    = await client.SendAsync((HttpRequestMessage)httpAction, httpCompletionOption, cancellationToken);

                T result;
                if (response.IsSuccessStatusCode || httpAction.ErrorHandler == null)
                {
                    try
                    {
                        result = await httpAction.ResponseHandler(response);
                    }
                    catch (Exception e)
                    {
                        if (httpAction.ErrorHandler != null)
                            result = await httpAction.ErrorHandler(response, e);
                        else
                            throw;
                    }
                }
                else
                    result = await httpAction.ErrorHandler(response, null);

                ActionResponse actionResponse = result as ActionResponse;
                if (actionResponse != null)
                    actionResponse.StatusCode = (int)response.StatusCode;

                return result;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (httpAction.ErrorHandler != null)
                    return await httpAction.ErrorHandler(null, ex);
                else
                    throw;
            }
        }
    }
}
