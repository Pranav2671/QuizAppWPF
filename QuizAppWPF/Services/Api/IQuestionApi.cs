using QuizAppWPF.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizAppWPF.Services.Api
{
    public interface IQuestionApi
    {
        // Get all questions
        [Get("/api/Questions")]
        Task<List<Question>> GetAllQuestionsAsync();

        // Get questions by topic
        [Get("/api/Questions/ByTopic/{topicId}")]
        Task<List<Question>> GetQuestionsByTopicAsync(int topicId);

        // Add a new question
        [Post("/api/Questions")]
        Task<ApiResponse<Question>> AddQuestionAsync([Body] Question question);

        // Delete a question
        [Delete("/api/Questions/{id}")]
        Task<ApiResponse<string>> DeleteQuestionAsync(int id);
    }
}
