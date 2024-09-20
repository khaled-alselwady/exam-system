using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class ResultConfiguration : EntityTypeConfiguration<Result>
    {
        public ResultConfiguration()
        {
            HasKey(r => r.Id);

            Property(r => r.Score)
                .HasColumnType("DECIMAL(5,2)")
                .IsRequired();

            Property(r => r.IsPassed)
                .IsRequired();

            HasRequired(r => r.Exam)
                .WithOptional(e => e.Result)
                .WillCascadeOnDelete(false);
        }
    }
}
