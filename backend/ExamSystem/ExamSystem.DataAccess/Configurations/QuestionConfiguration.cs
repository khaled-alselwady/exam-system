using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class QuestionConfiguration : EntityTypeConfiguration<Question>
    {
        public QuestionConfiguration()
        {
            HasKey(s => s.Id);

            Property(s => s.Text)
                .IsRequired()
                .HasMaxLength(int.MaxValue)
                .HasColumnType("nvarchar");

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
                .WithRequiredDependent(co => co.Question)
                .Map(m => m.MapKey("CorrectOptionId"));
        }
    }
}
