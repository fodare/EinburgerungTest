using Einburgerung.Model;
using Einburgerung.Services;
using Einburgerung.ViewModel;
using Moq;

namespace EinburgerungTests
{
    public class MainPageViewModelTests
    {
        [Fact]
        public void GetGeneralQuestions_ShallReturn_ListOfQuestionModel()
        {
            var generalQuestionServiceMock = new Mock<IGeneralQuestionService>();
            generalQuestionServiceMock
                .Setup(mock => mock.GetGeneralQuestions())
                .Returns(new List<QuestionModel> { new() { Num = 1, Question = "Question1" }, new() { Num = 2, Question = "Question 2" } });

            var notificationServiceMock = new Mock<NotificationService>();
            var mainPageViewModel = new MainPageViewModel(generalQuestionServiceMock.Object, notificationServiceMock.Object);

            var questions = mainPageViewModel.GetGeneralQuestions();
            Assert.NotEmpty(questions);
        }
    }
}