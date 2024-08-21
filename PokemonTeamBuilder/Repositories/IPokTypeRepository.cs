using Shared_Library.Models;

namespace PokemonTeamBuilder.Api.Repositories
{
	public interface IPokTypeRepository
	{
		public Task<TypeDto> GetTypeByName(string typeName);
	}
}
