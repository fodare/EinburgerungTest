using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Einburgerung.Model;
using Einburgerung.Services;

namespace Einburgerung.ViewModel
{
    public partial class StateQuestionsViewModel : BaseViewModel
    {
        private readonly IStateQuestionService _stateQuestionService;
        private readonly NotificationService _notificationService;
        public ObservableCollection<StateQuestionsViewModel> StateQuestions { get; } = new();
        public ObservableCollection<string> StatesNames { get; } = new();

        public StateQuestionsViewModel(IStateQuestionService stateQuestionService, NotificationService notificationService)
        {
            Title = "State Questions";
            _stateQuestionService = stateQuestionService;
            _notificationService = notificationService;
            InitilizeStateNames();
        }

        [ObservableProperty]
        public StateQuestionModel? currentQuestion;

        [RelayCommand]
        public async Task GetStateQuestionsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var stateQuestionsList = await _stateQuestionService.GetStateQuestionsAsync();

            }
            catch (System.Exception exp)
            {
                await Shell.Current.DisplayAlert("Error", "Error reading state questions~", "Ok");
                Debug.WriteLine($"Error reading state questions list {exp.Message}");
            }

            IsBusy = false;
        }

        public void InitilizeStateNames()
        {
            var distinctStateQuestion = _stateQuestionService.GetDistinctStates();
            foreach (var question in distinctStateQuestion)
            {
                StatesNames?.Add(question.State!);
            }
        }
    }
}
