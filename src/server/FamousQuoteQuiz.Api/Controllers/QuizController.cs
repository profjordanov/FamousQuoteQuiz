using FamousQuoteQuiz.Core;
using FamousQuoteQuiz.Core.Models;
using FamousQuoteQuiz.Core.Services;
using FamousQuoteQuiz.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Api.Controllers
{
    /// <summary>
    /// A controller that manages the quiz game.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ApiController
    {
        private readonly IQuizQuestionService _quizQuestionService;

        public QuizController(IQuizQuestionService quizQuestionService)
        {
            _quizQuestionService = quizQuestionService;
        }

        /// <summary>
        /// Gets last BinaryChoiceQuestion.
        /// </summary>
        /// <returns><see cref="BinaryChoiceQuestion"/></returns>
        /// <response code="200">If there is any <see cref="BinaryChoiceQuestion"/>.</response>
        /// <response code="404">No questions found.</response>
        [HttpGet]
        [Route("last-binary-choice-question")]
        [ProducesResponseType(typeof(BinaryChoiceQuestion), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLastBinaryChoiceQuestion() =>
            (await _quizQuestionService.GetLastBinaryChoiceQuestionAsync())
            .Match(Ok, NotFound);

        /// <summary>
        /// Gets BinaryChoiceQuestion whose ID is bigger the one received.
        /// </summary>
        /// <param name="initialId">
        /// ID of BinaryChoiceQuestion which has to be surpassed.
        /// </param>
        /// <returns><see cref="BinaryChoiceQuestionViewModel"/></returns>
        /// <response code="200">If such question exists.</response>
        /// <response code="400">No existing questions or no more than received.</response>
        [HttpGet]
        [Route("binary-choice-question")]
        [ProducesResponseType(typeof(BinaryChoiceQuestionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBinaryChoiceQuestion([FromQuery] long initialId) =>
            (await _quizQuestionService.GetBinaryChoiceQuestionAsync(initialId))
            .Match(Ok, Error);

        /// <summary>
        ///  Gets last <see cref="MultipleChoiceQuestion"/>.
        /// </summary>
        /// <returns><see cref="MultipleChoiceQuestion"/></returns>
        /// <response code="200">If there is any <see cref="MultipleChoiceQuestion"/>.</response>
        /// <response code="404">No questions found.</response>
        [HttpGet]
        [Route("last-multiple-choice-question")]
        [ProducesResponseType(typeof(MultipleChoiceQuestion), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetLastMultipleChoiceQuestion() =>
            (await _quizQuestionService.GetLastMultipleChoiceQuestionAsync())
            .Match(Ok, Error);

        /// <summary>
        /// Gets <see cref="MultipleChoiceQuestion"/> whose ID is bigger the one received.
        /// </summary>
        /// <param name="initialId">
        /// ID of BinaryChoiceQuestion which has to be surpassed.
        /// </param>
        /// <returns><see cref="MultipleChoiceQuizQuestionViewModel"/></returns>
        /// <response code="200">If such question exists.</response>
        /// <response code="400">No existing questions or no more than received.</response>
        [HttpGet]
        [Route("multiple-choice-question")]
        [ProducesResponseType(typeof(MultipleChoiceQuizQuestionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetMultipleChoiceQuestion([FromQuery] long initialId) =>
            (await _quizQuestionService.GetMultipleChoiceQuizQuestionAsync(initialId))
            .Match(Ok, Error);
    }
}