using Microsoft.AspNetCore.Identity;
using Shared_Library.Data;
using Shared_Library.Models;

namespace PokedexAppUseRedis.Middleware
{
	public class SeedDataMiddleware
	{
		private readonly RequestDelegate _next;

		public SeedDataMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext http, PokemonTeamBuilderContext context, RoleManager<Role> roleManger)
		{
			var roles = new List<Role>
			{
				new Role{Name = "Admin"},
				new Role {Name = "User"}
			};

			await roleManger.CreateAsync(roles[0]);
			await roleManger.CreateAsync(roles[1]);

			await _next.Invoke(http);
		}
	}
}
