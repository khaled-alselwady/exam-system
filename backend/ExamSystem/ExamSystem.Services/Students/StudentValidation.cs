using ExamSystem.Entities;
using System.Threading.Tasks;

namespace ExamSystem.Services.Students
{
    public class StudentValidation : IValidation<Student, StudentService>
    {
        public async Task<bool> IsValid(Student student, StudentService studentService)
        {
            bool isValid = ValidationHelper.IsNotEmpty(student.Email)

                           && (student.Email.Length <= 200)

                           && await ValidationHelper.ValidateAsync(
                               entity: student,
                               valueCheck: (s) => studentService.NotExistsByEmail(s.Email));


            return isValid;
        }
    }
}
