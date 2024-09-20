using System;
using System.Collections.Generic;

namespace ExamSystem.Entities
{
    public class Exam : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime ExamDate { get; set; }

        public ICollection<StudentAnswer> StudentAnswers { get; set; }
        public Result Result { get; set; } 
    }
}
