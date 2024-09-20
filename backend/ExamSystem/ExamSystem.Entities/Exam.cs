using System;

namespace ExamSystem.Entities
{
    public class Exam : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public DateTime ExamDate { get; set; }  
    }
}
