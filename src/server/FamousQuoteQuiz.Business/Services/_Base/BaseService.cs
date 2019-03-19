using FamousQuoteQuiz.Data.EntityFramework;

namespace FamousQuoteQuiz.Business.Services._Base
{
	public abstract class BaseService
    {
	    protected BaseService(ApplicationDbContext dbContext)
	    {
		    DbContext = dbContext;
	    }

		protected ApplicationDbContext DbContext { get; }
	}
}
