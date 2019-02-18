using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Qred.Connect.Abstractions;

namespace Qred.Connect.Implementations
{
    public class ConnectService : IConnectService
    {
        protected HttpClient httpClient;
        protected ILogger<ConnectService> logger;
        protected ITokenFactory tokenFactory;
        protected ConnectConfig options;

        public ConnectService(HttpClient httpClient, 
            IOptions<ConnectConfig> options, 
            ITokenFactory tokenFactory, ILoggerFactory loggerFactory)
        {
            this.httpClient = httpClient;
            logger = loggerFactory.CreateLogger<ConnectService>();
            this.tokenFactory = tokenFactory;
            this.options = options.Value ?? throw new NullReferenceException(nameof(options));
        }
        public async Task<ApplicationCreateResponse> Create(ApplicationRequest request)
        {
            await ApplyTokenToHttpClient();

            var response = await httpClient.PostAsync(string.Join("/", options.Api.TrimEnd('/'), "loans/v1/applications"), new StringContent(request.ToJson(), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Got an error from request {request}", request.ToJson());
                throw await GetExceptionFromResponse(response);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var applicationResponse = JsonConvert.DeserializeObject<ApplicationCreateResponse>(content);
                return applicationResponse;
            }
        }

        protected static async Task<Exception> GetExceptionFromResponse(HttpResponseMessage response)
        {
            return new Exception(
                $"Got status code:\n{response.StatusCode} with content:\n{await response.Content.ReadAsStringAsync()}");
        }

        public async Task<ApplicationDecision> GetApplication(string applicationId)
        {
            await ApplyTokenToHttpClient();

            var response = await httpClient.GetAsync(string.Join("/", options.Api.TrimEnd('/'), "loans/v1/applications" ,Uri.EscapeDataString(applicationId)));
            if (!response.IsSuccessStatusCode)
            {
                throw await GetExceptionFromResponse(response);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var jObj = JObject.Parse(content);
                var decision = jObj.GetValue("decision").ToObject<string>();
                switch (decision)
                {
                    case "Approved":
                        return jObj.ToObject<ApplicationDecisionApproved>();
                    case "Denied":
                        return jObj.ToObject<ApplicationDecisionRejected>();
                    case "ManualProcess":
                        return jObj.ToObject<ApplicationDecisionManualProcess>();
                    default:
                        throw new Exception("decision :" + decision);
                }
            }
        }

        protected async Task ApplyTokenToHttpClient()
        {
            var token = await tokenFactory.GetToken();
            if (token != null)
            {
                httpClient.SetBearerToken(token.AccessToken);
            }
        }

    }
}
