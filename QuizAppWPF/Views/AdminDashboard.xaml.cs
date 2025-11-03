using QuizAppWPF.Services.Api;
using QuizAppWPF.ViewModels;
using QuizAppWPF.Views.Admin;
using Refit;
using System.Windows;

namespace QuizAppWPF.Views
{
    public partial class AdminDashboard : Window
    {
        private readonly IUserApi userApi;

        public AdminDashboard(IUserApi user)
        {
            InitializeComponent();
            userApi = user;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView(userApi);
            loginView.Show();
            this.Close();
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            var manageUsers = new UserManagementView();
            manageUsers.ShowDialog();
        }

        private async void ManageCategories_Click(object sender, RoutedEventArgs e)
        {
            var topicApi = RestService.For<ITopicApi>("https://localhost:5001");
            var questionApi = RestService.For<IQuestionApi>("https://localhost:5001");

            var manageTopics = new ManageTopicsView(topicApi, questionApi);
            manageTopics.ShowDialog();
        }



        // Add Quiz button handler
        private void AddQuiz_Click(object sender, RoutedEventArgs e)
        {
            var quizApi = RestService.For<IQuizApi>("https://localhost:5001");
            var topicApi = RestService.For<ITopicApi>("https://localhost:5001");

            var addQuizDialog = new AddQuizDialog(topicApi, quizApi);
            addQuizDialog.ShowDialog();
        }
        private async void CreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            var selectedTopic = (DataContext as AddQuizViewModel)?.SelectedTopic;
            if (selectedTopic == null)
            {
                MessageBox.Show("Please select a topic first.");
                return;
            }

            var success = await (DataContext as AddQuizViewModel)?.AddQuizAsync(selectedTopic.Id);
            if (success)
            {
                MessageBox.Show("Quiz created successfully!");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to create quiz. Please try again.");
            }
        }

        private async void ViewQuizzes_Click(object sender, RoutedEventArgs e)
        {
            var quizApi = RestService.For<IQuizApi>("https://localhost:5001");
            var topicApi = RestService.For<ITopicApi>("https://localhost:5001");

            var viewQuizzes = new ViewQuizzesView(quizApi, topicApi);
            viewQuizzes.ShowDialog();
        }



    }
}
