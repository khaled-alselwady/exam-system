using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class QuestionConfiguration : EntityTypeConfiguration<Question>
    {
        public QuestionConfiguration()
        {
            HasKey(s => s.Id);

            Property(q => q.Text)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            HasIndex(s => s.SubjectId);
            HasIndex(s => s.CorrectOptionId);

            // question - option
            HasMany(q => q.Options)
             .WithRequired(o => o.Question)
             .HasForeignKey(o => o.QuestionId)
             .WillCascadeOnDelete(false);

            // question - subject
            HasRequired(q => q.Subject)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SubjectId)
                .WillCascadeOnDelete(false);

            // question - correctOption
            HasRequired(q => q.CorrectOption)
            .WithOptional() // CorrectOption does not need to know about Question
            .Map(m => m.MapKey("QuestionId"))
            .WillCascadeOnDelete(false);
        }
    }
}
