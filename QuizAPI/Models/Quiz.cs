namespace QuizAPI.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int TopicId { get; set; }

        public string TopicTitle { get; set; }
        public int QuestionCount { get; set; }

        public Topic Topic { get; set; } // optional navigation
    }
}
