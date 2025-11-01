using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI.Models
{
    public class Question
    {
        // Primary key
        public int Id { get; set; }

        // The question text
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        // Four possible options
        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        [Required]
        public string OptionC { get; set; }

        [Required]
        public string OptionD { get; set; }

        // The correct answer (e.g., "A", "B", "C", "D")
        [Required]
        [MaxLength(1)]
        public string CorrectOption { get; set; }

        // Foreign key to Topic (which this question belongs to)
        public int TopicId { get; set; }

        [ForeignKey("TopicId")]
        [ValidateNever]
        public Topic Topic { get; set; }
    }
}
