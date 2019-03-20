using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousQuoteQuiz.Data.Entities
{
    public class BinaryChoiceQuestion
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long QuoteId { get; set; }
        public virtual Quote Quote { get; set; }

        [Required]
        public long QuestionableAuthorId { get; set; }
        public virtual Author QuestionableAuthor { get; set; }

        [Required]
        public long CorrectAuthorId { get; set; }
        public virtual Author CorrectAuthor { get; set; }

        [NotMapped]
        public bool IsTrue => QuestionableAuthorId == CorrectAuthorId;
    }
}