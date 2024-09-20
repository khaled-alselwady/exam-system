namespace ExamSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exams",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    SubjectId = c.Byte(nullable: false),
                    ExamDate = c.DateTime(nullable: false, defaultValue: DateTime.Now),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);

            CreateTable(
                "dbo.Results",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ExamId = c.Int(nullable: false),
                    Score = c.Decimal(nullable: false, precision: 5, scale: 2),
                    IsPassed = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Students",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 100),
                    Email = c.String(nullable: false, maxLength: 200, unicode: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.StudentAnswers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ExamId = c.Int(nullable: false),
                    QuestionId = c.Int(nullable: false),
                    SelectedOptionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Options", t => t.SelectedOptionId)
                .Index(t => t.ExamId)
                .Index(t => t.QuestionId)
                .Index(t => t.SelectedOptionId);

            CreateTable(
                "dbo.Questions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Text = c.String(nullable: false),
                    SubjectId = c.Byte(nullable: false),
                    CorrectOptionId = c.Int(nullable: false),
                    QuestionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Options", t => t.QuestionId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId)
                .Index(t => t.SubjectId)
                .Index(t => t.CorrectOptionId)
                .Index(t => t.QuestionId);

            CreateTable(
                "dbo.Options",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Text = c.String(maxLength: 255),
                    QuestionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);

            CreateTable(
                "dbo.Subjects",
                c => new
                {
                    Id = c.Byte(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Exams", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentAnswers", "SelectedOptionId", "dbo.Options");
            DropForeignKey("dbo.StudentAnswers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Options", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "QuestionId", "dbo.Options");
            DropForeignKey("dbo.StudentAnswers", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Results", "Id", "dbo.Exams");
            DropIndex("dbo.Options", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "CorrectOptionId" });
            DropIndex("dbo.Questions", new[] { "SubjectId" });
            DropIndex("dbo.StudentAnswers", new[] { "SelectedOptionId" });
            DropIndex("dbo.StudentAnswers", new[] { "QuestionId" });
            DropIndex("dbo.StudentAnswers", new[] { "ExamId" });
            DropIndex("dbo.Results", new[] { "Id" });
            DropIndex("dbo.Exams", new[] { "SubjectId" });
            DropIndex("dbo.Exams", new[] { "StudentId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Options");
            DropTable("dbo.Questions");
            DropTable("dbo.StudentAnswers");
            DropTable("dbo.Students");
            DropTable("dbo.Results");
            DropTable("dbo.Exams");
        }
    }
}
