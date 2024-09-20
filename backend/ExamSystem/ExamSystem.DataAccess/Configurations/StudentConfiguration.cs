using ExamSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
