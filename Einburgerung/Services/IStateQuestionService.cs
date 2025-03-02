using Einburgerung.Model;

namespace Einburgerung.Services;
public interface IStateQuestionService
{
    Task<List<StateQuestionModel>> GetStateQuestionsAsync();
}
