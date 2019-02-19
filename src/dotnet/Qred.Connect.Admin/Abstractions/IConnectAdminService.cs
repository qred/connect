using System;
using System.Threading.Tasks;
using Qred.Connect.Abstractions;

namespace Qred.Connect.Admin.Abstractions
{

    public interface IConnectAdminService : IConnectService
    {
        Task<ApplicationSource> GetApplicationSource(string applicationId);
        Task<ApplicationsPage> GetApplications();
    }
}
