using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Einburgerung.Model;
using Einburgerung.Services;

namespace Einburgerung.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IGeneralQuestionService _generalQuestionService;
        private readonly NotificationService _notificationService;
        public ObservableCollection<QuestionModel> QuestionsList { get; } = new();

        [ObservableProperty]
        public QuestionModel? currentQuestion;

        public MainPageViewModel(IGeneralQuestionService generalQuestionService, NotificationService notificationService)
        {
            _generalQuestionService = generalQuestionService;
            _notificationService = notificationService;
            Title = "GenralQuestions";
            InitializeCurrentQuestion();
        }

        public void InitializeCurrentQuestion()
        {
            var questionsList = GetGeneralQuestions();
            CleanAndUpdateQuestionsList(questionsList);
            QuestionModel firstGeneralQuestion = QuestionsList.First();
            UpdateCurrentQuestion(firstGeneralQuestion);
        }

        [RelayCommand]
        public async Task GetGeneralQuestionsAsync()
        {
            IsBusy = true;
            try
            {
                var questionsList = await _generalQuestionService.GetGeneralQuestionsAsync();
                CleanAndUpdateQuestionsList(questionsList);
            }
            catch (FileNotFoundException)
            {
                await Shell.Current.DisplayAlert("Error!", "Error loading general questions!", "Ok");
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
                await Shell.Current.DisplayAlert("Info", $"There are no further questions. Pull to refresh or checkout state questions", "Ok");
                IsBusy = false;
                CurrentQuestion = QuestionsList.First();
                return;
            }

            try
            {
                if (selectedOption == CurrentQuestion!.Solution)
                    await _notificationService.SnakbarNotification("Correct.");
                else
                    await _notificationService.SnakbarNotification($"Wrong. Correct answer is {CurrentQuestion.Solution}");

                await Task.Delay(2000);
                NextQuestion();
            }
            catch (System.Exception)
            {
                await _notificationService.SnakbarNotification("Error validating selection!");
            }
            IsBusy = false;
        }

        [RelayCommand]
        public async Task JumpToQuestionNumberAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            string userInput = await Shell.Current.DisplayPromptAsync("Enter question number below.", $"Range 1 - {QuestionsList.Count}");
            if (int.TryParse(userInput, out int questionNumber) && questionNumber >= 1 && questionNumber <= QuestionsList.Count)
            {
                CurrentQuestion = QuestionsList.Where(question => question.Num == questionNumber).FirstOrDefault();
            }
            else if (userInput == null)
            {
                IsBusy = false;
                return;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", $"Question number ranges between 1 - {QuestionsList.Count}", "Ok");
            }
            IsBusy = false;
        }

        public List<QuestionModel> GetGeneralQuestions()
        {
            return _generalQuestionService.GetGeneralQuestions();
        }

        public void CleanAndUpdateQuestionsList(List<QuestionModel> newQuestionsList)
        {
            QuestionsList?.Clear();
            foreach (QuestionModel? question in newQuestionsList)
            {
                QuestionsList!.Add(question);
            }
        }

        public void UpdateCurrentQuestion(QuestionModel? questionModel)
        {
            CurrentQuestion = questionModel;
        }

        public void NextQuestion()
        {
            CurrentQuestion = QuestionsList.FirstOrDefault(question => question.Num == (CurrentQuestion!.Num + 1));
        }

        public bool IsReachedEndOfQuestionList()
        {
            if (CurrentQuestion is null)
                return true;

            return false;
        }
    }
}
