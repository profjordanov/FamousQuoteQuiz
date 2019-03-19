using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Data.Entities
{
    public class MultipleChoiceAnswer
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long MultipleChoiceQuestionId { get; set; }
        public virtual MultipleChoiceQuestion MultipleChoiceQuestion { get; set; }

        [Required]
        public long AuthorChoiceId { get; set; }
        public virtual Author AuthorChoice { get; set; }
    }
}