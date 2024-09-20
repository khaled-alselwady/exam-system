namespace ExamSystem.Entities
{
    public class StudentAnswer : BaseEntity
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }  

        public int SelectedOptionId { get; set; }
        public Option SelectedOption { get; set; }
    }
}
