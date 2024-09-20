using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class StudentAnswerConfiguration : EntityTypeConfiguration<StudentAnswer>
    {
        public StudentAnswerConfiguration()
        {
            HasKey(s => s.Id);

            HasRequired(sa => sa.Exam)
                 .WithMany(e => e.StudentAnswers)
                 .HasForeignKey(sa => sa.ExamId)
                 .WillCascadeOnDelete(false);
        
            HasRequired(sa => sa.Question)
                .WithMany() 
                .HasForeignKey(sa => sa.QuestionId) 
                .WillCascadeOnDelete(false);

            HasRequired(sa => sa.SelectedOption)
                .WithMany() 
                .HasForeignKey(sa => sa.SelectedOptionId)
                .WillCascadeOnDelete(false);
        }
    }
}
