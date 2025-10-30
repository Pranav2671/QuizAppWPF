using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
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
            LoadTopicsAsync();
        }

        // Load all topics
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

        // Add topic
        private async void AddTopic_Click(object sender, RoutedEventArgs e)
        {
            var name = TopicNameBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a topic name.");
                return;
            }

            try
            {
                var newTopic = new Topic { Name = name };
                await _topicApi.AddTopicAsync(newTopic); // we'll confirm the method name next
                MessageBox.Show("Topic added successfully!");
                await LoadTopicsAsync();
                TopicNameBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding topic: {ex.Message}");
            }
        }

        // Delete topic
        private async void DeleteTopic_Click(object sender, RoutedEventArgs e)
        {
            var selectedTopic = TopicsDataGrid.SelectedItem as Topic;
            if (selectedTopic == null)
            {
                MessageBox.Show("Select a topic to delete.");
                return;
            }

            if (MessageBox.Show($"Delete topic '{selectedTopic.Name}'?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    await _topicApi.DeleteTopicAsync(selectedTopic.Id);
                    MessageBox.Show("Topic deleted successfully!");
                    await LoadTopicsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting topic: {ex.Message}");
                }
            }
        }
    }
}
