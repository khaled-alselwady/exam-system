using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }    
    }
}
