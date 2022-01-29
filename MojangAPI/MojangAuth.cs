using HttpAction;
using MojangAPI.Cache;
using MojangAPI.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MojangAPI
{
    public class MojangAuth
    {
        private HttpClient client;
        private ICacheManager<Session> cacheManager;

        public MojangAuth() : this(null) { }

        public MojangAuth(HttpClient client) :
            this(client, new SessionFileCacheManager("mojang_auth.json"))
        { }

        public MojangAuth(HttpClient httpClient, ICacheManager<Session> _cacheManager)
        {
            if (httpClient == null)
                this.client = Mojang.DefaultClient;
            else
                this.client = httpClient;

            this.cacheManager = _cacheManager;
        }

        private string CreateClientToken()
        {
            return Guid.NewGuid().ToString();
        }

        private void SaveSession(Session session)
        {
            if (string.IsNullOrEmpty(session.ClientToken))
            {
                Session cachedSession = cacheManager?.ReadCache();
                if (cachedSession == null || string.IsNullOrEmpty(cachedSession.ClientToken))
                    session.ClientToken = CreateClientToken();
                else
                    session.ClientToken = cachedSession.ClientToken;
            }
            cacheManager?.SaveCache(session);
        }

        private async Task<T> Request<T>(HttpAction<T> httpAction) where T : MojangAuthResponse
        {
            if (string.IsNullOrEmpty(httpAction.Host))
                httpAction.Host = "https://authserver.mojang.com";

            T response = await client.SendActionAsync(httpAction);

            if (response.IsSuccess && response.Session != null)
                SaveSession(response.Session);

            return response;
        }

        private async Task<MojangAuthResponse> authResponseHandler(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            JObject job = JObject.Parse(content);

            var selectedProfile = job["selectedProfile"];
            if (selectedProfile == null)
                return new MojangAuthResponse(MojangAuthResult.NoProfile);

            return new MojangAuthResponse(MojangAuthResult.Success)
            {
                Session = new Session
                {
                    AccessToken = job["accessToken"]?.ToString(),
                    UUID = selectedProfile["id"]?.ToString(),
                    Username = selectedProfile["name"]?.ToString()
                }
            };
        }

        private async Task<MojangAuthResponse> errorResponseHandler(HttpResponseMessage response, Exception ex)
        {
            if (response == null)
                return new MojangAuthResponse(MojangAuthResult.UnknownError);

            try
            {
                string content = await response.Content.ReadAsStringAsync();
                MojangAuthResponse authResponse = JsonConvert.DeserializeObject<MojangAuthResponse>(content);

                switch (authResponse.Error)
                {
                    case "Method Not Allowed":
                    case "Not Found":
                    case "Unsupported Media Type":
                        authResponse.Result = MojangAuthResult.BadRequest;
                        break;
                    case "IllegalArgumentException":
                    case "ForbiddenOperationException":
                        authResponse.Result = MojangAuthResult.InvalidCredentials;
                        break;
                    default:
                        authResponse.Result = MojangAuthResult.UnknownError;
                        break;
                }

                return authResponse;

            }
            catch
            {
                return new MojangAuthResponse(MojangAuthResult.UnknownError);
            }
        }

        public Task<MojangAuthResponse> Authenticate(string email, string password) =>
            Authenticate(email, password, cacheManager?.ReadCache()?.ClientToken);

        public Task<MojangAuthResponse> Authenticate(string email, string password, string clientToken) =>
            Request(new HttpAction<MojangAuthResponse>
            {
                Method = HttpMethod.Post,
                Path = "authenticate",

                Content = new JsonHttpContent(new
                {
                    agent = new
                    {
                        name = "Minecraft",
                        version = 1
                    },
                    username = email,
                    password = password,
                    clientToken = clientToken
                }),

                CheckValidation = (h) =>
                {
                    if (string.IsNullOrEmpty(email)) return nameof(email);
                    else if (string.IsNullOrEmpty(password)) return nameof(password);
                    else return null;
                },
                ResponseHandler = authResponseHandler,
                ErrorHandler = errorResponseHandler
            });

        public async Task<MojangAuthResponse> TryAutoLogin()
        {
            MojangAuthResponse res = await Validate();

            if (!res.IsSuccess)
                res = await Refresh();

            return res;
        }

        public async Task<MojangAuthResponse> TryAutoLogin(Session session)
        {
            MojangAuthResponse res = await Validate(session);

            if (!res.IsSuccess)
                res = await Refresh(session);

            return res;
        }

        public Task<MojangAuthResponse> Refresh()
        {
            Session cachedSession = cacheManager?.ReadCache();
            if (cachedSession == null)
                return Task.FromResult(new MojangAuthResponse(MojangAuthResult.InvalidSession));
            else
                return Refresh(cachedSession);
        }

        public Task<MojangAuthResponse> Refresh(Session session) =>
            Request(new HttpAction<MojangAuthResponse>
            {
                Method = HttpMethod.Post,
                Path = "refresh",

                Content = new JsonHttpContent(new
                {
                    accessToken = session.AccessToken,
                    clientToken = session.ClientToken,

                    selectedProfile = new
                    {
                        id = session.UUID,
                        name = session.Username
                    }
                }),

                CheckValidation = (h) =>
                {
                    if (session == null) return nameof(session);
                    else if (session.IsEmpty) return "Empty session";
                    else return null;
                },
                ResponseHandler = authResponseHandler,
                ErrorHandler = errorResponseHandler
            });

        public Task<MojangAuthResponse> Validate()
        {
            Session cachedSession = cacheManager.ReadCache();
            if (cachedSession == null || string.IsNullOrEmpty(cachedSession.AccessToken))
                return Task.FromResult(new MojangAuthResponse(MojangAuthResult.InvalidSession));
            else
                return Validate(cachedSession);
        }

        public async Task<MojangAuthResponse> Validate(Session session)
        {
            MojangAuthResponse res = await Validate(session.AccessToken, session.ClientToken);
            if (res.IsSuccess)
                res.Session = session;
            return res;
        }

        public Task<MojangAuthResponse> Validate(string accessToken, string clientToken) =>
            Request(new HttpAction<MojangAuthResponse>
            {
                Method = HttpMethod.Post,
                Path = "validate",

                Content = new JsonHttpContent(new
                {
                    accessToken = accessToken,
                    clientToken = clientToken
                }),

                CheckValidation = (h) =>
                {
                    if (string.IsNullOrEmpty(accessToken)) return nameof(accessToken);
                    else return null;
                },
                ResponseHandler = HttpResponseHandlers.GetSuccessCodeResponseHandler(
                    new MojangAuthResponse(MojangAuthResult.Success)),
                ErrorHandler = errorResponseHandler
            });

        public Task<MojangAuthResponse> Signout(string email, string password) =>
            Request(new HttpAction<MojangAuthResponse>
            {
                Method = HttpMethod.Post,
                Path = "signout",

                Content = new JsonHttpContent(new
                {
                    username = email,
                    password = password
                }),

                CheckValidation = (h) =>
                {
                    if (string.IsNullOrEmpty(email)) return nameof(email);
                    else if (string.IsNullOrEmpty(password)) return nameof(password);
                    else return null;
                },
                ResponseHandler = HttpResponseHandlers.GetSuccessCodeResponseHandler(
                    new MojangAuthResponse(MojangAuthResult.Success)),
                ErrorHandler = errorResponseHandler
            });

        public Task<MojangAuthResponse> Invalidate()
        {
            Session cachedSession = cacheManager?.ReadCache();
            if (cachedSession == null || string.IsNullOrEmpty(cachedSession.AccessToken))
                return Task.FromResult(new MojangAuthResponse(MojangAuthResult.InvalidSession));
            else
                return Invalidate(cachedSession.AccessToken, cachedSession.ClientToken);
        }

        public Task<MojangAuthResponse> Invalidate(string accessToken, string clientToken) =>
            Request(new HttpAction<MojangAuthResponse>
            {
                Method = HttpMethod.Post,
                Path = "invalidate",

                Content = new JsonHttpContent(new
                {
                    accessToken = accessToken,
                    clientToken = clientToken
                }),

                CheckValidation = (h) =>
                {
                    if (string.IsNullOrEmpty(accessToken)) return nameof(accessToken);
                    else return null;
                },
                ResponseHandler = HttpResponseHandlers.GetSuccessCodeResponseHandler(
                    new MojangAuthResponse(MojangAuthResult.Success)),
                ErrorHandler = errorResponseHandler
            });

        public Task<MicrosoftAuthResponse> LoginWithXbox(string uhs, string xstsToken) =>
            client.SendActionAsync(new HttpAction<MicrosoftAuthResponse>
            {
                Method = HttpMethod.Post,
                Host = "https://api.minecraftservices.com",
                Path = "authentication/login_with_xbox",

                Content = new StringContent($"{{\"identityToken\": \"XBL3.0 x={uhs};{xstsToken}\"}}"),

                ResponseHandler = async (response) =>
                {
                    string content = await response.Content.ReadAsStringAsync();
                    MicrosoftAuthResponse authResponse = JsonConvert.DeserializeObject<MicrosoftAuthResponse>(content);
                    authResponse.ExpiresOn = DateTime.Now.AddSeconds(authResponse.ExpiresIn);
                    return authResponse;
                },

                CheckValidation = (h) =>
                {
                    if (string.IsNullOrEmpty(uhs)) return nameof(uhs);
                    else if (string.IsNullOrEmpty(xstsToken)) return nameof(xstsToken);
                    else return null;
                }
            });

        public async Task<MojangAuthResponse> RequestSessionWithXbox(string uhs, string xstsToken)
        {
            try
            {
                MicrosoftAuthResponse authResponse = await LoginWithXbox(uhs, xstsToken);

                if (!authResponse.IsSuccess)
                    return new MojangAuthResponse(MojangAuthResult.InvalidCredentials);

                Mojang mojang = new Mojang(this.client);

                if (!await mojang.CheckGameOwnership(authResponse.AccessToken))
                    return new MojangAuthResponse(MojangAuthResult.NoProfile);

                PlayerProfile profile = await mojang.GetProfileUsingAccessToken(authResponse.AccessToken);
                Session session = new Session
                {
                    Username = profile.Name,
                    AccessToken = authResponse.AccessToken,
                    UUID = profile.UUID
                };

                SaveSession(session);

                return new MojangAuthResponse(MojangAuthResult.Success)
                {
                    Session = session
                };
            }
            catch
            {
                return new MojangAuthResponse(MojangAuthResult.UnknownError);
            }
        }
    }
}
