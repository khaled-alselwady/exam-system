using ExamSystem.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class ExamConfiguration : EntityTypeConfiguration<Exam>
    {
        public ExamConfiguration()
        {
            HasKey(s => s.Id);

            HasIndex(s => s.StudentId);
            HasIndex(s => s.SubjectId);

            Property(e => e.ExamDate)
                .HasColumnType("datetime")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            // exam - student
            HasRequired(e => e.Student)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            // exam - subject
            HasRequired(e => e.Subject)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);
        }
    }
}
