using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokeApiNet;
using PokedexAppUseRedis.ClassSupports;
using PokedexAppUseRedis.Middleware;
using Shared_Library.Data;
using Shared_Library.Models;
using StackExchange.Redis;
using System.Net;
using System.Text;

namespace PokedexAppUseRedis
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddControllers();

			builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetSection("Redis:Endpoints:Default").Value));

			builder.Services.AddScoped<PokeApiClient>();

			builder.Services.AddDbContext<PokemonTeamBuilderContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
			});

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateLifetime = true,
					ValidateAudience = false,
					ValidateIssuer = false,
					
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
				};
			});

			builder.Services.AddAuthorization();

			builder.Services.AddIdentity<User, Shared_Library.Models.Role>()
				.AddEntityFrameworkStores<PokemonTeamBuilderContext>()
				.AddDefaultTokenProviders();


			builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));

			


			var app = builder.Build();



			if(app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.Map("/seedData", async Task (RoleManager<Shared_Library.Models.Role> roleManager) =>
			{
				if(roleManager.Roles.Count() == 0)
				{
					await roleManager.CreateAsync(new Shared_Library.Models.Role { Name = "Admin" });
					await roleManager.CreateAsync(new Shared_Library.Models.Role { Name = "User" });
				}	

				
			});

			app.UseRouting();
			
			app.UseAuthentication();
			app.UseAuthorization();



			app.MapControllers();
			

			app.Run();
		}
	}
}
