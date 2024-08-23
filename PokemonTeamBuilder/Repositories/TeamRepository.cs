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
			var teams = await _context.Teams.Select(t => new Team
			{
				Id = t.Id,
				TeamName = t.TeamName,
				UserId = t.UserId,
				PokemonSetUps = t.PokemonSetUps.Select(p => new PokemonSetUp
				{
					PokId = p.PokId,
					PokemonName = p.PokemonName,
					
				})
			}).AsNoTracking().ToListAsync();
			foreach(var team in teams)
			{
				
			}	
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
