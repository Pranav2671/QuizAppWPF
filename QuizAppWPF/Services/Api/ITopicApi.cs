using QuizAppWPF.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizAppWPF.Services.Api
{
    public interface ITopicApi
    {
        [Get("/api/topics")]
        Task<List<Topic>> GetTopicsAsync();

        [Post("/api/topics")]
        Task<ApiResponse<Topic>> AddTopicAsync([Body] Topic topic);

        [Put("/api/topics/{id}")]
        Task<ApiResponse<Topic>> UpdateTopicAsync(int id, [Body] Topic topic);

        [Delete("/api/topics/{id}")]
        Task<ApiResponse<object>> DeleteTopicAsync(int id);
    }
}
