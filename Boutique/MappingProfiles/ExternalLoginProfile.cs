using AutoMapper;
using Boutique.Entities.Concrate;
using Boutique.Models;

namespace Boutique.MappingProfiles
{
    public class ExternalLoginProfile:Profile
    {
        public ExternalLoginProfile()
        {
            CreateMap<ExternalLoginModel, User>()
        .ForMember(x => x.UserName, member2 => member2.MapFrom(user => user.Email.Substring(0, user.Email.IndexOf("@"))));
        }
    }
}
