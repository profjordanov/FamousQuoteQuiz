using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FamousQuoteQuiz.Business.Services;
using FamousQuoteQuiz.Data.Entities;
using FamousQuoteQuiz.Data.EntityFramework;
using Shouldly;
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
	}
}