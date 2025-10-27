using System;
using System.Windows;
using QuizAppWPF.ViewModels;

namespace QuizAppWPF
{

    public partial class MainWindow: Window
    {
        private QuizViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            //Step 1 : Create the ViewModel
            _viewModel = new QuizViewModel();

            // Step 2: Connect ViewModel to UI (DataContext = brain of the UI)
            DataContext = _viewModel;

            // Step 3: Load data from API (run asynchronously)
            Loaded += async (s, e) => await _viewModel.LoadQuestionsAsync();

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.NextQuestion();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PreviousQuestion();
        }

    }
}
