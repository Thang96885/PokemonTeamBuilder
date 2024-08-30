using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared_Library.Data;
using Shared_Library.Dto;
using Shared_Library.Enum;
using Shared_Library.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PokedexAppUseRedis.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;
		private readonly PokemonTeamBuilderContext _context;

		public AuthController(UserManager<User> userManager
			, SignInManager<User> signInManager
			, IMapper mapper
			, PokemonTeamBuilderContext context
			, IConfiguration config
			, RoleManager<Role> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
			_context = context;
			_config = config;
			_roleManager = roleManager;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerInfo)
		{
			if (registerInfo == null && string.IsNullOrEmpty(registerInfo.UserName) && string.IsNullOrEmpty(registerInfo.Password) && string.IsNullOrEmpty(registerInfo.Email))
			{
				return BadRequest("InValid input");
			}

			User user =_mapper.Map<User>(registerInfo);

			var result = await _userManager.CreateAsync(user, registerInfo.Password);
			if (result.Succeeded)
			{
				if(user.UserName == "admin")
				{
					await _userManager.AddToRoleAsync(user, nameof(RoleEnum.Admin));
				}	
				else
					await _userManager.AddToRoleAsync(user, nameof(RoleEnum.User));
				return Ok("User registers successfully");
			}
			else
			{
				return BadRequest(result.Errors);
			}
		}


		[HttpPost("login")]
		public async Task<ActionResult<LoginResultDto>> Login([FromBody] LoginRequestDto loginInfo)
		{
			LoginResultDto result = new LoginResultDto
			{
				IsSuccess = false,
				JwtToken = "",
				RefreshToken = "",
				Message = "Login false",
				UserName = ""
			};
			var user = await FindUser(loginInfo.UserNameOrEmail);
			if(user == null)
			{
				return BadRequest(result);
			}

			

			var loginResult = await _signInManager.PasswordSignInAsync(user, loginInfo.Password, false, false);
			if(loginResult.Succeeded)
			{
				var roles = await _userManager.GetRolesAsync(user);
				user.RefreshToken = CreateRefeshToken();
				user.TimeExpireRefreshToken = DateTime.Now.AddMinutes(5);

				result.JwtToken = CreateJwtToken(user, roles);
				result.IsSuccess = true;
				result.RefreshToken = user.RefreshToken;
				
				await _userManager.UpdateAsync(user);
				result.UserName = user.UserName;
				result.Message = "Login successfully";
				return Ok(result);
			}	
			else
			{
				result.Message = "Login failed";
				return BadRequest(result);
			}
		}

		

		[HttpPost("refresh")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshRequestDto refreshInfo)
		{
			var result = new LoginResultDto
			{
				IsSuccess = false,
				Message = "",
				JwtToken = "",
				RefreshToken = ""
			};
			if (refreshInfo == null || string.IsNullOrEmpty(refreshInfo.JwtToken) || string.IsNullOrEmpty(refreshInfo.RefreshToken))
			{
				result.Message = "Invalid input";
				return Ok(result);
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value);
			var token = tokenHandler.ReadToken(refreshInfo.JwtToken) as JwtSecurityToken;
			if (token == null)
			{
				result.Message = "Invalid input";
				return Ok(result);
			}
			var userName = token.Claims.Where(claim => claim.Type == "unique_name").FirstOrDefault()?.Value;
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null || user.RefreshToken != refreshInfo.RefreshToken || user.TimeExpireRefreshToken < DateTime.Now)
			{
				result.Message = "invalid input";
				return Ok(result);
			}

			var role = await _userManager.GetRolesAsync(user);

			result.IsSuccess = true;
			result.Message = "Refresh token successfully";
			result.JwtToken = CreateJwtToken(user, role);
			user.RefreshToken = CreateRefeshToken();
			user.TimeExpireRefreshToken = DateTime.Now.AddMinutes(5);
			result.RefreshToken = user.RefreshToken;
			await _userManager.UpdateAsync(user);
			return Ok(result);
		}


		private async Task<User> FindUser(string userNameOrEmail)
		{
			User userWithName = await _userManager.FindByNameAsync(userNameOrEmail);
			User userWithEmail = await _userManager.FindByEmailAsync(userNameOrEmail);
			if (userWithName != null)
				return userWithName;
			else if (userWithEmail != null)
				return userWithEmail;
			else
				return null;
		}

		private string CreateJwtToken(User user, IList<string> roles)
		{
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key")!.Value);

			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new System.Security.Claims.ClaimsIdentity(new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.UserName),
				}),

				Issuer = _config.GetSection("Jwt:Issuer").Value,
				Expires = DateTime.Now.AddSeconds(30),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
			};

			foreach(var role in roles)
			{
				tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
			}	

			var token = tokenHandler.CreateToken(tokenDescription);
			return tokenHandler.WriteToken(token);
		}


		private string CreateRefeshToken()
		{
			var randomNumber = new byte[32];
			using(var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
	}
}
