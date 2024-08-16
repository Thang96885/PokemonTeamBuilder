using PokeApiNet;
using System.Net.Http.Json;

namespace PokemonTeamBuilder.Client.Services
{
    public interface IPokemonService
    {
        public Task<NamedApiResourceList<Pokemon>> GetAllPokemonAsync();
        public Task<Pokemon> GetPokemonInfoByNameAsync(string name);
    }

    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _client;

        public PokemonService(HttpClient client)
        {
            _client = client;
        }
        public async Task<NamedApiResourceList<Pokemon>> GetAllPokemonAsync()
        {
            var result = await _client.GetFromJsonAsync<NamedApiResourceList<Pokemon>>("/GetAllPokemon");
            return result;
        }

        public Task<Pokemon> GetPokemonInfoByNameAsync(string name)
        {
            name = name.ToLower();
            var pokemon = _client.GetFromJsonAsync<Pokemon>($"/GetPokemonInfoByName/{name}");
            return pokemon;
        }
    }
}
