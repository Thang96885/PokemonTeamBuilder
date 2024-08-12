using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.Api.Repositories;
using Shared_Library.Data;
using Shared_Library.Dto;
using Shared_Library.Models;
using StackExchange.Redis;
using System.Security.Cryptography.X509Certificates;

namespace PokemonTeamBuilder.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
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

		[HttpPost("/CreateTeam")] 
		public async Task<IActionResult> CreateNewTeam([FromBody] TeamCreateRequestDto teamInfo)
		{
			var user = await _userManager.FindByNameAsync(teamInfo.UserName);
			if(user == null)
			{
				return BadRequest("User not fould");
			}
			var team = new Team
			{
				TeamName = teamInfo.TeamName,
				User = user,
			};
			_unitOfWork.TeamRepository.CreateNewTeam(team);
			var result = await _unitOfWork.SaveChangeAsync();
			return Ok();
		}

		[HttpPost("/AddPokemonSetupToTeam")]
		public async Task<IActionResult> AddPokemonSetupToTeam([FromBody] AddPokemonSetupRequest requestInfo)
		{
			var team = await _unitOfWork.TeamRepository.GetTeamAsync(requestInfo.TeamId);
			if (team == null)
			{
				return NotFound();
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
