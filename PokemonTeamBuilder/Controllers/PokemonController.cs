
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeApiNet;
using StackExchange.Redis;
using System.Diagnostics;
using System.Text.Json;

namespace PokedexAppUseRedis.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PokemonController : ControllerBase
	{
		private readonly IConnectionMultiplexer _redis;
		private readonly IDatabase _db;
		private readonly PokeApiClient _pokeApiClient;
		private readonly ILogger<PokemonController> _logger;
		public PokemonController(IConnectionMultiplexer redis, PokeApiClient pokeApiClient, ILogger<PokemonController> logger)
		{
			_logger = logger;
			_redis = redis;
			_db = _redis.GetDatabase();
			_pokeApiClient = pokeApiClient;
		}

		[HttpGet("/GetAllPokemon")]
		public async Task<IActionResult> GetAllPokemon()
		{
			var pokemonList = await _pokeApiClient.GetNamedResourcePageAsync<Pokemon>(1302, 0);
			return Ok(pokemonList);
		}





		private List<int> GetPokemonIdList(int page, int numberOfPokemons)
		{
			var pokemonIdList = new List<int>();
			for(int i = 1; i <= numberOfPokemons; i++)
			{
				pokemonIdList.Add((page - 1) * numberOfPokemons + i);
			}
			return pokemonIdList;
		}
	}
}
