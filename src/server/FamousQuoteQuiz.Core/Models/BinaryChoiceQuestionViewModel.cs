namespace FamousQuoteQuiz.Core.Models
{
    public class BinaryChoiceQuestionViewModel : QuizQuestionViewModel
    {
        public long AuthorId { get; set; }
        public virtual string Author { get; set; }

        public bool IsTrue { get; set; }
    }
}