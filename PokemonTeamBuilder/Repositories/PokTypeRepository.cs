
using Microsoft.EntityFrameworkCore;
using Shared_Library.Data;
using Shared_Library.Models;

namespace PokemonTeamBuilder.Api.Repositories
{
	public class PokTypeRepository : IPokTypeRepository
	{
		private readonly PokemonTeamBuilderContext _contect;

		public PokTypeRepository(PokemonTeamBuilderContext contect)
		{
			_contect = contect;
		}
		public async Task<TypeDto> GetTypeByName(string typeName)
		{
			var type = await _contect.Types.Where(t => t.Name.ToLower() == typeName.ToLower()).FirstAsync();
			return type;
		}
	}
}
