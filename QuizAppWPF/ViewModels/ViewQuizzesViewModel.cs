using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;

namespace QuizAppWPF.ViewModels
{
    public class ViewQuizzesViewModel : BaseViewModel
    {
        private readonly IQuizApi _quizApi;
        private readonly ITopicApi _topicApi;

        public ObservableCollection<Quiz> Quizzes { get; set; }

        public ICommand LoadQuizzesCommand { get; }
        public ICommand AddQuizCommand { get; }

        public ViewQuizzesViewModel(IQuizApi quizApi, ITopicApi topicApi)
        {
            _quizApi = quizApi;
            _topicApi = topicApi;
            Quizzes = new ObservableCollection<Quiz>();

            LoadQuizzesCommand = new RelayCommand(async _ => await LoadQuizzesAsync());
            AddQuizCommand = new RelayCommand(async _ => await AddQuizAsync());
        }

        public async Task LoadQuizzesAsync()
        {
            Quizzes.Clear();
            var quizzes = await _quizApi.GetAllQuizzesAsync();
            foreach (var quiz in quizzes)
            {
                Quizzes.Add(quiz);
            }
        }

        private async Task AddQuizAsync()
        {
            var dialog = new QuizAppWPF.Views.AddQuizDialog(_topicApi, _quizApi);
            if (dialog.ShowDialog() == true)
            {
                await LoadQuizzesAsync(); // refresh list after adding quiz
            }
        }
    }
}
