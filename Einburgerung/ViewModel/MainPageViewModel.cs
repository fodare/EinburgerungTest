using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Einburgerung.Services;

namespace Einburgerung.ViewModel
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IGeneralQuestionService _generalQuestionService;
        public MainPageViewModel(IGeneralQuestionService generalQuestionService)
        {
            _generalQuestionService = generalQuestionService;
            Title = "GenralQuestions";
        }

        [ObservableProperty]
        public int generalQuestionCount;

        [RelayCommand]
        public async Task GetGeneralQuestionsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var questionsList = await _generalQuestionService.GetGeneralQuestions();
                GeneralQuestionCount = questionsList.Count;
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
    }
}
