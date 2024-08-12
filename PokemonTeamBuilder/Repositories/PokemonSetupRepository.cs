using Microsoft.EntityFrameworkCore;
using Shared_Library.Data;
using Shared_Library.Models;

namespace PokemonTeamBuilder.Api.Repositories
{
	public class PokemonSetupRepository : IPokemonSetupRepository
	{
		private readonly PokemonTeamBuilderContext _context;

		public PokemonSetupRepository(PokemonTeamBuilderContext context)
		{
			_context = context;
		}

		public async Task AddPokemonSetupToTeam(IEnumerable<PokemonSetUp> pokemonSetupList, int teamId)
		{
			var team = await _context.Teams.Include(t => t.PokemonSetUps).Where(t => t.Id == teamId).FirstOrDefaultAsync();
			if (team == null)
			{
				return;
			}
			var oldPokemonSetup = team.PokemonSetUps;
			_context.PokemonSetUps.RemoveRange(team.PokemonSetUps);
			team.PokemonSetUps = pokemonSetupList;
		}
	}
}
