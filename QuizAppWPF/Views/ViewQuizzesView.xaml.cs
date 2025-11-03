using QuizAppWPF.ViewModels;
using QuizAppWPF.Services.Api;
using Refit;
using System.Windows;

namespace QuizAppWPF.Views
{
    public partial class ViewQuizzesView : Window
    {
        private readonly ViewQuizzesViewModel _viewModel;

        public ViewQuizzesView(IQuizApi quizApi, ITopicApi topicApi)
        {
            InitializeComponent();

            _viewModel = new ViewQuizzesViewModel(quizApi, topicApi);
            DataContext = _viewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadQuizzesAsync();
        }
    }
}
