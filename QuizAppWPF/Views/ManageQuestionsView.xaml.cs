using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace QuizAppWPF.Views
{
    public partial class ManageQuestionsView : Window
    {
        private readonly int _topicId;
        private readonly string _topicName;
        private readonly IQuestionApi _questionApi;
        private ITopicApi topicApi;

        public ManageQuestionsView(int topicId, string topicName, IQuestionApi questionApi)
        {
            InitializeComponent();
            _topicId = topicId;
            _topicName = topicName;
            _questionApi = questionApi;

            TopicTitle.Text = $"Questions for: {_topicName}";
            _ = LoadQuestionsAsync();
        }

        public ManageQuestionsView(int topicId, string topicName, IQuestionApi questionApi, ITopicApi topicApi) : this(topicId, topicName, questionApi)
        {
            this.topicApi = topicApi;
        }

        private async Task LoadQuestionsAsync()
        {
            try
            {
                var questions = await _questionApi.GetQuestionsByTopicAsync(_topicId);
                QuestionsList.ItemsSource = questions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}");
            }
        }

        private async void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddQuestionDialog(_topicId, _questionApi); // ✅ make sure dialog takes these params
            if (dialog.ShowDialog() == true)
            {
                await LoadQuestionsAsync(); // refresh list after adding
            }
        }

        private async void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var question = button?.DataContext as Question;
            if (question == null) return;

            // Open dialog in edit mode
            var dialog = new AddQuestionDialog(question, _questionApi);
            if (dialog.ShowDialog() == true)
            {
                await LoadQuestionsAsync(); // refresh question list
            }
        }

        private async void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            var question = button?.DataContext as Question;
            if (question == null)
                return;

            if (MessageBox.Show($"Delete question: \"{question.Text}\"?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    await _questionApi.DeleteQuestionAsync(question.Id);
                    await LoadQuestionsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting question: {ex.Message}");
                }
            }
        }
    }
}
