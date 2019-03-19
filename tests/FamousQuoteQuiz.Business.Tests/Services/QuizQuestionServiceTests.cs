using System;
using AutoFixture;
using AutoFixture.Xunit2;
using FamousQuoteQuiz.Business.Services;
using FamousQuoteQuiz.Data.Entities;
using FamousQuoteQuiz.Data.EntityFramework;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FamousQuoteQuiz.Business.Tests.Services
{
    /// <summary>
    /// SQL Server Database Integration Testing
    /// using xUnit, Shouldly, Arrange Act Assert Pattern.
    /// </summary>
    public class QuizQuestionServiceTests
    {
        private readonly QuizQuestionService _quizQuestionService;
        private readonly ApplicationDbContext _dbContext;

        public QuizQuestionServiceTests()
        {
            _dbContext = DbContextProvider.GetSqlServerDbContext();
            _quizQuestionService = new QuizQuestionService(_dbContext);
        }

        [Theory]
        [AutoData]
        public async Task GetLastBinaryChoiceQuestionAsync_Returns_Correct_Data(Fixture fixture)
        {
            // Arrange
            await CreateNewBinaryChoiceQuestionAsync(fixture);

            // Act
            var result = await _quizQuestionService.GetLastBinaryChoiceQuestionAsync();

            // Assert
            result.HasValue.ShouldBe(true);

            await EraseAllContentAsync();
        }

        [Fact]
        public async Task GetLastBinaryChoiceQuestionAsync_Returns_None_When_There_Are_No_Questions()
        {
            // Arrange
            _dbContext.BinaryChoiceQuestions.RemoveRange(_dbContext.BinaryChoiceQuestions);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _quizQuestionService.GetLastBinaryChoiceQuestionAsync();

            // Assert
            result.HasValue.ShouldBe(false);
            result.MatchNone(error => error.Messages.ShouldAllBe(
                msg => msg == $"Something went wrong with {nameof(BinaryChoiceQuestion)} !"));
        }

        [Theory]
        [AutoData]
        public async Task GetBinaryChoiceQuestionAsync_Returns_None_When_There_Is_No_Expected_Question(Fixture fixture)
        {
            // Arrange
            await CreateNewBinaryChoiceQuestionAsync(fixture);

            // Act
            var result = await _quizQuestionService.GetBinaryChoiceQuestionAsync(long.MaxValue);

            // Assert
            result.HasValue.ShouldBe(false);
            result.MatchNone(error => error.Messages.ShouldAllBe(
                msg => msg == "Question limit has been reached!"));

            await EraseAllContentAsync();
        }

        [Theory]
        [AutoData]
        public async Task GetLastMultipleChoiceQuestionAsync_Returns_Correct_Data(Fixture fixture)
        {
            // Arrange
            await CreateMultipleChoiceQuestionAsync(fixture);

            // Act
            var result = await _quizQuestionService.GetLastMultipleChoiceQuestionAsync();

            // Assert
            result.HasValue.ShouldBe(true);
            await EraseAllContentAsync();
        }

        [Fact]
        public async Task GetLastMultipleChoiceQuestionAsync_Returns_None_When_There_Are_No_Questions()
        {
            // Arrange
            _dbContext.MultipleChoiceQuestions.RemoveRange(_dbContext.MultipleChoiceQuestions);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _quizQuestionService.GetLastMultipleChoiceQuestionAsync();

            // Assert
            result.HasValue.ShouldBe(false);
            result.MatchNone(error => error.Messages.ShouldAllBe(
                msg => msg == $"Something went wrong with {nameof(MultipleChoiceQuestion)} !"));
        }

        private async Task<BinaryChoiceQuestion> CreateNewBinaryChoiceQuestionAsync(Fixture fixture)
        {
            _dbContext.BinaryChoiceQuestions.RemoveRange(_dbContext.BinaryChoiceQuestions);
            await _dbContext.SaveChangesAsync();

            var author = new Author
            {
                Name = fixture.Create<string>()
            };
            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            var quote = new Quote
            {
                Content = fixture.Create<string>()
            };

            await _dbContext.Quotes.AddAsync(quote);
            await _dbContext.SaveChangesAsync();

            var binaryChoiceQuestion = new BinaryChoiceQuestion
            {
                QuoteId = quote.Id,
                AuthorId = author.Id,
                IsTrue = fixture.Create<bool>()
            };

            await _dbContext.BinaryChoiceQuestions.AddAsync(binaryChoiceQuestion);
            await _dbContext.SaveChangesAsync();

            return binaryChoiceQuestion;
        }

        private async Task<MultipleChoiceQuestion> CreateMultipleChoiceQuestionAsync(Fixture fixture)
        {
            _dbContext.MultipleChoiceQuestions.RemoveRange(_dbContext.MultipleChoiceQuestions);
            await _dbContext.SaveChangesAsync();

            var author = new Author
            {
                Name = fixture.Create<string>()
            };
            var authorX = new Author
            {
                Name = fixture.Create<string>()
            };
            var authorY = new Author
            {
                Name = fixture.Create<string>()
            };

            await _dbContext.Authors.AddRangeAsync(author, authorX, authorY);
            await _dbContext.SaveChangesAsync();

            var quote = new Quote
            {
                Content = fixture.Create<string>()
            };

            await _dbContext.Quotes.AddAsync(quote);
            await _dbContext.SaveChangesAsync();

            var multipleChoiceQuestion = new MultipleChoiceQuestion
            {
                QuoteId = quote.Id,
                CorrectAuthorId = author.Id
            };

            await _dbContext.MultipleChoiceQuestions.AddAsync(multipleChoiceQuestion);
            await _dbContext.SaveChangesAsync();

            var answerX = new MultipleChoiceAnswer
            {
                MultipleChoiceQuestionId = multipleChoiceQuestion.Id,
                AuthorChoiceId = authorX.Id
            };

            var answerY = new MultipleChoiceAnswer
            {
                MultipleChoiceQuestionId = multipleChoiceQuestion.Id,
                AuthorChoiceId = authorY.Id
            };

            await _dbContext.MultipleChoiceAnswers.AddRangeAsync(new List<MultipleChoiceAnswer>
            {
                answerY,
                answerX
            });

            await _dbContext.SaveChangesAsync();

            return multipleChoiceQuestion;
        }

        private async Task EraseAllContentAsync()
        {
            _dbContext.Authors.RemoveRange(_dbContext.Authors);
            await _dbContext.SaveChangesAsync();

            _dbContext.Quotes.RemoveRange(_dbContext.Quotes);
            await _dbContext.SaveChangesAsync();

            _dbContext.BinaryChoiceQuestions.RemoveRange(_dbContext.BinaryChoiceQuestions);
            await _dbContext.SaveChangesAsync();

            _dbContext.MultipleChoiceAnswers.RemoveRange(_dbContext.MultipleChoiceAnswers);
            await _dbContext.SaveChangesAsync();

            _dbContext.MultipleChoiceQuestions.RemoveRange(_dbContext.MultipleChoiceQuestions);
            await _dbContext.SaveChangesAsync();
        }
    }
}