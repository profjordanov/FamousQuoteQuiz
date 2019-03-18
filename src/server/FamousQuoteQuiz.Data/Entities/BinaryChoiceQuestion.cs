using System.ComponentModel.DataAnnotations;

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
        public long AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [Required]
        public bool IsTrue { get; set; }
    }
}