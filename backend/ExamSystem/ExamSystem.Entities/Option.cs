namespace ExamSystem.Entities
{
    public class Option : BaseEntity
    {
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }  
    }
}
