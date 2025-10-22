using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using QuizAppWPF.Models;

namespace QuizAppWPF.Services
{
    public class ApiService
    {
        private readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/") // Change port if your API runs differently
        };

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _client.GetFromJsonAsync<List<Question>>("api/questions")
                   ?? new List<Question>();
        }
    }
}
