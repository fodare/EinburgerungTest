using Einburgerung.Model;

namespace Einburgerung.Services
{
    public interface IGeneralQuestionService
    {
        Task<List<QuestionModel>> GetGeneralQuestions();
        Task<QuestionModel> GetGeneralQuestionById(int questionId);
    }
}
