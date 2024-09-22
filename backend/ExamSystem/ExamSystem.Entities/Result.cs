using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystem.Entities
{
    public class Result
    {
        [Key, ForeignKey("Exam")]
        public int Id { get; set; }
        public decimal Score { get; set; }
        public bool IsPassed { get; set; }
        public Exam Exam { get; set; }
    }
}
