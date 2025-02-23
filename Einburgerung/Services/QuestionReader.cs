using Einburgerung.Model;
using Newtonsoft.Json;

namespace Einburgerung.Services
{
    public class QuestionReader : IQuestionReader
    {
        private static readonly string appDirectory = FileSystem.Current.AppDataDirectory;
        private static readonly string generalQuestionsDirectory = System.IO.Path.Combine(appDirectory, "generalquestions.json");

        public async Task<List<QuestionModel>> ReadGeneralQuestions()
        {
            using var fileStream = await FileSystem.OpenAppPackageFileAsync("generalquestions.json");
            using StreamReader? streamReader = new(fileStream);
            var generalQuestionContnet = streamReader.ReadToEnd();
            List<QuestionModel> generalQuestionList = JsonConvert.DeserializeObject<List<QuestionModel>>(generalQuestionContnet)!;
            return generalQuestionList;
        }
    }
}
