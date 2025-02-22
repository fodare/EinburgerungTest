using Einburgerung.Model;

namespace Einburgerung.Services
{
    public interface IGeneralQuestionService
    {
        Task<List<QuestionModel>> GetGeneralQuestionsAsync();
        Task<QuestionModel> GetGeneralQuestionByIdAsync(int questionId);
        List<QuestionModel> GetGeneralQuestions();
    }
}
