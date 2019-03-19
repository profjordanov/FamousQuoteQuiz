using FamousQuoteQuiz.Data.Entities;
using FamousQuoteQuiz.Data.Entities._Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<BinaryChoiceQuestion> BinaryChoiceQuestions { get; set; }

        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

        public DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureBinaryChoiceQuestionRelations();
            modelBuilder.ConfigureAuthorBinaryChoiceQuestionRelations();
            modelBuilder.ConfigureMultipleChoiceQuestionRelations();
            modelBuilder.ConfigureCorrectInMultipleChoiceQuestionsRelations();
            modelBuilder.ConfigureMultipleChoiceAnswerRelations();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges() => SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added ||
                     e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
