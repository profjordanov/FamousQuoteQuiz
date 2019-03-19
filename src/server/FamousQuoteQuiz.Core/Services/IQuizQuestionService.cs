using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Data.Entities;
using Optional;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Core.Services
{
    public interface IQuizQuestionService
    {
	    Task<Option<BinaryChoiceQuestion, Error>> GetLastBinaryChoiceQuestionAsync();
	    Task<Option<MultipleChoiceQuestion, Error>> GetLastMultipleChoiceQuestionAsync();
		Task<Option<BinaryChoiceQuestionViewModel, Error>> GetBinaryChoiceQuestionAsync(long initialId = 0);
        Task<Option<MultipleChoiceQuizQuestionViewModel, Error>> GetMultipleChoiceQuizQuestionAsync(long initialId = 0);
    }
}