using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Shared_Library.Dto;

namespace PokemonTeamBuilder.Client.Helper
{
	public class AuthHelper
	{
		private readonly ISessionStorageService _sessionStorageService;
		private readonly ILocalStorageService _localStorageService;

		public AuthHelper(ISessionStorageService sessionStorageService, ILocalStorageService localStorageService, HttpClient httpCLient)
		{
			_sessionStorageService = sessionStorageService;
			_localStorageService = localStorageService;
		}
		public async Task StoreToken(LoginResultDto token)
		{
			try
			{
				await _sessionStorageService.SetItemAsync("JwtToken", token.JwtToken);
				await _sessionStorageService.SetItemAsync("RefreshToken", token.RefreshToken);
				await _sessionStorageService.SetItemAsync("UserName", token.UserName);
				
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}


		public async Task<string> GetJwtToken()
		{
			var token = await _sessionStorageService.GetItemAsync<string>("JwtToken");
			return token;
		}

		public async Task<string> GetRefreshToken()
		{
			var token = await _sessionStorageService.GetItemAsync<string>("RefreshToken");
			return token;
		}

		public async Task<string> GetUserName()
		{
			var userName = await _sessionStorageService.GetItemAsync<string>("UserName");
			return userName;
		}
	}
}
