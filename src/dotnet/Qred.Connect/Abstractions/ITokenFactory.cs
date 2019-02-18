using System.Threading.Tasks;
using IdentityModel.Client;

namespace Qred.Connect.Abstractions
{
    public interface ITokenFactory
    {
        Task<TokenResponse> GetToken();
    }
}