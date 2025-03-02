using Einburgerung.Model;

namespace Einburgerung.Services
{
    public class StateQuestionService : IStateQuestionService
    {
        private readonly IQuestionReader _questionReader;

        public StateQuestionService(IQuestionReader questionReader)
        {
            _questionReader = questionReader;
        }

        public async Task<List<StateQuestionModel>> GetStateQuestionsAsync()
        {
            var stateQuestionsList = await _questionReader.ReadStateQuestions();
            return [.. stateQuestionsList.Where(question => question != null)];
        }
    }
}
