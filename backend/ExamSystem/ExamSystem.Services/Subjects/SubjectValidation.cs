using ExamSystem.Entities;
using System.Threading.Tasks;

namespace ExamSystem.Services.Subjects
{
    public class SubjectValidation : IValidation<Subject, SubjectService>
    {
        public async Task<bool> IsValid(Subject subject, SubjectService subjectService)
        {
            bool isValid = ValidationHelper.IsNotEmpty(subject.Name)

                           && subject.Name.Length <= 50

                           && await ValidationHelper.ValidateAsync(
                               subject,
                               valueCheck: (s) => subjectService.NotExistsByNameAsync(s.Name));

            return isValid;
        }
    }
}
