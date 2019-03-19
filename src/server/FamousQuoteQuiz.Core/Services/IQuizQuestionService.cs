using FamousQuoteQuiz.Core.Models;
using Optional;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Core.Services
{
    public interface IQuizQuestionService
    {
        Task<Option<BinaryChoiceQuestionViewModel, Error>>
            GetBinaryChoiceQuestionAsync(long initialId = 0);

        Task<Option<MultipleChoiceQuizQuestionViewModel, Error>>
            GetMultipleChoiceQuizQuestionAsync(long initialId = 0);
    }
}