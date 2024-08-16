
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

		[HttpGet("/GetPokemonInfoByName/{name}")]
		public async Task<IActionResult> GetPokemonInfoByName(string name)
		{
			var pokemon = await _pokeApiClient.GetResourceAsync<Pokemon>(name);
			return Ok(pokemon);
		}


	}
}
