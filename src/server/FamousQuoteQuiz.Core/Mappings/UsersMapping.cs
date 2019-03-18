using AutoMapper;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Data.Entities;

namespace FamousQuoteQuiz.Core.Mappings
{
    public class UsersMapping : Profile
    {
        public UsersMapping()
        {
            CreateMap<User, UserModel>(MemberList.Destination);

            CreateMap<RegisterUserModel, User>(MemberList.Source)
                .ForMember(d => d.UserName, opts => opts.MapFrom(s => s.Email))
                .ForSourceMember(s => s.Password, opts => opts.Ignore());
        }
    }
}
