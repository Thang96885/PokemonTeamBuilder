using Shared_Library.Models;

namespace PokemonTeamBuilder.Api.Repositories
{
	public interface ITeamRepository
	{
		public Task<IEnumerable<Team>> GetAllTeamBasicInfoAsync(string userName);
		public void CreateNewTeam(Team team);

		public void UpdateTeam(Team team);

		public Task<Team> GetTeamAsync(int teamId);
	}
}
