using Einburgerung.Model;

namespace Einburgerung.Services
{
    public class GeneralQuestionService : IGeneralQuestionService
    {
        private readonly IQuestionReader _questionReader;

        public GeneralQuestionService(IQuestionReader questionReader)
        {
            _questionReader = questionReader;
        }

        public Task<QuestionModel> GetGeneralQuestionByIdAsync(int questionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionModel>> GetGeneralQuestionsAsync()
        {
            var questionsList = await _questionReader.ReadGeneralQuestions();
            return [.. questionsList.Where(question => question != null)];
        }

        public List<QuestionModel> GetGeneralQuestions()
        {
            var questionsList = _questionReader.ReadGeneralQuestions();
            return [.. questionsList.Result.Where(question => question != null)];
        }
    }
}
