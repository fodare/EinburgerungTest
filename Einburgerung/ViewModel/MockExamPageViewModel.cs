using Einburgerung.Services;
namespace Einburgerung.ViewModel
{
    public partial class MockExamPageViewModel : BaseViewModel
    {
        private readonly IStateQuestionService _stateQuestionService;
        private readonly IGeneralQuestionService _generalQuestionService;
        private readonly NotificationService _notificationService;

        public MockExamPageViewModel(IGeneralQuestionService generalQuestionService, IStateQuestionService stateQuestionService, NotificationService notificationService)
        {
            _generalQuestionService = generalQuestionService;
            _stateQuestionService = stateQuestionService;
            _notificationService = notificationService;
            Title = "Mock Exam";
        }
    }
}
