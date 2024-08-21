using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PokemonTeamBuilder.Client;
using MudBlazor.Services;
using PokemonTeamBuilder.Client.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using PokemonTeamBuilder.Client.Helper;

namespace PokemonTeamBuilder.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");
			builder.Services.AddScoped(HttpClient => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("HttpRootApi").Value)});
			//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddMudServices();

			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddBlazoredSessionStorage();
			builder.Services.AddScoped<AuthHelper>();

			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<NotifyLoginService>();
			builder.Services.AddScoped<IPokemonService, PokemonService>();
			builder.Services.AddScoped<IItemService, ItemService>();
			builder.Services.AddScoped<ITeamService, TeamService>();
			builder.Services.AddScoped<IMoveService, MoveService>();

			await builder.Build().RunAsync();
		}
	}
}
