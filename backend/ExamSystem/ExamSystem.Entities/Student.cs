using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Exam> Exams { get; set; }
    }
}
