using System;
using System.Threading.Tasks;

namespace Qred.Connect.Abstractions
{
    public interface IConnectService
    {
        Task<ApplicationCreateResponse> Create(ApplicationRequest request);
        Task<ApplicationDecision> GetApplication(string applicationId);
    }
}
