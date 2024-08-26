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
			var oldPokemonSetup = await _context.PokemonSetUps.Where(p => p.TeamId == teamId).Select(p => new PokemonSetUp
			{
				Id = p.Id,
				PokemonName = p.PokemonName,
				PokId = p.PokId,
				AbilityName = p.AbilityName,
				ItemName = p.ItemName,
				Moves = p.Moves.Select(m => new PokemonMoveChoose
				{
					Id = m.Id,
				}).ToList(),
				Team = null,
				TeamId = p.TeamId
			}).ToListAsync();
			_context.PokemonSetUps.RemoveRange(oldPokemonSetup);

			foreach(var pokemonSetup in pokemonSetupList)
			{
				pokemonSetup.TeamId = teamId;
				_context.Entry<PokemonSetUp>(pokemonSetup).State = EntityState.Added;
				foreach(var move in pokemonSetup.Moves)
				{
					_context.Entry<PokemonMoveChoose>(move).State = EntityState.Added;
				}
			}
			
		}

		public async Task<IEnumerable<PokemonSetUp>> GetPokemonSetupInTeam(int teamId)
		{
			try
			{
				var pokemonSetupList = await _context.PokemonSetUps.Include(p => p.Types).Include(p => p.Moves).Where(p => p.TeamId == teamId).Select(p => new PokemonSetUp
				{
					Id = p.Id,
					TeamId = p.TeamId,
					PokId = p.PokId,
					PokemonName = p.PokemonName,
					AbilityName = p.AbilityName,
					ItemName = p.ItemName,
					Types = p.Types.Select(t => new TypeDto
					{
						Id = t.Id,
						Name = t.Name,
					}).ToList(),
					Moves = p.Moves.Select(m => new PokemonMoveChoose
					{
						Id = m.Id,
						Name = m.Name,
						Power = m.Power,
						PokemonSetUp = null,
						Type = new TypeDto
						{
							Id = m.Type.Id,
							Name = m.Type.Name,
						},
					}).ToList(),
				}).ToListAsync();
				return pokemonSetupList;
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
