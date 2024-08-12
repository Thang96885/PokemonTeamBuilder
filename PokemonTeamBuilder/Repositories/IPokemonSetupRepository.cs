using Shared_Library.Models;

namespace PokemonTeamBuilder.Api.Repositories
{
	public interface IPokemonSetupRepository
	{
		public Task AddPokemonSetupToTeam(IEnumerable<PokemonSetUp> pokemonSetupList, int teamId);
	}
}
