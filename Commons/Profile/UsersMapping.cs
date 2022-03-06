
using AutoMapper;
using Commons.Profile.Enity;
using Data;
namespace Commons.Profile
{
    public class UsersMapping : AutoMapper.Profile
    {
        public UsersMapping()
        {
            CreateMap<Users, UsersEntity>().ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id));

        }
    }
}
