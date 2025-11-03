using QuizAppWPF.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizAppWPF.Services.Api
{
    public interface IQuizApi
    {
        [Get("/api/quiz")]
        Task<List<Quiz>> GetAllQuizzesAsync();

        [Post("/api/quiz")]
        Task<ApiResponse<Quiz>> AddQuizAsync([Body] Quiz quiz);

        [Delete("/api/quiz/{id}")]
        Task<ApiResponse<object>> DeleteQuizAsync(int id);
    }
}
