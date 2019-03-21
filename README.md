# FamousQuoteQuiz
Quiz game system asks questions and user should try to pick a correct answer. 
Depending on selected mode user will have to choose correct answer from a list of
answers, or simply to answer with Yes/No to the question.

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
1. You'll need version `2.1.4` of the [`.NET Core SDK`](https://dotnet.microsoft.com/download).

2. If not, you'll need to have [SQLServer](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) either installed locally or at least have some instance available to set up the connection strings.

### Running Using Visual Studio

1. Download it via `CLONE OR DOWNLOAD BUTTON`
2. Unzip the project
3. Open the `.sln` file using Visual Studio
4. Set up the connection strings inside `FamousQuoteQuiz.Api/appsettings.Development.json` (or leave the set)
5. Execute `Update-Database` inside the `Package Manager Console`
6. Run the FamousQuoteQuiz.Api
7. Go to src/client/jQuery.Client/index.html
8. Have FUN!!!

## Technology Stack:
- [x] C# 7.1
- [x] .NET Core Web API v2.1
- [x] EntityFramework Core with SQL Server and ASP.NET Identity
- [x] jQuery v3.1
- [x] jQuery Fancybox
- [x] Bootstrap v3.3
- [x] Database Diagram
![alt text](https://raw.githubusercontent.com/profjordanov/FamousQuoteQuiz/master/resources/database-diagram.PNG)

### Test Suite
- [x] SQL Database Integration Testing
- [x] Arrange Act Assert Pattern
- [x] xUnit
- [x] Autofixture
- [x] Moq
- [x] Shouldly

## Features:

### Web API
- [x] AutoMapper
- [x] File logging with Serilog
- [x] JWT authentication/authorization
- [x] Stylecop
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

### jQuery Client
- [x] Mobile-First Responsive Design
- [x] Works correctly in the latest HTML5-compatible browsers: Chrome, Firefox, Edge, Opera, Safari 
- [x] Followed the best practices for high-quality HTML and CSS: good formatting, good code structure, consistent naming, semantic HTML, correct usage of classes, etc.
