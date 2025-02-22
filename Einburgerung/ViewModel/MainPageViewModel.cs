using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Einburgerung.Model;
using Einburgerung.Services;

namespace Einburgerung.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IGeneralQuestionService _generalQuestionService;
        public ObservableCollection<QuestionModel> QuestionsList { get; } = new();

        [ObservableProperty]
        public QuestionModel? currentQuestion;

        public MainPageViewModel(IGeneralQuestionService generalQuestionService)
        {
            _generalQuestionService = generalQuestionService;
            Title = "GenralQuestions";
            InitializeCurrentQuestion();
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

            finally
            {
                IsBusy = false;
            }

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

        public void InitializeCurrentQuestion()
        {
            var questionsList = GetGeneralQuestions();
            CleanAndUpdateQuestionsList(questionsList);
            QuestionModel firstGeneralQuestion = QuestionsList.First();
            UpdateCurrentQuestion(firstGeneralQuestion);
        }

        public void NextQuestion()
        {
            CurrentQuestion = QuestionsList.FirstOrDefault(question => question.Num == (CurrentQuestion!.Num + 1));
        }

        [RelayCommand]
        public async Task CheckAnswerAsync(string? selectedOption)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Debug.WriteLine($"Selected option: {selectedOption}");
                // QuestionsList?.Remove(question)
                NextQuestion();

            }
            catch (System.Exception exp)
            {
                Debug.WriteLine($"Error. {exp.Message}");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
