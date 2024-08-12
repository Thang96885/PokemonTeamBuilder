using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Shared_Library.Dto;

namespace PokemonTeamBuilder.Client.Helper
{
	public class AuthHelper
	{
		private readonly ISessionStorageService _sessionStorageService;
		private readonly ILocalStorageService _localStorageService;

		public AuthHelper(ISessionStorageService sessionStorageService, ILocalStorageService localStorageService)
		{
			_sessionStorageService = sessionStorageService;
			_localStorageService = localStorageService;
		}
		public async void StoreToken(LoginResultDto token)
		{
			await _sessionStorageService.SetItemAsync("JwtToken", token.JwtToken);
			await _sessionStorageService.SetItemAsync("RefreshToken", token.RefreshToken);
			await _localStorageService.SetItemAsync("UserName", token.UserName);
		}


		public async Task<string> GetJwtToken()
		{
			var token = await _sessionStorageService.GetItemAsync<string>("JwtToken");
			return token;
		}

		public async Task<string> GetRefreshToken()
		{
			var token = await _sessionStorageService.GetItemAsync<string>("RefreshTOken");
			return token;
		}

		public async Task<string> GetUserName()
		{
			var userName = await _localStorageService.GetItemAsync<string>("UserName");
			return userName;
		}
	}
}
