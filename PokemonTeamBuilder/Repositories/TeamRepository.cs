using Microsoft.EntityFrameworkCore;
using Shared_Library.Data;
using Shared_Library.Models;
using System.Security.Cryptography.X509Certificates;

namespace PokemonTeamBuilder.Api.Repositories
{
	public class TeamRepository : ITeamRepository
	{
		private readonly PokemonTeamBuilderContext _context;

		public TeamRepository(PokemonTeamBuilderContext context)
		{
			_context = context;
		}

		public void CreateNewTeam(Team team)
		{
			_context.Add(team);
		}

		public async Task<IEnumerable<Team>> GetAllTeamBasicInfoAsync(int userId)
		{
			var teams = await _context.Teams.Where(t => t.UserId == userId).AsNoTracking().ToListAsync();
			return teams;
		}

		public async Task<Team> GetTeamAsync(int teamId)
		{
			var team = await _context.Teams.FindAsync(teamId);
			return team;
		}

		public void UpdateTeam(Team team)
		{
			var teamNeedUpdate = _context.Entry<Team>(team);
			teamNeedUpdate.State = EntityState.Modified;
		}
	}
}
