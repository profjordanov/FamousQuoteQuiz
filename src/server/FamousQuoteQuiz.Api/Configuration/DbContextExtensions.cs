using System;
using System.Collections.Generic;
using System.Linq;
using FamousQuoteQuiz.Data.Entities;
using FamousQuoteQuiz.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FamousQuoteQuiz.Api.Configuration
{
    public static class DbContextExtensions
    {
        private const string StephenKingQuote = "I try to create sympathy for my characters, then turn the monsters loose.";
        private const string ErnestHemingwayQuote = "Prose is architecture, not interior decoration.";
        private const string JohnBrownFalseQuote = "It’s none of their business that you have to learn to write. Let them think you were born that way.";
        private const string MarkTwainQuote 
            = "Most writers regard the truth as their most valuable possession, and therefore are most economical in its use.";
        private const string IvanIvanovFalseQuote = "To produce a mighty book, you must choose a mighty theme.";

        

        public static void Seed(this ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();

            if (!EnumerableExtensions.Any(dbContext.Quotes))
            {
                AddQuote(dbContext, StephenKingQuote);
                AddQuote(dbContext, ErnestHemingwayQuote);
                AddQuote(dbContext, JohnBrownFalseQuote);
                AddQuote(dbContext, MarkTwainQuote);
                AddQuote(dbContext, IvanIvanovFalseQuote);

            }

            if (!EnumerableExtensions.Any(dbContext.Authors))
            {
                AddAuthor(dbContext, "Stephen King");
                AddAuthor(dbContext, "Ernest Hemingway");
                AddAuthor(dbContext, "John Brown");
                AddAuthor(dbContext, "Ivan Ivanov");
                AddAuthor(dbContext, "Mark Twain");
            }

            if (!EnumerableExtensions.Any(dbContext.BinaryChoiceQuestions))
            {
                AddBinaryQuestion(dbContext, StephenKingQuote, "Stephen King", true);
                AddBinaryQuestion(dbContext, ErnestHemingwayQuote, "Ernest Hemingway", true);
                AddBinaryQuestion(dbContext, JohnBrownFalseQuote, "John Brown", false);
                AddBinaryQuestion(dbContext, MarkTwainQuote, "Mark Twain", true);
                AddBinaryQuestion(dbContext, IvanIvanovFalseQuote, "Ivan Ivanov", false);

            }

            if (!EnumerableExtensions.Any(dbContext.MultipleChoiceQuestions))
            {
                AddMultipleChoiceQuestion(dbContext, StephenKingQuote, "Stephen King");
                AddMultipleChoiceQuestion(dbContext, MarkTwainQuote, "Mark Twain");
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
            string quote,
            string author,
            bool isTrue)
        {
            var quoteId = dbContext.Quotes.FirstOrDefault(q => q.Content == quote)?.Id;
            var authorId = dbContext.Authors.FirstOrDefault(a => a.Name == author)?.Id;

            var binaryChoiceQuestion = new BinaryChoiceQuestion
            {
                QuoteId = quoteId.Value,
                AuthorId = authorId.Value,
                IsTrue = isTrue
            };

            dbContext.BinaryChoiceQuestions.Add(binaryChoiceQuestion);
            dbContext.SaveChanges(true);
        }

        private static void AddMultipleChoiceQuestion(
            ApplicationDbContext dbContext,
            string quote,
            string correctAuthor)
        {
            var quoteId = dbContext.Quotes.FirstOrDefault(q => q.Content == quote)?.Id;
            var authorId = dbContext.Authors.FirstOrDefault(a => a.Name == correctAuthor)?.Id;

            var firstIncrAuthor = dbContext.Authors.FirstOrDefault(a => a.Id != authorId);
            var secIncrAuthor = dbContext.Authors.FirstOrDefault(a => a.Id != authorId && 
                                                                       a.Id != firstIncrAuthor.Id);

            var multipleChoiceQuestion = new MultipleChoiceQuestion
            {
                QuoteId = quoteId.Value,
                CorrectAuthorId = authorId.Value
            };

            dbContext.MultipleChoiceQuestions.Add(multipleChoiceQuestion);
            dbContext.SaveChanges(true);

            var answerX = new MultipleChoiceAnswer
            {
                MultipleChoiceQuestionId = multipleChoiceQuestion.Id,
                AuthorChoiceId = firstIncrAuthor.Id
            };

            var answerY = new MultipleChoiceAnswer
            {
                MultipleChoiceQuestionId = multipleChoiceQuestion.Id,
                AuthorChoiceId = secIncrAuthor.Id
            };

            dbContext.MultipleChoiceAnswers.AddRange(new List<MultipleChoiceAnswer>
            {
                answerY,
                answerX
            });

            dbContext.SaveChanges();
        }
    }
}