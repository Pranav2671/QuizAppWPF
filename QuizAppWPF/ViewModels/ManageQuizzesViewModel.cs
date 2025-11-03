using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;

namespace QuizAppWPF.ViewModels
{
    public class ManageQuizzesViewModel : BaseViewModel
    {
        private readonly ITopicApi _topicApi;
        private readonly IQuestionApi _questionApi;

        public ObservableCollection<Topic> Topics { get; set; }

        public ICommand LoadTopicsCommand { get; }
        public ICommand ViewQuestionsCommand { get; }

        public ManageQuizzesViewModel(ITopicApi topicApi, IQuestionApi questionApi)
        {
            _topicApi = topicApi;
            _questionApi = questionApi;
            Topics = new ObservableCollection<Topic>();

            LoadTopicsCommand = new RelayCommand(async _ => await LoadTopicsAsync());
            ViewQuestionsCommand = new RelayCommand(async topicObj => await ViewQuestionsAsync(topicObj as Topic));
        }

        public async Task LoadTopicsAsync()
        {
            try
            {
                Topics.Clear();
                var topics = await _topicApi.GetTopicsAsync();
                foreach (var t in topics)
                    Topics.Add(t);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading topics: {ex.Message}");
            }
        }

        private async Task ViewQuestionsAsync(Topic topic)
        {
            if (topic == null) return;

            var questions = await _questionApi.GetQuestionsByTopicAsync(topic.Id);
            if (questions.Count == 0)
            {
                MessageBox.Show("No questions found for this topic.");
                return;
            }

            var dialog = new Views.ManageQuestionsView(topic, questions);
            dialog.ShowDialog();
        }
    }
}
