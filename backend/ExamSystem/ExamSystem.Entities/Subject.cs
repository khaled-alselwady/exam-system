using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSystem.Entities
{
    public class Subject
    {
        [Key]
        public byte Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
