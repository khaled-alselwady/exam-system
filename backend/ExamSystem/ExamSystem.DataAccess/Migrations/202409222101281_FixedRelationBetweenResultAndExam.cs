using System.Data.Entity.Migrations;

public partial class FixedRelationBetweenResultAndExam : DbMigration
{
    public override void Up()
    {
        // Drop the existing foreign key constraint
        DropForeignKey("dbo.Results", "ExamId", "dbo.Exams");

        // Drop the ExamId column from the Results table
        DropColumn("dbo.Results", "ExamId");

        // Create a new foreign key relationship with the Id column
        AddForeignKey("dbo.Results", "Id", "dbo.Exams", "Id");
    }

    public override void Down()
    {
        // Recreate the ExamId column (if rolling back)
        AddColumn("dbo.Results", "ExamId", c => c.Int(nullable: false));

        // Recreate the original foreign key relationship
        AddForeignKey("dbo.Results", "ExamId", "dbo.Exams", "Id");

        // Optionally drop the new foreign key if rolling back
        DropForeignKey("dbo.Results", "Id", "dbo.Exams");
    }
}
