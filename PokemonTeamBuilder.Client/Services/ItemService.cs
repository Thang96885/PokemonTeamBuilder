using PokeApiNet;
using System.Net.Http.Json;

namespace PokemonTeamBuilder.Client.Services
{
	public interface IItemService
	{
		public Task<NamedApiResourceList<Item>> GetAllItemsAsync();
	}
	public class ItemService : IItemService
	{
		private readonly HttpClient _client;

		public ItemService(HttpClient client)
		{
			_client = client;
		}

		public async Task<NamedApiResourceList<Item>> GetAllItemsAsync()
		{
			try
			{
				var result = await _client.GetFromJsonAsync<NamedApiResourceList<Item>>("/GetAllItems");
				return result;
			}
			catch(Exception e)
			{
				return null;
			}
		}
	}
}
