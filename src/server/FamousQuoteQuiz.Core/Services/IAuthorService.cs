using FamousQuoteQuiz.Data.Entities;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Core.Services
{
    public interface IAuthorService
    {
        Task<Author> GetRandomAuthorOutsideRange(params long[] usedIds);
    }
}