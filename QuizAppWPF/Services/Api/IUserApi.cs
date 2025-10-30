using QuizAppWPF.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizAppWPF.Services.Api
{
    public interface IUserApi
    {
        [Post("/api/users/register")]
        Task<ApiResponse<User>> RegisterAsync([Body] User user);


        [Post("/api/users/login")]
        Task<ApiResponse<User>> LoginAsync([Body] User user);


        [Get("/api/users")]
        Task<List<User>> GetUsersAsync();


        [Delete("/api/users/{id}")]
        Task<ApiResponse<string>> DeleteUserAsync(int id);

        // -------------------- Topic Endpoints --------------------

        [Get("/api/Topics")]
        Task<List<Topic>> GetTopicsAsync();

        [Post("/api/Topics")]
        Task<Topic>AddTopicAsync([Body] Topic topic);

        [Put("/api/Topics/{id}")]
        Task UpdateTopicAsync(int id, [Body] Topic topic);

        [Delete("/api/Topics/{id}")]
        Task DeleteTopicAsync(int id);

        // -------------------- Question Endpoints --------------------

        [Get("/api/Questions")]
        Task<List<Question>> GetQuestionsAsync();

        [Get("/api/Questions/ByTopic/{topicId}")]
        Task<List<Question>> GetQuestionsByTopicAsync(int topicId);

        [Post("/api/Questions")]
        Task<Question> AddQuestionAsync([Body] Question question);

        [Put("/api/Questions/{id}")]
        Task UpdateQuestionAsync(int id, [Body] Question question);

        [Delete("/api/Questions/{id}")]
        Task DeleteQuestionAsync(int id);


    }
}
