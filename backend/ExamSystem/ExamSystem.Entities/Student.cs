using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSystem.Entities
{
    public class Student : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
