using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Einburgerung.Services;

namespace Einburgerung.ViewModel
{
    public partial class StateQuestionsViewModel : BaseViewModel
    {
        private readonly IStateQuestionService _stateQuestionService;
        private readonly NotificationService _notificationService;

        public StateQuestionsViewModel(IStateQuestionService stateQuestionService, NotificationService notificationService)
        {
            Title = "State Questions";
            _stateQuestionService = stateQuestionService;
            _notificationService = notificationService;
        }

        [ObservableProperty]
        public int stateQuestionsListCount;

        [RelayCommand]
        public async Task GetStateQuestionsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var stateQuestionsList = await _stateQuestionService.GetStateQuestionsAsync();
                StateQuestionsListCount = stateQuestionsList.Count;

            }
            catch (System.Exception exp)
            {
                await Shell.Current.DisplayAlert("Error", "Error reading state questions~", "Ok");
                Debug.WriteLine($"Error reading state questions list {exp.Message}");
            }

            IsBusy = false;
        }
    }
}
