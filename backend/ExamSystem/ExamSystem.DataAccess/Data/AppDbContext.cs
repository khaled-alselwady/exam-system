using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        protected AppDbContext() : base("ExamSystemDbContext")
        {
        }
    }
}
