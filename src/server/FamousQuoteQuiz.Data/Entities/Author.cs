using FamousQuoteQuiz.Data.Entities._Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Data.Entities
{
    public class Author : BaseDeletableModel<long>
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<BinaryChoiceQuestion> BinaryChoiceQuestions { get; set; }
            = new HashSet<BinaryChoiceQuestion>();

        public virtual ICollection<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
            = new HashSet<MultipleChoiceQuestion>();
    }
}