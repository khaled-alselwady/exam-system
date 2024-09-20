using System.Collections.Generic;

namespace ExamSystem.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int CorrectOptionId { get; set; }
        public Option CorrectOption { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}
