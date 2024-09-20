namespace ExamSystem.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixesRelationBetweenResultAndExam : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Results", "Id", "dbo.Exams");
            DropIndex("dbo.Results", new[] { "Id" });
            DropPrimaryKey("dbo.Results");
            AddColumn("dbo.Exams", "ResultId", c => c.Int());
            AlterColumn("dbo.Results", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Results", "Id");
            AddForeignKey("dbo.Exams", "ResultId", "dbo.Results", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Results", "ResultId", "dbo.Exams");
            DropIndex("dbo.Results", new[] { "ResultId" });
            DropPrimaryKey("dbo.Results");
            AlterColumn("dbo.Results", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Results", "ResultId");
            DropColumn("dbo.Exams", "ResultId");
            AddPrimaryKey("dbo.Results", "Id");
            CreateIndex("dbo.Results", "Id");
            AddForeignKey("dbo.Results", "Id", "dbo.Exams", "Id");
        }
    }
}
