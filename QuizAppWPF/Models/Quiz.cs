namespace QuizAppWPF.Models
{
    public class Quiz
    {
        public int Id { get; set; }                  // Unique ID of the quiz
        public int TopicId { get; set; }             // Foreign key linking to the topic
        public string TopicTitle { get; set; }       // The title/name of the topic (for display)
        public int QuestionCount { get; set; }       // Number of questions in this quiz
    }
}
