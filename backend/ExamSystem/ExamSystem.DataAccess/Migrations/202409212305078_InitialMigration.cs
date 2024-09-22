namespace ExamSystem.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Results", new[] { "ResultId" });
            //DropIndex("dbo.Questions", new[] { "QuestionId" });
            //DropIndex("dbo.Options", new[] { "QuestionId" });
            //DropColumn("dbo.Results", "Id");
            //DropColumn("dbo.Questions", "CorrectOptionId");
            //RenameColumn(table: "dbo.Results", name: "ResultId", newName: "Id");
            //RenameColumn(table: "dbo.Questions", name: "QuestionId", newName: "CorrectOptionId");
            //DropPrimaryKey("dbo.Results");
            //AlterColumn("dbo.Results", "Id", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.Results", "Id");
            CreateIndex("dbo.Results", "Id");
            CreateIndex("dbo.Students", "Email", unique: true, name: "IX_Unique_Email");
            //CreateIndex("dbo.Options", "QuestionId");
            CreateIndex("dbo.Subjects", "Name", unique: true, name: "IX_Unique_SubjectName");
            //DropColumn("dbo.Exams", "ResultId");
        }

        public override void Down()
        {
            AddColumn("dbo.Exams", "ResultId", c => c.Int());
            DropIndex("dbo.Subjects", "IX_Unique_SubjectName");
            DropIndex("dbo.Options", new[] { "QuestionId" });
            DropIndex("dbo.Students", "IX_Unique_Email");
            DropIndex("dbo.Results", new[] { "Id" });
            DropPrimaryKey("dbo.Results");
            AlterColumn("dbo.Results", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Results", "Id");
            RenameColumn(table: "dbo.Questions", name: "CorrectOptionId", newName: "QuestionId");
            RenameColumn(table: "dbo.Results", name: "Id", newName: "ResultId");
            AddColumn("dbo.Questions", "CorrectOptionId", c => c.Int(nullable: false));
            AddColumn("dbo.Results", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Options", "QuestionId");
            CreateIndex("dbo.Questions", "QuestionId");
            CreateIndex("dbo.Results", "ResultId");
        }
    }
}
