namespace FamousQuoteQuiz.Core.Models
{
    public class BinaryChoiceQuestionViewModel : QuizQuestionViewModel
    {
        public long QuestionableAuthorId { get; set; }
        public virtual string QuestionableAuthor { get; set; }

        public long CorrectAuthorId { get; set; }
        public virtual string CorrectAuthor { get; set; }

        public bool IsTrue { get; set; }
    }
}