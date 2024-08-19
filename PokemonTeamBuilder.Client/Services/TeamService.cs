using PokemonTeamBuilder.Client.Helper;
using Shared_Library.Dto;
using Shared_Library.Models;
using System.Net.Http.Json;

namespace PokemonTeamBuilder.Client.Services
{
	public interface ITeamService
	{
		public Task<IEnumerable<Team>> GetAllTeam(string userName);
		public Task<TeamCreateResultDto> AddNewTeam(TeamCreateRequestDto info);
		public Task<bool> UpdateTeam(string userName, Team team); 

	}
	public class TeamService : ITeamService
	{
		private readonly HttpClient _client;
		private readonly AuthHelper _authHelper;

		public TeamService(HttpClient client, AuthHelper authHelper)
		{
			_authHelper = authHelper;
			_client = client;
		}

		public async Task<TeamCreateResultDto> AddNewTeam(TeamCreateRequestDto info)
		{
			try
			{
				var response = await _client.PostAsJsonAsync<TeamCreateRequestDto>("/CreateTeam", info);
				var stringResult = await response.Content.ReadAsStringAsync();
				var result = await response.Content.ReadFromJsonAsync<TeamCreateResultDto>();
				return result;	
			}
			catch(Exception e)
			{
				return new TeamCreateResultDto
				{
					IsSuccess = false,
					ErrorMessage = e.Message
				};

			}

		}

		public async Task<IEnumerable<Team>> GetAllTeam(string userName)
		{
			try
			{
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await _authHelper.GetJwtToken());
				var respone = await _client.GetAsync("/GetAllTeam?userName=" + userName);
				if(respone.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return await respone.Content.ReadFromJsonAsync<IEnumerable<Team>>();
				}	
				else
				{
					return new List<Team>();
				}	
				
			}
			catch(Exception e)
			{
				return new List<Team>();
			}
		}

		public Task<bool> UpdateTeam(string userName, Team team)
		{
			throw new NotImplementedException();
		}
	}
}
