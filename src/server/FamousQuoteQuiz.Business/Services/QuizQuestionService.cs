using AutoMapper;
using FamousQuoteQuiz.Business.Extensions;
using FamousQuoteQuiz.Core;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Core.Services;
using FamousQuoteQuiz.Data.Entities;
using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Business.Services
{
    public class QuizQuestionService : IQuizQuestionService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public QuizQuestionService(
            ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Option<BinaryChoiceQuestionViewModel, Error>> 
            GetBinaryChoiceQuestionAsync(long initialId = 0)
        {
            var lastRecord = await _dbContext
                .BinaryChoiceQuestions
                .OrderByDescending(question => question.Id)
                .FirstOrDefaultAsync();

            if (initialId > lastRecord.Id)
            {
                return Option.None<BinaryChoiceQuestionViewModel, Error>(
                    new Error($"Cannot find question with ID bigger than {initialId}!"));
            }

            return (await _dbContext
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
            var lastRecord = await _dbContext
                .MultipleChoiceQuestions
                .OrderByDescending(question => question.Id)
                .FirstOrDefaultAsync();

            if (initialId > lastRecord.Id)
            {
                return Option.None<MultipleChoiceQuizQuestionViewModel, Error>(
                    new Error($"Cannot find question with ID bigger than {initialId}!"));
            }

            return (await _dbContext
                    .MultipleChoiceQuestions
                    .AsNoTracking()
                    .Include(question => question.CorrectAuthor)
                    .Include(question => question.Quote)
                    .Where(question => question.Id > initialId)
                    .Select(question => new MultipleChoiceQuizQuestionViewModel
                    {
                       Id = question.Id,
                        QuoteId = question.QuoteId,
                        Quote = question.Quote.Content,
                        CorrectAuthorId = question.CorrectAuthorId,
                        Authors = new List<AuthorViewModel>
                        {
                            new AuthorViewModel
                            {
                                Id = question.CorrectAuthorId,
                                Name = question.CorrectAuthor.Name
                            },
                            GenerateTwoRandomAuthors(question.CorrectAuthorId, lastRecord.Id)
                                .firstIncorrectAuthor,
                            GenerateTwoRandomAuthors(question.CorrectAuthorId, lastRecord.Id)
                                .secondIncorrectAuthor
                        }
                    })
                    .FirstOrDefaultAsync())
                .Some<MultipleChoiceQuizQuestionViewModel, Error>();
        }

        private (AuthorViewModel firstIncorrectAuthor, AuthorViewModel secondIncorrectAuthor)
            GenerateTwoRandomAuthors(long exceptedAuthorId, long maxId)
        {
            var random = new Random();
            var getRanNum1 = random.NextLong(0, maxId);
            while (getRanNum1 == exceptedAuthorId)
            {
                getRanNum1 = random.NextLong(0, maxId);
            }
            var getRanNum2 = random.NextLong(0, maxId);
            while (getRanNum2 == getRanNum1 && 
                   getRanNum2 == exceptedAuthorId)
            {
                getRanNum2 = random.NextLong(0, maxId);
            }

            var authorX = _dbContext
                .Authors
                .Find(getRanNum1);

            var authorY = _dbContext
                .Authors
                .Find(getRanNum2);

            return (_mapper.Map<Author, AuthorViewModel>(authorX),
                _mapper.Map<Author, AuthorViewModel>(authorY));
        }
    }
}