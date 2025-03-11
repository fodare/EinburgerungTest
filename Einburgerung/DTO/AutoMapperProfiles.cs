using AutoMapper;
using Einburgerung.Model;
namespace Einburgerung.DTO
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<QuestionModel, MockExamQuestionModel>();
            CreateMap<StateQuestionModel, MockExamQuestionModel>();
        }
    }
}
