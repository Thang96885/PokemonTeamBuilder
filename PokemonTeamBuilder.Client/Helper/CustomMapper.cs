using PokeApiNet;
using Shared_Library.Models;

namespace PokemonTeamBuilder.Client.Helper
{
	public class CustomMapper
	{
		public PokemonSetUp MapPokemon(Pokemon pokemon)
		{
			return new PokemonSetUp
			{
				PokemonName = pokemon.Name,
				PokId = pokemon.Id,
				Types = pokemon.Types.Select(t => new TypeDto { Name = t.Type.Name}).ToList(),
				PicUrl = $"https://play.pokemonshowdown.com/sprites/ani/{pokemon.Name.ToLower()}.gif",
			};


		}
	}
}
