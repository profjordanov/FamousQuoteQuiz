using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FamousQuoteQuiz.Business.Tests
{
    public static class DbContextProvider
    {
        private const string TestDbConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;" +
            "Database=FamousQuoteQuizDb;" +
            "Integrated Security=True;" +
            "Trusted_Connection=True;" +
            "MultipleActiveResultSets=true;";

        public static ApplicationDbContext GetSqlServerDbContext(string connectionString = TestDbConnectionString) =>
            new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString: connectionString)
                .Options);
    }
}
