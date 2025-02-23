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
            return await _questionReader.ReadGeneralQuestions();
        }

        public List<QuestionModel> GetGeneralQuestions()
        {
            var questionsList = _questionReader.ReadGeneralQuestions();
            return questionsList.Result;
        }
    }
}
