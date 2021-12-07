using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Users;

namespace WebApi.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, UserModel>()
				.ForMember(d => d.RegistrationReferrerName, 
				opt => opt.MapFrom(x => 
					x.RegistrationReferrer.CanEnterManually 
						? x.CustomUserRegistrationReferrer
						: x.RegistrationReferrer.Name));
			CreateMap<RegisterModel, User>()
				.ForMember(d => d.CustomUserRegistrationReferrer,
				opt => opt.MapFrom(x => x.RegistrationReferrerCustomText));
			CreateMap<UpdateModel, User>();
		}
	}
}