using Shared_Library.Models;
using System.Net.Http.Json;
using System.Net.WebSockets;

namespace PokemonTeamBuilder.Client.Services
{
	public interface IMoveService
	{
		public Task<PokemonMoveChoose> GetMoveByName(string name);
	}
	public class MoveService : IMoveService
	{
		private readonly HttpClient _client;

		public MoveService(HttpClient client)
		{
			_client = client;
		}
		public async Task<PokemonMoveChoose> GetMoveByName(string name)
		{
			var response = await _client.GetAsync("GetMoveInfoByName/" + name);
			if(response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return await response.Content.ReadFromJsonAsync<PokemonMoveChoose>();
			}
			else
			{
				return null;
			}
		}
	}
}
