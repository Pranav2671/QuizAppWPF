using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using QuizAppWPF.Models;
using QuizAppWPF.Services;

namespace QuizAppWPF.ViewModels
{
    public class QuizViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();

        private ObservableCollection<Question> _questions = new();
        private int _currentIndex;
        private Question _currentQuestion;

        public ObservableCollection<Question> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                OnPropertyChanged();
            }
        }

        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
