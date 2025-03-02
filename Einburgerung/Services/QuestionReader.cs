using Einburgerung.Model;
using Newtonsoft.Json;

namespace Einburgerung.Services
{
    public class QuestionReader : IQuestionReader
    {
        public async Task<List<QuestionModel>> ReadGeneralQuestions()
        {
            using var fileStream = await FileSystem.OpenAppPackageFileAsync("generalquestions.json");
            using StreamReader? streamReader = new(fileStream);
            var generalQuestionContnet = streamReader.ReadToEnd();
            List<QuestionModel> generalQuestionList = JsonConvert.DeserializeObject<List<QuestionModel>>(generalQuestionContnet)!;
            return generalQuestionList;
        }

        public async Task<List<StateQuestionModel>> ReadStateQuestions()
        {
            using var fileStream = await FileSystem.OpenAppPackageFileAsync("updatedStateQuestion.json");
            using StreamReader? streamReader = new(fileStream);
            var stateQuestionsContent = streamReader.ReadToEnd();
            List<StateQuestionModel> stateQuestionsList = JsonConvert.DeserializeObject<List<StateQuestionModel>>(stateQuestionsContent)!;
            return stateQuestionsList;
        }
    }
}
