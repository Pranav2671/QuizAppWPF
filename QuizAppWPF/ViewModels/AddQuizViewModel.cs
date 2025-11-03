using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace QuizAppWPF.ViewModels
{
    public class AddQuizViewModel : BaseViewModel
    {
        private readonly IQuizApi _quizApi;
        private readonly ITopicApi _topicApi;

        public ObservableCollection<Topic> Topics { get; set; } = new ObservableCollection<Topic>();

        private Topic _selectedTopic;
        public Topic SelectedTopic
        {
            get => _selectedTopic;
            set
            {
                _selectedTopic = value;
                OnPropertyChanged(nameof(SelectedTopic));
            }
        }

        public AddQuizViewModel(IQuizApi quizApi, ITopicApi topicApi)
        {
            _quizApi = quizApi;
            _topicApi = topicApi;

            // Load available topics as soon as dialog opens
            _ = LoadTopicsAsync();
        }

        /// <summary>
        /// Loads topics from backend API
        /// </summary>
        public async Task LoadTopicsAsync()
        {
            Topics.Clear();
            var topics = await _topicApi.GetTopicsAsync();

            foreach (var topic in topics)
                Topics.Add(topic);

            MessageBox.Show($"Loaded {Topics.Count} topics"); // Temporary check
        }

        /// <summary>
        /// Adds a new quiz for the selected topic
        /// </summary>
        public async Task<bool> AddQuizAsync(int topicId)
        {
            var newQuiz = new Quiz
            {
                TopicId = topicId,
                QuestionCount = 0 // initially no questions
            };

            var createdQuiz = await _quizApi.AddQuizAsync(newQuiz);
            return createdQuiz != null;
        }
    }
}
