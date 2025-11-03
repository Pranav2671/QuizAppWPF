using QuizAppWPF.ViewModels;
using QuizAppWPF.Services.Api;
using Refit;
using System.Windows;

namespace QuizAppWPF.Views
{
    public partial class ManageQuizzesView : Window
    {
        private readonly ManageQuizzesViewModel _viewModel;

        public ManageQuizzesView()
        {
            InitializeComponent();

            var quizApi = RestService.For<IQuizApi>("https://localhost:5001");
            var topicApi = RestService.For<ITopicApi>("https://localhost:5001");

            _viewModel = new ManageQuizzesViewModel(quizApi, topicApi);
            DataContext = _viewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadQuizzesAsync();
        }
    }
}
