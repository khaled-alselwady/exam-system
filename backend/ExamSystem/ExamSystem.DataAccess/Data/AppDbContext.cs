﻿using ExamSystem.DataAccess.Configurations;
using ExamSystem.Entities;
using System.Data.Entity;

namespace ExamSystem.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<Result> Results { get; set; }


        public AppDbContext() : base("name=ExamSystemDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(StudentConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
