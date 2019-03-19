using FamousQuoteQuiz.Core;
using Microsoft.AspNetCore.Mvc;

namespace FamousQuoteQuiz.Api.Controllers
{
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        protected IActionResult Error(Error error) =>
            new BadRequestObjectResult(error);

        protected IActionResult NotFound(Error error) =>
	        new NotFoundObjectResult(error);
	}
}
