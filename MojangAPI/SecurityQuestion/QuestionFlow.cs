using MojangAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpAction;
using Newtonsoft.Json.Linq;

namespace MojangAPI.SecurityQuestion
{
    public class QuestionFlow
    {
        private HttpClient client;

        public QuestionFlow()
        {
            client = Mojang.DefaultClient;
        }

        public QuestionFlow(HttpClient client)
        {
            this.client = client;
        }

        public Task<MojangAPIResponse> CheckTrusted(string accessToken) =>
            client.SendActionAsync(new HttpAction<MojangAPIResponse>
            {
                Method = HttpMethod.Get,
                Host = "https://api.mojang.com",
                Path = "user/security/location",

                RequestHeaders = new HttpHeaderCollection
                {
                    { "Authorization", "Bearer " + accessToken ?? throw new ArgumentNullException(nameof(accessToken)) }
                },

                ResponseHandler = HttpResponseHandlers.GetSuccessCodeResponseHandler(new MojangAPIResponse()),
                ErrorHandler = HttpResponseHandlers.GetJsonErrorHandler<MojangAPIResponse>()
            });

        public Task<QuestionFlowResponse> GetQuestionList(string accessToken) =>
            client.SendActionAsync(new HttpAction<QuestionFlowResponse>
            {
                Method = HttpMethod.Get,
                Host = "https://api.mojang.com",
                Path = "user/security/challenges",

                RequestHeaders = new HttpHeaderCollection
                {
                    { "Authorization", "Bearer " + accessToken ?? throw new ArgumentNullException() }
                },

                ResponseHandler = async (response) =>
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JArray jarr = JArray.Parse(content);

                    List<Question> questions = new List<Question>(3);
                    foreach (var item in jarr)
                    {
                        questions.Add(new Question(
                            questionId: int.Parse(item["question"]?["id"]?.ToString() ?? ""),
                            questionMsg: item["question"]?["question"]?.ToString(),
                            answerId: int.Parse(item["answer"]?["id"]?.ToString() ?? "")));
                    }

                    return new QuestionFlowResponse(new QuestionList(questions.ToArray()));
                },
                ErrorHandler = HttpResponseHandlers.GetJsonErrorHandler<QuestionFlowResponse>()
            });

        public Task<MojangAPIResponse> SendAnswers(QuestionList list, string accessToken)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (accessToken == null)
                throw new ArgumentNullException(nameof(accessToken));

            if (!list.CheckAllAnswered())
                throw new ArgumentException("Not all answered");

            JArray jarr = new JArray();
            foreach (var item in list)
            {
                jarr.Add(JObject.FromObject(new
                {
                    id = item.AnswerId,
                    answer = item.Answer
                }));
            }

            return client.SendActionAsync(new HttpAction<MojangAPIResponse>
            {
                Method = HttpMethod.Post,
                Host = "https://api.mojang.com",
                Path = "user/security/location",

                RequestHeaders = new HttpHeaderCollection
                {
                    { "Authorization", "Bearer " + accessToken }
                },

                Content = new StringContent(jarr.ToString(), Encoding.UTF8, "application/json"),

                ResponseHandler = HttpResponseHandlers.GetSuccessCodeResponseHandler(new MojangAPIResponse()),
                ErrorHandler = HttpResponseHandlers.GetJsonErrorHandler<MojangAPIResponse>()
            });
        }
    }
}
