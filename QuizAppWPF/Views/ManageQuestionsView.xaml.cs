using QuizAppWPF.Services.Api;
using QuizAppWPF.ViewModels;
using System.Windows;

namespace QuizAppWPF.Views
{
    public partial class ManageQuestionsView : Window
    {
        public ManageQuestionsView(int topicId, string topicName, IQuestionApi questionApi)
        {
            InitializeComponent();
            DataContext = new ManageQuestionsViewModel(topicId, topicName, questionApi);
        }
    }
}
