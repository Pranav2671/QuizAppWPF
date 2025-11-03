using System.Windows;
using QuizAppWPF.ViewModels;
using QuizAppWPF.Services.Api;

namespace QuizAppWPF.Views
{
    public partial class AddQuizDialog : Window
    {
        private readonly AddQuizViewModel _viewModel;

        public AddQuizDialog(ITopicApi topicApi, IQuizApi quizApi)
        {
            InitializeComponent();

            _viewModel = new AddQuizViewModel(quizApi,topicApi);
            DataContext = _viewModel; // ✅ This binds the ViewModel to the UI

            //Loaded += async (s, e) => await _viewModel.LoadTopicsAsync();
        }

        private void CreateQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedTopic != null)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a topic first!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
