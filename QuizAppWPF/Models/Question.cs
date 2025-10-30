namespace QuizAppWPF.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }

        // Matches the API property name exactly
        public string CorrectOption { get; set; }

        // Foreign key to Topic
        public int TopicId { get; set; }

        // Optional: used when displaying questions with topic names
        public Topic Topic { get; set; }
    }
}
