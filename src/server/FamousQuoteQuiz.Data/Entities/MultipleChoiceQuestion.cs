using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Data.Entities
{
    public class MultipleChoiceQuestion
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long QuoteId { get; set; }
        public virtual Quote Quote { get; set; }

        [Required]
        public long CorrectAuthorId { get; set; }
        public virtual Author CorrectAuthor { get; set; }
    }
}