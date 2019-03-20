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
                .HasIndex(question => new
                {
                    question.QuoteId,
                    question.CorrectAuthorId,
                    question.QuestionableAuthorId
                })
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
                .HasOne(question => question.QuestionableAuthor)
                .WithMany(author => author.QuestionableInBinaryChoiceQuestions)
                .HasForeignKey(question => question.QuestionableAuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<BinaryChoiceQuestion>()
                .HasOne(question => question.CorrectAuthor)
                .WithMany(author => author.CorrectInBinaryChoiceQuestions)
                .HasForeignKey(question => question.CorrectAuthorId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        internal static void ConfigureMultipleChoiceQuestionRelations(this ModelBuilder builder)
        {
            builder
                .Entity<MultipleChoiceQuestion>()
                .HasOne(question => question.Quote)
                .WithMany(quote => quote.MultipleChoiceQuestions)
                .HasForeignKey(question => question.QuoteId);
        }

        internal static void ConfigureCorrectInMultipleChoiceQuestionsRelations(this ModelBuilder builder)
        {
            builder
                .Entity<MultipleChoiceQuestion>()
                .HasOne(question => question.CorrectAuthor)
                .WithMany(quote => quote.CorrectInMultipleChoiceQuestions)
                .HasForeignKey(question => question.CorrectAuthorId);
        }

        internal static void ConfigureMultipleChoiceAnswerRelations(this ModelBuilder builder)
        {
            builder
                .Entity<MultipleChoiceAnswer>()
                .HasOne(question => question.AuthorChoice)
                .WithMany(quote => quote.ChoiceInMultipleAnswers)
                .HasForeignKey(question => question.AuthorChoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}