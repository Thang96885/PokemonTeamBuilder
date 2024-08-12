using Microsoft.EntityFrameworkCore;
using PokeApiNet;
using Shared_Library.Data;
using Shared_Library.Models;

namespace PokemonTeamBuilder.Api.Repositories
{
	public class PokemonMoveChooseRepository : IPokemonMoveChooseRepository
	{
		private readonly PokemonTeamBuilderContext _context;
		private readonly PokeApiClient _pokeClient;

		public PokemonMoveChooseRepository(PokemonTeamBuilderContext context, PokeApiClient pokeClient)
		{
			_pokeClient = pokeClient;
			_context = context;
		}
		public async Task AddMoveToPokemonSetup(int pokemonSetupId, IEnumerable<NamedApiResource<Move>> moveList)
		{
			var pokemonSetup = await _context.PokemonSetUps.Include(x => x.Moves).Where(x => x.Id == pokemonSetupId).FirstOrDefaultAsync();
			if(pokemonSetup == null)
			{
				return;
			}	
			foreach(var move in pokemonSetup.Moves)
			{
				_context.Moves.Remove(move);
			}	
			foreach(var move in moveList)
			{
				var moveInfo = await _pokeClient.GetResourceAsync<Move>(move.Name);
				var type = await _context.Types.Where(t => t.Name.ToLower() == moveInfo.Type.Name.ToLower()).FirstOrDefaultAsync();
				var movetoAdd = new PokemonMoveChoose
				{
					Name = move.Name,
					Power = moveInfo.Power,
					Type = type,
					PokemonSetUp = pokemonSetup,
				};
				_context.Moves.Add(movetoAdd);
			}	

		}
	}
}
