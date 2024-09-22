using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class OptionConfiguration : EntityTypeConfiguration<Option>
    {
        public OptionConfiguration()
        {
            HasKey(o => o.Id);

            Property(o => o.Text)
                .HasMaxLength(255)
                .HasColumnType("nvarchar");
        }
    }
}
