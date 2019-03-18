using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FamousQuoteQuiz.Api.Configuration
{
    public static class DbContextExtensions
    {
        public static void Seed(this ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();

            if (!dbContext.Quotes.Any())
            {

            }
        }
    }
}