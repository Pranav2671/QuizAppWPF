using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace QuizAppWPF.Views.Admin
{
    public partial class ManageTopicsView : Window
    {
        private readonly ITopicApi _topicApi;

        public ManageTopicsView(ITopicApi topicApi)
        {
            InitializeComponent();
            _topicApi = topicApi;
            _ = LoadTopicsAsync(); // load existing topics
        }

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
                // Don’t send CreatedAt — let the server handle it
            };

            try
            {
                var response = await _topicApi.AddTopicAsync(newTopic);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    MessageBox.Show($"✅ Topic '{response.Content.Name}' added successfully!");
                    TopicNameBox.Clear();
                    await LoadTopicsAsync(); // refresh
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
                        await LoadTopicsAsync(); // refresh after delete
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
    }
}
