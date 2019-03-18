using System.Collections.Generic;

namespace FamousQuoteQuiz.Core.Models
{
    public class MultipleChoiceQuizQuestionViewModel : QuizQuestionViewModel
    {
        public long CorrectAuthorId { get; set; }

        public IEnumerable<AuthorViewModel> Authors { get; set; }
    }
}