using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.Api.Repositories;
using Shared_Library;
using Shared_Library.Data;
using Shared_Library.Dto;
using Shared_Library.Models;
using StackExchange.Redis;
using System.Security.Cryptography.X509Certificates;

namespace PokemonTeamBuilder.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class TeamController : ControllerBase
	{
		private readonly PokemonTeamBuilderContext _context;
		private readonly IConnectionMultiplexer _redis;
		private readonly IDatabase _redisDb;
		private readonly UserManager<User> _userManager;
		private readonly UnitOfWork _unitOfWork;

		public TeamController(PokemonTeamBuilderContext context, IConnectionMultiplexer redis, UserManager<User> userManager, UnitOfWork unitOfWork)
		{
			_context = context;
			_redis = redis;
			_redisDb = _redis.GetDatabase();
			_userManager = userManager;
			_unitOfWork = unitOfWork;
		}

		[HttpGet("/GetAllTeam")]
		public async Task<IActionResult> GetAllTeam([FromQuery] string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			var team = await _unitOfWork.TeamRepository.GetAllTeamBasicInfoAsync(user.Id);
			if(team == null)
			{
				return NotFound();
			}
			return Ok(team);
		}

		[HttpGet("/GetPokemonSetupInTeam")]
		public async Task<IActionResult> GetPokemonSetupInTeam([FromQuery] int teamId)
		{
			var pokemonSetup = await _unitOfWork.PokemonSetupRepository.GetPokemonSetupInTeam(teamId);
			if(pokemonSetup == null)
			{
				return NotFound();
			}	

			return Ok(pokemonSetup);
		}

		[HttpPost("/CreateTeam")] 
		public async Task<IActionResult> CreateNewTeam([FromBody] TeamCreateRequestDto teamInfo)
		{
			var result = new TeamCreateResultDto
			{
				IsSuccess = false,
				ErrorMessage = "Create team failed",
			};
			var user = await _userManager.FindByNameAsync(teamInfo.UserName);
			if(user == null)
			{
				return BadRequest(result);
			}
			var team = new Team
			{
				TeamName = teamInfo.TeamName,
				User = user,
			};
			_unitOfWork.TeamRepository.CreateNewTeam(team);
			var saveResult = await _unitOfWork.SaveChangeAsync();
			if(saveResult == 0)
			{
				return BadRequest(result);
			}
			team.User = null;
			result.IsSuccess = true;
			result.Team = team;
			result.ErrorMessage = "";
			return Ok(result);
		}

		[HttpPost("/AddPokemonSetup")]
		public async Task<IActionResult> AddPokemonSetupToTeam([FromBody] AddPokemonSetupRequest requestInfo)
		{
			var team = await _unitOfWork.TeamRepository.GetTeamAsync(requestInfo.TeamId);
			if (team == null)
			{
				return NotFound();
			}
			foreach(var pokemon in requestInfo.PokemonSetupList)
			{
				foreach(var type in pokemon.Types)
				{
					type.Id = (await _unitOfWork.TypeRepository.GetTypeByName(type.Name)).Id;
				}	
			}

			await _unitOfWork.PokemonSetupRepository.AddPokemonSetupToTeam(requestInfo.PokemonSetupList, team.Id);

			var result = await _unitOfWork.SaveChangeAsync();
			if (result == 0)
			{
				return BadRequest();
			}
			return Ok();
		}
	}
}
