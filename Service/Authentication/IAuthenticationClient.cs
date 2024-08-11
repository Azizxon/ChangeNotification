using ChangeNotification.Models;

namespace ChangeNotification.Service.Authentication
{
    public interface IAuthenticationClient
    {
        Task<Token?> GetAccessTokenAsync();
        Task<Token?> SendRequestAsync(HttpRequestMessage request);
    }
}
