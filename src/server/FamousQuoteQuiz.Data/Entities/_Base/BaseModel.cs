using System;
using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Data.Entities._Base
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}