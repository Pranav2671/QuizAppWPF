using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuizAppWPF.Views
{
    public partial class ManageQuestionsView : Window
    {
        private readonly ITopicApi _topicApi;
        private readonly IQuestionApi _questionApi;

        public ManageQuestionsView(ITopicApi topicApi, IQuestionApi questionApi)
        {
            InitializeComponent();
            _topicApi = topicApi;
            _questionApi = questionApi;
            LoadTopicsAsync();
        }

        // Load all topics into dropdown
        private async void LoadTopicsAsync()
        {
            try
            {
                var topics = await _topicApi.GetTopicsAsync();
                TopicComboBox.ItemsSource = topics;
                TopicComboBox.DisplayMemberPath = "Name";
                TopicComboBox.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load topics: " + ex.Message);
            }
        }

        // Load questions when a topic is selected
        private async void TopicComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedTopic = TopicComboBox.SelectedItem as Topic;
            if (selectedTopic != null)
            {
                await LoadQuestionsAsync(selectedTopic.Id);
            }
        }

        private async Task LoadQuestionsAsync(int topicId)
        {
            try
            {
                var questions = await _questionApi.GetQuestionsByTopicAsync(topicId);
                QuestionsDataGrid.ItemsSource = questions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load questions: " + ex.Message);
            }
        }

        // Add Question button
        private async void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var selectedTopic = TopicComboBox.SelectedItem as Topic;
            if (selectedTopic == null)
            {
                MessageBox.Show("Please select a topic first.");
                return;
            }

            var dialog = new AddQuestionDialog();
            if (dialog.ShowDialog() == true)
            {
                var newQuestion = dialog.NewQuestion;
                newQuestion.TopicId = selectedTopic.Id;

                try
                {
                    await _questionApi.AddQuestionAsync(newQuestion);
                    MessageBox.Show("Question added successfully!");
                    await LoadQuestionsAsync(selectedTopic.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding question: " + ex.Message);
                }
            }
        }

        // Delete selected question
        private async void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuestion = QuestionsDataGrid.SelectedItem as Question;
            if (selectedQuestion == null)
            {
                MessageBox.Show("Select a question to delete.");
                return;
            }

            if (MessageBox.Show($"Delete question '{selectedQuestion.Text}'?", "Confirm", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    await _questionApi.DeleteQuestionAsync(selectedQuestion.Id);
                    MessageBox.Show("Question deleted successfully!");
                    var topic = TopicComboBox.SelectedItem as Topic;
                    if (topic != null)
                        await LoadQuestionsAsync(topic.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting question: " + ex.Message);
                }
            }
        }
    }
}
