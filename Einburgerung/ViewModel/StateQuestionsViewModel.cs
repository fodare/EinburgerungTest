using System.Collections.ObjectModel;
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
        public ObservableCollection<StateQuestionModel> StateQuestions { get; } = new();
        public ObservableCollection<string> StatesNames { get; } = new();

        public StateQuestionsViewModel(IStateQuestionService stateQuestionService, NotificationService notificationService)
        {
            Title = "State Questions";
            _stateQuestionService = stateQuestionService;
            _notificationService = notificationService;
            InitilizeStateNames();
        }

        public void InitilizeStateNames()
        {
            var distinctStateQuestion = _stateQuestionService.GetDistinctStates();
            foreach (var question in distinctStateQuestion)
            {
                StatesNames?.Add(question.State!);
            }
        }

        [ObservableProperty]
        public bool isStateQuestionVisible = false;

        [ObservableProperty]
        public StateQuestionModel? currentQuestion;

        [ObservableProperty]
        public string? selectedState;

        [RelayCommand]
        public async Task GetSelectedStateQuestions()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var stateQuestions = await _stateQuestionService.GetStateQuestionsAsync();
                var selectedStatequestions = stateQuestions.Where(question => question.State == SelectedState);
                StateQuestions.Clear();
                foreach (var question in selectedStatequestions)
                {
                    StateQuestions?.Add(question);
                }

                CurrentQuestion = StateQuestions!.First();
                IsStateQuestionVisible = true;
            }
            catch (System.Exception)
            {
                await Shell.Current.DisplayAlert("Error", $"Error reading questions for {SelectedState}", "Ok");
            }

            IsBusy = false;
        }

        [RelayCommand]
        public async Task CheckAnswerAsync(string? selectedOption)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (IsReachedEndOfQuestionList())
            {
                await Shell.Current.DisplayAlert("Info", $"There are no further questions for {SelectedState}. Try getting new questions or explore questions from other states", "Ok");
                IsStateQuestionVisible = false;
                IsBusy = false;
                return;
            }

            try
            {
                if (CurrentQuestion?.Solution == selectedOption)
                    await _notificationService.SnakbarNotification("Correct!");

                else
                    await _notificationService.SnakbarNotification("Wrong!");

                await Task.Delay(2000);
                NextQuestion();
            }
            catch (System.Exception)
            {
                await Shell.Current.DisplayAlert("Error", $"Error validating input", "Ok");
            }
            IsBusy = false;
        }

        public void NextQuestion()
        {
            CurrentQuestion = StateQuestions.FirstOrDefault(question => question.Num == (CurrentQuestion!.Num + 1));
        }

        public bool IsReachedEndOfQuestionList()
        {
            if (CurrentQuestion is null)
                return true;

            return false;
        }
    }
}
