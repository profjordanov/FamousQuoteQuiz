using FamousQuoteQuiz.Business.Services._Base;
using FamousQuoteQuiz.Core;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Core.Services;
using FamousQuoteQuiz.Data.Entities;
using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;
using Optional.Async;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Business.Services
{
    public class QuizQuestionService : BaseService, IQuizQuestionService
    {
        public QuizQuestionService(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<Option<BinaryChoiceQuestion, Error>> GetLastBinaryChoiceQuestionAsync() =>
             DbContext
                .BinaryChoiceQuestions
                .LastOrDefaultAsync()
                .SomeNotNull(new Error($"Something went wrong with {nameof(BinaryChoiceQuestion)} !"));

        public Task<Option<MultipleChoiceQuestion, Error>> GetLastMultipleChoiceQuestionAsync() =>
            DbContext
                .MultipleChoiceQuestions
                .LastOrDefaultAsync()
                .SomeNotNull(new Error($"Something went wrong with {nameof(MultipleChoiceQuestion)} !"));

        public Task<Option<BinaryChoiceQuestionViewModel, Error>> GetBinaryChoiceQuestionAsync(long initialId = 0) =>
            GetLastBinaryChoiceQuestionAsync().FlatMapAsync(async lastQuestion =>
            {
                if (initialId >= lastQuestion.Id)
                {
                    return Option.None<BinaryChoiceQuestionViewModel, Error>(
                        new Error("Question limit has been reached!"));
                }

                return (await DbContext
                        .BinaryChoiceQuestions
                        .AsNoTracking()
                        .Include(question => question.QuestionableAuthor)
                        .Include(question => question.Quote)
                        .Where(question => question.Id > initialId)
                        .Select(question => new BinaryChoiceQuestionViewModel
                        {
                            Id = question.Id,
                            QuoteId = question.QuoteId,
                            Quote = question.Quote.Content,
                            QuestionableAuthorId = question.QuestionableAuthorId,
                            QuestionableAuthor = question.QuestionableAuthor.Name,
                            CorrectAuthorId = question.CorrectAuthorId,
                            CorrectAuthor = question.CorrectAuthor.Name,
                            IsTrue = question.IsTrue
                        })
                        .FirstOrDefaultAsync())
                    .Some<BinaryChoiceQuestionViewModel, Error>();
            });

        public Task<Option<MultipleChoiceQuizQuestionViewModel, Error>> GetMultipleChoiceQuizQuestionAsync(long initialId = 0) =>
            GetLastMultipleChoiceQuestionAsync().FlatMapAsync(async lastQuestion =>
            {
                if (initialId >= lastQuestion.Id)
                {
                    return Option.None<MultipleChoiceQuizQuestionViewModel, Error>(
                        new Error("Question limit has been reached!"));
                }

                var multipleChoiceQuestion = await DbContext
                    .MultipleChoiceQuestions
                    .AsNoTracking()
                    .Include(question => question.Quote)
                    .Include(question => question.CorrectAuthor)
                    .Include(question => question.Answers)
                    .ThenInclude(answer => answer.AuthorChoice)
                    .Where(question => question.Id > initialId)
                    .FirstOrDefaultAsync();

                return new MultipleChoiceQuizQuestionViewModel
                    {
                        Id = multipleChoiceQuestion.Id,
                        QuoteId = multipleChoiceQuestion.QuoteId,
                        Quote = multipleChoiceQuestion.Quote.Content,
                        CorrectAuthorId = multipleChoiceQuestion.CorrectAuthorId,
                        Authors = new[]
                        {
                            new AuthorViewModel // Correct Choice
                            {
                                Id = multipleChoiceQuestion.CorrectAuthorId,
                                Name = multipleChoiceQuestion.CorrectAuthor.Name
                            },
                            new AuthorViewModel
                            {
                                Id = multipleChoiceQuestion.Answers.FirstOrDefault().AuthorChoice.Id,
                                Name = multipleChoiceQuestion.Answers.FirstOrDefault()?.AuthorChoice.Name,
                            },
                            new AuthorViewModel
                            {
                                Id = multipleChoiceQuestion.Answers.LastOrDefault().AuthorChoice.Id,
                                Name = multipleChoiceQuestion.Answers.LastOrDefault()?.AuthorChoice.Name
                            }
                        }
                    }
                    .Some<MultipleChoiceQuizQuestionViewModel, Error>();
            });
    }
}