using System;

namespace FamousQuoteQuiz.Data.Entities._Base
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}