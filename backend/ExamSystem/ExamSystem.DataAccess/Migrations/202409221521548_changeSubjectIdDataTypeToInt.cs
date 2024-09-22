namespace ExamSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSubjectIdDataTypeToInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Exams", "SubjectId", "dbo.Subjects");
            DropPrimaryKey("dbo.Subjects");
            AlterColumn("dbo.Subjects", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.Subjects", "Id");
            AddForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects", "Id");
            AddForeignKey("dbo.Exams", "SubjectId", "dbo.Subjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exams", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects");
            DropPrimaryKey("dbo.Subjects");
            AlterColumn("dbo.Subjects", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.Subjects", "Id");
            AddForeignKey("dbo.Exams", "SubjectId", "dbo.Subjects", "Id");
            AddForeignKey("dbo.Questions", "SubjectId", "dbo.Subjects", "Id");
        }
    }
}
