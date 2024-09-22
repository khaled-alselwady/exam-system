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

            // Question - CorrectOption 
            HasRequired(q => q.CorrectOption)
            .WithMany()
            .HasForeignKey(q => q.CorrectOptionId) // FK in Question pointing to CorrectOption
            .WillCascadeOnDelete(false);
        }
    }
}
