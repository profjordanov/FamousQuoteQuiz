using AutoMapper;
using FamousQuoteQuiz.Core;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Core.Services;
using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;
using System.Linq;
using System.Threading.Tasks;
using FamousQuoteQuiz.Business.Services._Base;

namespace FamousQuoteQuiz.Business.Services
{
    public class QuizQuestionService : BaseService, IQuizQuestionService
    {
        private readonly IMapper _mapper;
        public QuizQuestionService(ApplicationDbContext dbContext,
	        IMapper mapper)
	        : base(dbContext)
        {
	        _mapper = mapper;
        }

		public async Task<Option<BinaryChoiceQuestionViewModel, Error>> 
            GetBinaryChoiceQuestionAsync(long initialId = 0)
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

        public async Task<Option<MultipleChoiceQuizQuestionViewModel, Error>>
            GetMultipleChoiceQuizQuestionAsync(long initialId = 0)
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

            return (await DbContext
				.MultipleChoiceQuestions
	            .AsNoTracking()
	            .Include(question => question.CorrectAuthor)
	            .Include(question => question.Answers)
	            .ThenInclude(answer => answer.AuthorChoice)
	            .Where(question => question.Id > initialId)
	            .Select(question => new MultipleChoiceQuizQuestionViewModel
	            {
		            Id = question.Id,
		            QuoteId = question.QuoteId,
		            Quote = question.Quote.Content,
		            CorrectAuthorId = question.CorrectAuthorId,
		            Authors = new AuthorViewModel[]
		            {
			            new AuthorViewModel // Correct Choice
			            {
				            Id = question.CorrectAuthorId,
				            Name = question.CorrectAuthor.Name
			            },
			            new AuthorViewModel
			            {
				            Id = question.Answers.FirstOrDefault().AuthorChoice.Id,
				            Name = question.Answers.FirstOrDefault().AuthorChoice.Name,
						},
			            new AuthorViewModel
			            {
				            Id = question.Answers.LastOrDefault().AuthorChoice.Id,
							Name = question.Answers.LastOrDefault().AuthorChoice.Name
						}
		            }
	            })
	            .FirstOrDefaultAsync())
	            .Some<MultipleChoiceQuizQuestionViewModel, Error>();
        }


    }
}