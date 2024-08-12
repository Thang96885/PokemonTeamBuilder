using PokeApiNet;

namespace PokemonTeamBuilder.Api.Repositories
{
	public interface IPokemonMoveChooseRepository
	{
		public Task AddMoveToPokemonSetup(int pokemonSetupId, IEnumerable<NamedApiResource<Move>> moveList);
	}
}
