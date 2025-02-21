using System;
using Einburgerung.Model;

namespace Einburgerung.Services
{
    public interface IQuestionReader
    {
        Task<List<QuestionModel>> ReadGeneralQuestions();
    }
}
