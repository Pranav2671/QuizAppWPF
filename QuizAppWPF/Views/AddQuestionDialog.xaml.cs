using System;
using System.Threading.Tasks;
using System.Windows;
using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using Refit;

namespace QuizAppWPF.Views
{
    public partial class AddQuestionDialog : Window
    {
        private readonly int _topicId;
        private readonly IQuestionApi _questionApi;
        private readonly bool _isEdit;
        private readonly Question _existingQuestion;

        public Question NewQuestion { get; private set; }

        // Constructor for Add (existing usage)
        public AddQuestionDialog(int topicId, IQuestionApi questionApi)
        {
            InitializeComponent();
            _topicId = topicId;
            _questionApi = questionApi;
            _isEdit = false;
        }

        // Constructor for Edit: pass the question to edit
        public AddQuestionDialog(Question questionToEdit, IQuestionApi questionApi)
        {
            InitializeComponent();
            if (questionToEdit == null) throw new ArgumentNullException(nameof(questionToEdit));

            _existingQuestion = questionToEdit;
            _topicId = questionToEdit.TopicId;
            _questionApi = questionApi;
            _isEdit = true;

            PrefillFields();
        }

        // Prefill UI with existing question values
        private void PrefillFields()
        {
            QuestionTextBox.Text = _existingQuestion.Text;
            OptionABox.Text = _existingQuestion.OptionA;
            OptionBBox.Text = _existingQuestion.OptionB;
            OptionCBox.Text = _existingQuestion.OptionC;
            OptionDBox.Text = _existingQuestion.OptionD;
            CorrectAnswerBox.Text = _existingQuestion.CorrectOption;
            // Optionally change window title or button text when editing
            this.Title = "Edit Question";
            // If your Add button has Content="Add", change it:
            // AddButton.Content = "Save";
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

            // Build the Question object to send
            var questionToSend = new Question
            {
                Text = QuestionTextBox.Text.Trim(),
                OptionA = OptionABox.Text.Trim(),
                OptionB = OptionBBox.Text.Trim(),
                OptionC = OptionCBox.Text.Trim(),
                OptionD = OptionDBox.Text.Trim(),
                CorrectOption = correct,
                TopicId = _topicId
            };

            try
            {
                if (_isEdit)
                {
                    // Use the existing question Id for updating
                    questionToSend.Id = _existingQuestion.Id;
                    var updateResponse = await _questionApi.UpdateQuestionAsync(questionToSend.Id, questionToSend);

                    if (!updateResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Failed to update question: {updateResponse.StatusCode}\n{updateResponse.Error?.Content}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MessageBox.Show("✅ Question updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    var addResponse = await _questionApi.AddQuestionAsync(questionToSend);

                    if (!addResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Failed to add question: {addResponse.StatusCode}\n{addResponse.Error?.Content}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    MessageBox.Show("✅ Question added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calling API: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
