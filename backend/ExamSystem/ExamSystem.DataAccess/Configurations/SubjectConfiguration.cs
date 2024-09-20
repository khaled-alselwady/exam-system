using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class SubjectConfiguration : EntityTypeConfiguration<Subject>
    {
        public SubjectConfiguration()
        {
            HasKey(s => s.Id);

            Property(s => s.Id)
                 .HasColumnType("tinyint");

            Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");
        }
    }
}
