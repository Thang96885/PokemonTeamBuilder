using Microsoft.AspNetCore.Components.Authorization;
using PokemonTeamBuilder.Client.Services;
using Shared_Library.Dto;
using System.Security.Claims;

namespace PokemonTeamBuilder.Client.Helper
{
	public class TokenAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly AuthHelper _authHelper;
		private readonly IAuthService _authService;
		public TokenAuthenticationStateProvider(AuthHelper authHelper, IAuthService authService)
		{
			_authHelper = authHelper;
			_authService = authService;
		}

		public async void StoreTokenAsync(LoginResultDto token)
		{
			await _authHelper.StoreToken(token);
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}

		public async Task<string> GetToken()
		{
			var token = await _authHelper.GetJwtToken();
			if(await CheckTokenExprie() == false)
			{
				throw new Exception("token expired");
			}
			return token;
		}

		public async Task<string> GetRefreshToken()
		{
			return await _authHelper.GetRefreshToken();
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _authHelper.GetJwtToken();
			var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(HelperFunction.ParseClaimsFromJwt(token), "jwt");
			return new AuthenticationState(new ClaimsPrincipal(identity));
		}

		private async Task<bool> CheckTokenExprie()
		{
			var authState = await GetAuthenticationStateAsync();
			var user = authState.User;
			var exp = user.FindFirst(c => c.Type.Equals("exp")).Value;
			var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
			var timeUTC = DateTime.UtcNow;
			var diff = expTime - timeUTC;
			if(expTime <= timeUTC)
			{
				var result = await _authService.Refresh(new RefreshRequestDto { JwtToken = await _authHelper.GetJwtToken(), RefreshToken = await _authHelper.GetRefreshToken() });
				return result.IsSuccess;
			}
			return true;

		}
	}
}
