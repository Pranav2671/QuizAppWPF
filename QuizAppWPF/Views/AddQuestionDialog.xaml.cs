using System;
using System.Threading.Tasks;
using System.Windows;
using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;

namespace QuizAppWPF.Views
{
    public partial class AddQuestionDialog : Window
    {
        private readonly int _topicId;
        private readonly IQuestionApi _questionApi; // ✅ store API reference

        public Question NewQuestion { get; private set; }

        public AddQuestionDialog(int topicId)
        {
            InitializeComponent();
            _topicId = topicId;
        }

        public AddQuestionDialog(int topicId, IQuestionApi questionApi) : this(topicId)
        {
            _questionApi = questionApi; // ✅ assign API
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text) ||
                string.IsNullOrWhiteSpace(OptionABox.Text) ||
                string.IsNullOrWhiteSpace(OptionBBox.Text) ||
                string.IsNullOrWhiteSpace(OptionCBox.Text) ||
                string.IsNullOrWhiteSpace(OptionDBox.Text) ||
                string.IsNullOrWhiteSpace(CorrectAnswerBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var correct = CorrectAnswerBox.Text.Trim().ToUpper();
            if (correct != "A" && correct != "B" && correct != "C" && correct != "D")
            {
                MessageBox.Show("Correct answer must be one of A, B, C or D.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var newQuestion = new Question
                {
                    Text = QuestionTextBox.Text.Trim(),
                    OptionA = OptionABox.Text.Trim(),
                    OptionB = OptionBBox.Text.Trim(),
                    OptionC = OptionCBox.Text.Trim(),
                    OptionD = OptionDBox.Text.Trim(),
                    CorrectOption = correct,
                    TopicId = _topicId
                };

                // ✅ Directly call AddQuestionAsync (no status code check needed)
                await _questionApi.AddQuestionAsync(newQuestion);

                MessageBox.Show("Question added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding question: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
