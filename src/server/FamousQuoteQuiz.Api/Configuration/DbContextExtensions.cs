using System;
using FamousQuoteQuiz.Data.Entities;
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
                AddQuote(dbContext, "I try to create sympathy for my characters, then turn the monsters loose.");
                AddQuote(dbContext, "Prose is architecture, not interior decoration.");
            }

            if (!dbContext.Authors.Any())
            {
                AddAuthor(dbContext, "Stephen King");
                AddAuthor(dbContext, "Ernest Hemingway");
            }

            if (!dbContext.BinaryChoiceQuestions.Any())
            {
                AddBinaryQuestion(dbContext, 1, 5, true);
                AddBinaryQuestion(dbContext, 2, 6, true);
            }
        }

        private static void AddAuthor(ApplicationDbContext dbContext, string name)
        {
            var author = new Author
            {
                CreatedOn = DateTime.UtcNow,
                Name = name
            };

            dbContext.Authors.Add(author);
            dbContext.SaveChanges(true);
        }

        private static void AddQuote(ApplicationDbContext dbContext, string content)
        {
            var quote = new Quote
            {
                CreatedOn = DateTime.UtcNow,
                Content = content
            };

            dbContext.Quotes.Add(quote);
            dbContext.SaveChanges(true);
        }

        private static void AddBinaryQuestion(
            ApplicationDbContext dbContext,
            long quoteId,
            long authorId,
            bool isTrue)
        {
            var binaryChoiceQuestion = new BinaryChoiceQuestion
            {
                QuoteId = quoteId,
                AuthorId = authorId,
                IsTrue = isTrue
            };

            dbContext.BinaryChoiceQuestions.Add(binaryChoiceQuestion);
            dbContext.SaveChanges(true);
        }

        private static void AddMultipleChoiceQuestion(
            ApplicationDbContext dbContext,
            long quoteId,
            long authorId)
        {
            var multipleChoiceQuestion = new MultipleChoiceQuestion
            {
                QuoteId = quoteId,
                CorrectAuthorId = authorId
            };

            dbContext.MultipleChoiceQuestions.Add(multipleChoiceQuestion);
            dbContext.SaveChanges(true);
        }
    }
}