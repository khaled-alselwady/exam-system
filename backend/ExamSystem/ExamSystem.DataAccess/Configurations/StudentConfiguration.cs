using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            HasKey(s => s.Id);

            Property(s => s.Name)
                .HasMaxLength(100)
                .HasColumnType("nvarchar");

            Property(s => s.Email)
                .HasMaxLength(200)
                .IsRequired()
                .HasColumnType("varchar");

            HasIndex(s => s.Email)
            .IsUnique()
            .HasName("IX_Unique_Email");
        }
    }
}
