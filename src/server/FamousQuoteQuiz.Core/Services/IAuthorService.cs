using System.Threading.Tasks;
using FamousQuoteQuiz.Data.Entities;

namespace FamousQuoteQuiz.Core.Services
{
	public interface IAuthorService
	{
		Task<Author> GetRandomAuthorOutsideRange(params long[] usedIds);
	}
}