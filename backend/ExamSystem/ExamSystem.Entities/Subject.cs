using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Entities
{
    public class Subject : BaseEntity
    {
        public new byte Id {  get; set; }
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();     
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();     
    }
}
