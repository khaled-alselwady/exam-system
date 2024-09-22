namespace ExamSystem.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixesAutoGeneratePKResult : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Results", new[] { "Id" });
            DropPrimaryKey("dbo.Results");
            DropForeignKey("dbo.Results", "ExamId", "dbo.Exams");
            AlterColumn("dbo.Results", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Results", "Id");
            AddForeignKey("dbo.Results", "Id", "dbo.Exams", "Id");
            CreateIndex("dbo.Results", "Id");
        }

        public override void Down()
        {
            DropIndex("dbo.Results", new[] { "Id" });
            DropPrimaryKey("dbo.Results");
            AlterColumn("dbo.Results", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Results", "Id");
            CreateIndex("dbo.Results", "Id");
        }
    }
}
