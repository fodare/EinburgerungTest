using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Einburgerung.Model;
using Einburgerung.Services;
namespace Einburgerung.ViewModel
{
    public partial class MockExamPageViewModel : BaseViewModel
    {
        private readonly IStateQuestionService _stateQuestionService;
        private readonly IGeneralQuestionService _generalQuestionService;
        private readonly NotificationService _notificationService;
        private readonly IMapper _mapper;

        public MockExamPageViewModel(IGeneralQuestionService generalQuestionService, IStateQuestionService stateQuestionService, NotificationService notificationService, IMapper mapper)
        {
            _generalQuestionService = generalQuestionService;
            _stateQuestionService = stateQuestionService;
            _notificationService = notificationService;
            _mapper = mapper;
            Title = "Mock Exam";
            InitilizeStateNames();
        }

        public ObservableCollection<MockExamQuestionModel> MockQuestions { get; } = new();
        public ObservableCollection<string> StateNames { get; } = new();

        [ObservableProperty]
        public string? selectedStateName;

        [ObservableProperty]
        public bool isQuestionVisible = false;

        [ObservableProperty]
        public MockExamQuestionModel? currentQuestion;

        [ObservableProperty]
        public int answeredCorrectlyCount;

        public void InitilizeStateNames()
        {
            var distinctQuestionByStateName = _stateQuestionService.GetDistinctStates();
            StateNames?.Clear();
            foreach (var question in distinctQuestionByStateName)
            {
                StateNames?.Add(question.State!);
            }
        }

        [RelayCommand]
        public async Task PrepareMockExamAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            if (SelectedStateName is null || SelectedStateName == "")
            {
                await Shell.Current.DisplayAlert("Info", "Please select desired state name!", "Ok");
                IsBusy = false;
                return;
            }
            var randomGeneralQuestions = await GetRandomGeneralQuestions(30);
            var randomStateQuestions = await GetRandomStateQuestions(3);
            MockQuestions?.Clear();
            PrepareMockQuestions(generalQuestions: randomGeneralQuestions, stateQuestions: randomStateQuestions);
            OverideMockQuestionsNumbers();
            CurrentQuestion = MockQuestions?.First();
            IsQuestionVisible = true;
            IsBusy = false;
        }

        [RelayCommand]
        public async Task CheckAnswerAsync(string? selectedOption)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                if (CurrentQuestion?.Solution == selectedOption)
                {
                    AnsweredCorrectlyCount += 1;
                    await _notificationService.SnakbarNotification("Correct!");
                }
                else
                {
                    await _notificationService.SnakbarNotification($"Wrong. Correct answer is {CurrentQuestion?.Solution}");
                }
                if (IsReachedEndOfQuestionList())
                {
                    await Shell.Current.DisplayAlert("Info", $"Reached end of question list. Scored {AnsweredCorrectlyCount}/{MockQuestions.Count}.", "OK");
                    IsQuestionVisible = false;
                    IsBusy = false;
                    return;
                }
                await Task.Delay(1500);
                NextQuestion();
            }
            catch (System.Exception)
            {
                await Shell.Current.DisplayAlert("Error", "Error validating option.", "Ok");
            }
            IsBusy = false;
        }

        public async Task<List<QuestionModel>> GetRandomGeneralQuestions(int questionCount)
        {
            Random _random = new();
            var generalQuestions = await _generalQuestionService.GetGeneralQuestionsAsync();
            if (generalQuestions != null)
                return [.. generalQuestions.OrderBy(question => _random.Next()).Take(questionCount)];
            return null!;
        }

        public async Task<List<StateQuestionModel>> GetRandomStateQuestions(int questionCount)
        {
            Random _random = new();
            var stateQuestions = await _stateQuestionService.GetStateQuestionsAsync();
            var randomStateQuestions = stateQuestions.Where(question => question.State == SelectedStateName).OrderBy(question => _random.Next()).Take(questionCount).ToList();
            if (randomStateQuestions != null)
                return randomStateQuestions;
            return null!;
        }

        public void PrepareMockQuestions(List<QuestionModel> generalQuestions, List<StateQuestionModel> stateQuestions)
        {
            foreach (var question in generalQuestions)
                MockQuestions.Add(_mapper.Map<MockExamQuestionModel>(question));

            foreach (var question in stateQuestions)
                MockQuestions.Add(_mapper.Map<MockExamQuestionModel>(question));
        }

        public void OverideMockQuestionsNumbers()
        {
            for (int i = 0; i < MockQuestions.Count; i++)
            {
                MockQuestions[i].Num = i + 1;
            }
        }

        public void NextQuestion()
        {
            CurrentQuestion = MockQuestions.FirstOrDefault(question => question.Num == (CurrentQuestion?.Num + 1));
        }

        public bool IsReachedEndOfQuestionList()
        {
            if (CurrentQuestion is null || CurrentQuestion?.Num == MockQuestions.Last().Num)
                return true;

            return false;
        }
    }
}
