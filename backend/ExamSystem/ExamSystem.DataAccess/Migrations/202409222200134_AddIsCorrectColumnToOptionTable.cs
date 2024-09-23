namespace ExamSystem.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIsCorrectColumnToOptionTable : DbMigration
    {
        public override void Up()
        {
            // Drop the foreign key constraint if it exists
            DropForeignKey("dbo.Questions", "CorrectOptionId", "dbo.Options");

            // Drop the index on the foreign key if it exists
            DropIndex("dbo.Questions", new[] { "CorrectOptionId" });

            // Now drop the column
            DropColumn("dbo.Questions", "CorrectOptionId");

            // Finally, add the new column to the Options table
            AddColumn("dbo.Options", "IsCorrect", c => c.Boolean(nullable: false));
        }


        public override void Down()
        {
            AddColumn("dbo.Questions", "CorrectOptionId", c => c.Int(nullable: false));
            DropColumn("dbo.Options", "IsCorrect");
            CreateIndex("dbo.Questions", "CorrectOptionId");
            AddForeignKey("dbo.Questions", "CorrectOptionId", "dbo.Options", "Id");
        }
    }
}
