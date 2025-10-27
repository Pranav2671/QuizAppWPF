using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using QuizAppWPF.Models;
using QuizAppWPF.Services;

namespace QuizAppWPF.ViewModels
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();

        private ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        private int _currentIndex = 0;
        private Question _currentQuestion;

        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                OnPropertyChanged("Questions");
            }
        }

        public Question CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                OnPropertyChanged("CurrentQuestion");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadQuestionsAsync()
        {
            var questionsFromApi = await _apiService.GetQuestionsAsync();
            Questions = new ObservableCollection<Question>(questionsFromApi);
            if (Questions.Count > 0)
                CurrentQuestion = Questions[0];
        }

        public void NextQuestion()
        {
            if (_currentIndex < Questions.Count - 1)
            {
                _currentIndex++;
                CurrentQuestion = Questions[_currentIndex];
            }
        }

        public void PreviousQuestion()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                CurrentQuestion = Questions[_currentIndex];
            }
        }
    }
}
