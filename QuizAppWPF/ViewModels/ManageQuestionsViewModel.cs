using QuizAppWPF.Models;
using QuizAppWPF.Services.Api;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuizAppWPF.ViewModels
{
    public class ManageQuestionsViewModel : INotifyPropertyChanged
    {
        private readonly IQuestionApi _questionApi;
        private readonly int _topicId;

        public ObservableCollection<Question> Questions { get; set; }
        public string TopicName { get; set; }

        public ICommand LoadQuestionsCommand { get; private set; }
        public ICommand AddQuestionCommand { get; private set; }
        public ICommand EditQuestionCommand { get; private set; }
        public ICommand DeleteQuestionCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ManageQuestionsViewModel(int topicId, string topicName, IQuestionApi questionApi)
        {
            _topicId = topicId;
            TopicName = topicName;
            _questionApi = questionApi;
            Questions = new ObservableCollection<Question>();

            LoadQuestionsCommand = new RelayCommand(async _ => await LoadQuestionsAsync());
            AddQuestionCommand = new RelayCommand(async _ => await AddQuestionAsync());
            EditQuestionCommand = new RelayCommand(async q => await EditQuestionAsync(q as Question));
            DeleteQuestionCommand = new RelayCommand(async q => await DeleteQuestionAsync(q as Question));

            _ = LoadQuestionsAsync();
        }

        private async Task LoadQuestionsAsync()
        {
            try
            {
                var questions = await _questionApi.GetQuestionsByTopicAsync(_topicId);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Questions.Clear();
                    foreach (var q in questions)
                        Questions.Add(q);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message);
            }
        }

        private async Task AddQuestionAsync()
        {
            var dialog = new Views.AddQuestionDialog(_topicId, _questionApi);
            if (dialog.ShowDialog() == true)
            {
                await LoadQuestionsAsync();
            }
        }

        private async Task EditQuestionAsync(Question question)
        {
            if (question == null) return;
            var dialog = new Views.AddQuestionDialog(question, _questionApi);
            if (dialog.ShowDialog() == true)
            {
                await LoadQuestionsAsync();
            }
        }

        private async Task DeleteQuestionAsync(Question question)
        {
            if (question == null) return;

            if (MessageBox.Show($"Delete question: \"{question.Text}\"?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    await _questionApi.DeleteQuestionAsync(question.Id);
                    await LoadQuestionsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting question: " + ex.Message);
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Simple ICommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Func<object, Task> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public async void Execute(object parameter)
        {
            await _execute(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
