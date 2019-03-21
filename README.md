# FamousQuoteQuiz

## Technology Stack:
- [x] .Net Core Web Api 2.1
- [x] EntityFramework Core with SQL Server and ASP.NET Identity

## Features:

### Web API
- [x] AutoMapper
- [x] File logging with Serilog
- [x] Neat folder structure

```
├───src
|   |___clients
|       ├───jQuery.Client
│   ├───configuration
│   └───server
│       ├───FamousQuoteQuiz.Api
│       ├───FamousQuoteQuiz.Business
│       ├───FamousQuoteQuiz.Core
│       ├───FamousQuoteQuiz.Data
│       └───FamousQuoteQuiz.Data.EntityFramework
└───tests
    └───FamousQuoteQuiz.Business.Tests

```

- [x] Swagger UI + Fully Documented Controllers <br>
```csharp
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
```

- [x] Global Model Errors Handler <br>
- [x] Global Environment-Dependent Exception Handler <br>
- [x] Neatly organized solution structure <br>
- [x] Thin Controllers <br>
- [x] Robust service layer using the [Either](http://optional-github.com) monad. <br>
