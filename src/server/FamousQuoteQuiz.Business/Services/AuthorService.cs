using FamousQuoteQuiz.Business.Services._Base;
using FamousQuoteQuiz.Core.Generators;
using FamousQuoteQuiz.Core.Services;
using FamousQuoteQuiz.Data.Entities;
using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Business.Services
{
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly IRandomGenerator _randomGenerator;

        public AuthorService(
            ApplicationDbContext dbContext,
            IRandomGenerator randomGenerator)
            : base(dbContext)
        {
            _randomGenerator = randomGenerator;
        }

        public async Task<Author> GetRandomAuthorOutsideRange(params long[] usedIds)
        {
            var availableIds = await DbContext
                .Authors
                .Select(author => author.Id)
                .Except(usedIds)
                .ToArrayAsync();

            var randomAvailableNumber = _randomGenerator.GetRandomNumber(0, availableIds.Length);
            var randomAutorId = availableIds[randomAvailableNumber];

            return await DbContext.Authors.FindAsync(randomAutorId);
        }
    }
}