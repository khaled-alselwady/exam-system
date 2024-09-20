using ExamSystem.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ExamSystem.DataAccess.Configurations
{
    public class ResultConfiguration : EntityTypeConfiguration<Result>
    {
        public ResultConfiguration()
        {
            HasKey(r => r.Id);

            Property(s => s.Score)
                .HasColumnType("decimal")
                .IsRequired();

            Property(r => r.IsPassed)
                .IsRequired();

            HasRequired(r => r.Exam)
                .WithOptional(e => e.Result)
                .Map(m => m.MapKey("ResultId"))
                .WillCascadeOnDelete(false);
        }
    }
}
