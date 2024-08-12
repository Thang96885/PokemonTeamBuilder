using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeApiNet;

namespace PokemonTeamBuilder.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly PokeApiClient _pokeApiClient;

		public ItemController(PokeApiClient pokeApiClient)
		{
			_pokeApiClient = pokeApiClient;
		}

		[HttpGet("/GetAllItems")]
		public async Task<IActionResult> GetAllItems()
		{
			var items = await _pokeApiClient.GetNamedResourcePageAsync<Item>(2000, 0);
			return Ok(items);
		}
	}
}
