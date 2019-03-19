using AutoMapper;
using FamousQuoteQuiz.Business.Services._Base;
using FamousQuoteQuiz.Core;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Core.Services;
using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;
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

		public async Task<Option<BinaryChoiceQuestionViewModel, Error>> GetBinaryChoiceQuestionAsync(long initialId = 0)
        {
            var lastRecord = await DbContext
				.BinaryChoiceQuestions
                .OrderByDescending(question => question.Id)
                .FirstOrDefaultAsync();

            if (initialId > lastRecord.Id)
            {
                return Option.None<BinaryChoiceQuestionViewModel, Error>(
                    new Error($"Cannot find question with ID bigger than {initialId}!"));
            }

            return (await DbContext
				.BinaryChoiceQuestions
                .AsNoTracking()
                .Include(question => question.Author)
                .Include(question => question.Quote)
                .Where(question => question.Id > initialId)
                .Select(question => new BinaryChoiceQuestionViewModel
                {
                    Id = question.Id,
                    AuthorId = question.AuthorId,
                    Author = question.Author.Name,
                    QuoteId = question.QuoteId,
                    Quote = question.Quote.Content,
                    IsTrue = question.IsTrue
                })
                .FirstOrDefaultAsync())
                .Some<BinaryChoiceQuestionViewModel, Error>();
        }

        public async Task<Option<MultipleChoiceQuizQuestionViewModel, Error>> GetMultipleChoiceQuizQuestionAsync(long initialId = 0)
        {
            var lastRecord = await DbContext
				.MultipleChoiceQuestions
                .OrderByDescending(question => question.Id)
                .FirstOrDefaultAsync();

            if (initialId > lastRecord.Id)
            {
                return Option.None<MultipleChoiceQuizQuestionViewModel, Error>(
                    new Error($"Cannot find question with ID bigger than {initialId}!"));
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
            }.Some<MultipleChoiceQuizQuestionViewModel, Error>();
        }
    }
}