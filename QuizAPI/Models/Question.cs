namespace QuizAPI.Models
{
    public class Question
    {
        public int Id { get; set; }              // Database primary key
        public string Text { get; set; } = "";   // The question itself
        public List<string> Options { get; set; } = new(); // All possible answers
        public int CorrectIndex { get; set; }    // Index of correct answer in Options
    }
}
