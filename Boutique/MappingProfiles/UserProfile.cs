using AutoMapper;
using Boutique.Entities.Concrate;
using Boutique.Models;

namespace Boutique.MappingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.UserName, member2 => member2.MapFrom(user => user.Email.Substring(0, user.Email.IndexOf("@"))));
        }
    }
}
