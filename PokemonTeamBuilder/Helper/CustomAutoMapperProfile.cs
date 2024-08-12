using AutoMapper;
using Shared_Library.Dto;
using Shared_Library.Models;

namespace PokedexAppUseRedis.ClassSupports
{
	public class CustomAutoMapperProfile : Profile
	{
		public CustomAutoMapperProfile()
		{
			CreateMap<RegisterRequestDto, User>();
			CreateMap<TeamCreateRequestDto, Team>(MemberList.Destination);
		}
	}
}
