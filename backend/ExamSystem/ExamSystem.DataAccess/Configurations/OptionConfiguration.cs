using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class OptionConfiguration : EntityTypeConfiguration<Option>
    {
        public OptionConfiguration()
        {
            HasKey(s => s.Id);

            Property(s => s.Text)
                .HasMaxLength(255)
                .HasColumnType("nvarchar");

            Property(s => s.QuestionId)
                .IsRequired();

            HasIndex(s => s.QuestionId)
                .IsUnique();
        }
    }
}
