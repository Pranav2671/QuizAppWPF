using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace QuizAppWPF.Views
{
    public partial class ManageTopicsView : Window
    {
        private readonly ITopicApi _topicApi;
        private readonly IQuestionApi _questionApi; // ✅ Added

        public ManageTopicsView(ITopicApi topicApi, IQuestionApi questionApi)
        {
            InitializeComponent();
            _topicApi = topicApi;
            _questionApi = questionApi;
            _ = LoadTopicsAsync();
        }

        //public ManageTopicsView(ITopicApi topicApi)
        //{
        //    _topicApi = topicApi;
        //}

        private async Task LoadTopicsAsync()
        {
            try
            {
                var topics = await _topicApi.GetTopicsAsync();
                TopicsDataGrid.ItemsSource = topics;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading topics: {ex.Message}");
            }
        }

        private async void AddTopic_Click(object sender, RoutedEventArgs e)
        {
            string topicName = TopicNameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(topicName))
            {
                MessageBox.Show("Please enter a topic name.");
                return;
            }

            var newTopic = new Topic
            {
                Name = topicName
            };

            try
            {
                var response = await _topicApi.AddTopicAsync(newTopic);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    MessageBox.Show($"✅ Topic '{response.Content.Name}' added successfully!");
                    TopicNameBox.Clear();
                    await LoadTopicsAsync();
                }
                else
                {
                    MessageBox.Show($"❌ Failed to add topic. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding topic: {ex.Message}");
            }
        }

        private async void DeleteTopic_Click(object sender, RoutedEventArgs e)
        {
            var selectedTopic = TopicsDataGrid.SelectedItem as Topic;

            if (selectedTopic == null)
            {
                MessageBox.Show("Please select a topic to delete.");
                return;
            }

            if (MessageBox.Show($"Are you sure you want to delete '{selectedTopic.Name}'?",
                                "Confirm Delete",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    var response = await _topicApi.DeleteTopicAsync(selectedTopic.Id);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("✅ Topic deleted successfully!");
                        await LoadTopicsAsync();
                    }
                    else
                    {
                        MessageBox.Show($"❌ Failed to delete topic. Status: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting topic: {ex.Message}");
                }
            }
        }

        // ✅ When "View Questions" is clicked
        private void ViewQuestions_Click(object sender, RoutedEventArgs e)
        {
            var selectedTopic = (sender as FrameworkElement)?.DataContext as Topic;
            if (selectedTopic == null)
            {
                MessageBox.Show("Please select a topic first.");
                return;
            }

            var questionApi = RestService.For<IQuestionApi>("https://localhost:5001");
            var view = new ManageQuestionsView(selectedTopic.Id, selectedTopic.Name, questionApi, _topicApi);
            view.ShowDialog();
        }


    }
}
