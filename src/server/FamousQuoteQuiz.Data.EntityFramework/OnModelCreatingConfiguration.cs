using FamousQuoteQuiz.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FamousQuoteQuiz.Data.EntityFramework
{
    internal static class OnModelCreatingConfiguration
    {
        internal static void ConfigureBinaryChoiceQuestionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<BinaryChoiceQuestion>()
                .HasIndex(question => new { question.AuthorId, question.QuoteId, question.IsTrue })
                .IsUnique();

            builder
                .Entity<BinaryChoiceQuestion>()
                .HasOne(question => question.Quote)
                .WithMany(quote => quote.BinaryChoiceQuestions)
                .HasForeignKey(question => question.QuoteId);
        }

        internal static void ConfigureAuthorBinaryChoiceQuestionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<BinaryChoiceQuestion>()
                .HasOne(question => question.Author)
                .WithMany(author => author.BinaryChoiceQuestions)
                .HasForeignKey(question => question.AuthorId);
        }

        internal static void ConfigureMultipleChoiceQuestionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<MultipleChoiceQuestion>()
                .HasOne(question => question.CorrectAuthor)
                .WithMany(author => author.MultipleChoiceQuestions)
                .HasForeignKey(question => question.CorrectAuthorId);

            builder
                .Entity<MultipleChoiceQuestion>()
                .HasOne(question => question.Quote)
                .WithMany(quote => quote.MultipleChoiceQuestions)
                .HasForeignKey(question => question.QuoteId);
        }
    }
}