using AutoMapper;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Data.Entities;

namespace FamousQuoteQuiz.Core.Mappings
{
    public class AuthorsMapping : Profile
    {
        public AuthorsMapping()
        {
            CreateMap<Author, AuthorViewModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));
        }
    }
}