using PokemonTeamBuilder.Client.Helper;
using Shared_Library.Dto;
using System.Net.Http.Json;

namespace PokemonTeamBuilder.Client.Services
{
    public interface IAuthService
    {
        public Task<LoginResultDto> Login(LoginRequestDto loginInfo);
        public Task<LoginResultDto> Register(RegisterRequestDto registerInfo);
        public Task<LoginResultDto> Refresh(RefreshRequestDto refreshInfo);
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthHelper _authHelper;

        public AuthService(HttpClient httpClient, AuthHelper authHelper)
        {
            _httpClient = httpClient;
            _authHelper = authHelper;
        }

        public async Task<LoginResultDto> Login(LoginRequestDto loginInfo)
        {
            var respone = await _httpClient.PostAsJsonAsync("api/Auth/login", loginInfo);
            var result = await respone.Content.ReadFromJsonAsync<LoginResultDto>();
            if(result.IsSuccess)
            {
				_authHelper.StoreToken(result);
			}
            return result;
        }

		public async Task<LoginResultDto> Refresh(RefreshRequestDto refreshInfo)
		{
            var response = await _httpClient.PostAsJsonAsync<RefreshRequestDto>("api/Auth/refresh", refreshInfo);
            var result = await response.Content.ReadFromJsonAsync<LoginResultDto>();
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _authHelper.StoreToken(result);
            }
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
