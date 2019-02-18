using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Qred.Connect.Abstractions;
using Qred.Connect.Admin.Abstractions;
using Qred.Connect.Implementations;

namespace Qred.Connect.Admin.Implementations
{
    public class ConnectAdminService : ConnectService, IConnectAdminService
    {
        public ConnectAdminService(HttpClient httpClient, IOptions<ConnectConfig> options, ITokenFactory tokenFactory, ILoggerFactory loggerFactory)
         : base(httpClient, options, tokenFactory, loggerFactory)
        {
        }

        public async Task<ApplicationSource> GetApplicationSource(string applicationId)
        {
            await ApplyTokenToHttpClient();

            var response = await httpClient.GetAsync(string.Join("/", options.Api.TrimEnd('/'), "loans/v1/applications", Uri.EscapeDataString(applicationId ), "_source"));
            if (!response.IsSuccessStatusCode)
            {
                throw await GetExceptionFromResponse(response);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApplicationSource>(content);
            }
        }
        public async Task<ApplicationsPage> GetApplications()
        {
            await ApplyTokenToHttpClient();

            var response = await httpClient.GetAsync(string.Join("/", options.Api.TrimEnd('/'), "loans/v1/applications"));
            if (!response.IsSuccessStatusCode)
            {
                throw await GetExceptionFromResponse(response);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApplicationsPage>(content);
            }
        }
    }
}
