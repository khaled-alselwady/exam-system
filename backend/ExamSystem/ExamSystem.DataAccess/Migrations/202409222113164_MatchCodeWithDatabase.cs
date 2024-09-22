namespace ExamSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchCodeWithDatabase : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Results", "ExamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Results", "ExamId", c => c.Int(nullable: false));
        }
    }
}
