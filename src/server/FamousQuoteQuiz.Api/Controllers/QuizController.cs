using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamousQuoteQuiz.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FamousQuoteQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ApiController
    {
        private readonly IQuizQuestionService _quizQuestionService;

        public QuizController(IQuizQuestionService quizQuestionService)
        {
            _quizQuestionService = quizQuestionService;
        }

        [HttpGet]
        [Route("binary-choice-question")]
        public async Task<IActionResult> GetBinaryChoiceQuestion([FromQuery] long initialId) =>
            (await _quizQuestionService.GetBinaryChoiceQuestionAsync(initialId))
            .Match(Ok, Error);

        [HttpGet]
        [Route("multiple-choice-question")]
        public async Task<IActionResult> GetMultipleChoiceQuestion([FromQuery] long initialId) =>
            (await _quizQuestionService.GetMultipleChoiceQuizQuestionAsync(initialId))
            .Match(Ok, Error);
    }
}