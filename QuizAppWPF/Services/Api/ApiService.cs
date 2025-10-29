using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppWPF.Services.Api
{
    internal class ApiService
    {
        private static readonly string BaseUrl = "https://localhost:5001"; // your API base URL

        public static IUserApi UserApi { get; } = RestService.For<IUserApi>(BaseUrl);
    }
}
