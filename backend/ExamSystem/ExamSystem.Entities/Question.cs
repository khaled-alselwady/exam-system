namespace ExamSystem.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int CorrectOptionId { get; set; }
    }
}
