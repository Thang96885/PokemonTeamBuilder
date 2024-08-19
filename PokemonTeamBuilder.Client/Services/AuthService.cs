using Shared_Library.Dto;
using System.Net.Http.Json;

namespace PokemonTeamBuilder.Client.Services
{
    public interface IAuthService
    {
        public Task<LoginResultDto> Login(LoginRequestDto loginInfo);
        public Task<LoginResultDto> Register(RegisterRequestDto registerInfo);
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResultDto> Login(LoginRequestDto loginInfo)
        {
            var respone = await _httpClient.PostAsJsonAsync("api/Auth/login", loginInfo);
            var result = await respone.Content.ReadFromJsonAsync<LoginResultDto>();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.JwtToken);
            return result;
        }

        public Task<LoginResultDto> Register(RegisterRequestDto registerInfo)
        {
            throw new NotImplementedException();
        }
    }


    public class NotifyLoginService
    {
        public string UserName { get; set; } = "";

        public event EventHandler LoginSuccess;

        public void SetUserName(string userName)
        {
            UserName = userName;
            NotifyLoginSuccess();
        }

        protected virtual void NotifyLoginSuccess()
        {
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
    }
}
