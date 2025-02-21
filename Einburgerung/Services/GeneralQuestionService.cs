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

        public Task<QuestionModel> GetGeneralQuestionById(int questionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionModel>> GetGeneralQuestions()
        {
            return await _questionReader.ReadGeneralQuestions();
        }
    }
}
