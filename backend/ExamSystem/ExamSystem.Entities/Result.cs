namespace ExamSystem.Entities
{
    public class Result : BaseEntity
    {
        public decimal Score { get; set; }
        public bool IsPassed { get; set; }
        public Exam Exam { get; set; }
    }
}
