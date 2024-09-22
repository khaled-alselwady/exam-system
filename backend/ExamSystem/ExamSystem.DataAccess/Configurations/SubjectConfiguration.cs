using ExamSystem.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class SubjectConfiguration : EntityTypeConfiguration<Subject>
    {
        public SubjectConfiguration()
        {
            HasKey(s => s.Id);

            Property(s => s.Id)
                 .HasColumnType("tinyint")
                 .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            HasIndex(s => s.Name)
                .IsUnique()
                .HasName("IX_Unique_SubjectName");
            ;
        }
    }
}
