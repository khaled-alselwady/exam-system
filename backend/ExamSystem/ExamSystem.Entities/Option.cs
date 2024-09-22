namespace ExamSystem.Entities
{
    public class Option : BaseEntity
    {
        public string Text { get; set; }

        // Many-to-One relationship with Question
        public int QuestionId { get; set; } // FK for the one-to-many relationship
        public Question Question { get; set; }
    }
}
