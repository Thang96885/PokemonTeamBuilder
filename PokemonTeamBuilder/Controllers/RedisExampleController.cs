using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared_Library;
using StackExchange.Redis;

namespace PokedexAppUseRedis.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = "Bearer", Roles = nameof(RoleEnum.Admin))]
	public class RedisExampleController : ControllerBase
	{
		private readonly IConnectionMultiplexer _redis;
		private readonly IDatabase _db;

		public RedisExampleController(IConnectionMultiplexer redis)
		{
			_redis = redis;
			_db = _redis.GetDatabase();
		}

		[HttpGet]
		public async Task<IActionResult> GetSampleJsonData([FromQuery]int id)
		{
			var jsonData = await _db.StringGetAsync(id.ToString());
			return Ok("Hello");
		}
	}
}
