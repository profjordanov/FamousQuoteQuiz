using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FamousQuoteQuiz.Business.Tests
{
    public static class DbContextProvider
    {
        public static ApplicationDbContext GetInMemoryDbContext() =>
            new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Business.Tests").Options);
    }
}
