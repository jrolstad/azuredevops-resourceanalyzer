using System.Threading.Tasks;

namespace azuredevopsresourceanalyzer.core.Services
{
    public interface ITokenService
    {
        Task<string> GetBearerToken(string resource);
    }
}