namespace ExamSystem.Entities
{
    public class Result : BaseEntity
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int Score { get; set; }

        public bool IsPassed { get; set; }
    }
}
