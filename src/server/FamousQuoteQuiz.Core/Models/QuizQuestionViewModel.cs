namespace FamousQuoteQuiz.Core.Models
{
    public class QuizQuestionViewModel
    {
        public long Id { get; set; }

        public long QuoteId { get; set; }
        public string Quote { get; set; }
    }
}