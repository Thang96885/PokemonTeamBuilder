using PokeApiNet;

namespace PokemonTeamBuilder.Client.Helper
{
	public static class PokemonSearchHelper<T> where T : NamedApiResource
	{
		public static async Task<IEnumerable<string>> SearchName(string searchName, List<NamedApiResource<T>> ItemList, CancellationToken token)
		{
			return await Task.Run<IEnumerable<string>>(() =>
			{
				if (string.IsNullOrEmpty(searchName))
				{
					return ItemList.Select(x => x.Name);
				}
				return ItemList.Where(x => x.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Name);
			}, token);
		}
	}
}
