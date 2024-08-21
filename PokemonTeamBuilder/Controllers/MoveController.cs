using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeApiNet;
using PokemonTeamBuilder.Api.Repositories;
using Shared_Library.Data;
using Shared_Library.Models;

namespace PokemonTeamBuilder.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoveController : ControllerBase
	{
		private readonly PokeApiClient _pokeClient;
		private UnitOfWork _unitOfWork;
		public MoveController(PokeApiClient pokeClient, UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_pokeClient = pokeClient;
		}

		[HttpGet("/GetMoveInfoByName/{name}")]
		public async Task<IActionResult> GetMoveByName(string name)
		{
			var move = await _pokeClient.GetResourceAsync<Move>(name);
			if(move == null)
			{
				return NotFound();
			}
			var moveDto = new PokemonMoveChoose
			{
				Name = move.Name,
				Power = move.Power,
			};
			moveDto.Type = await _unitOfWork.TypeRepository.GetTypeByName(move.Type.Name);
			return Ok(moveDto);
		}
	}
}
