using Einburgerung.Services;
using Einburgerung.View;
using Einburgerung.ViewModel;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace Einburgerung
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder.UseMauiApp<App>().ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			}).UseMauiCommunityToolkit();

			builder.Services.AddSingleton<IQuestionReader, QuestionReader>();
			builder.Services.AddSingleton<IGeneralQuestionService, GeneralQuestionService>();
			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddSingleton<MainPageViewModel>();
			builder.Services.AddSingleton<NotificationService>();

			builder.Services.AddSingleton<StateQuestionsPage>();
			builder.Services.AddSingleton<StateQuestionsViewModel>();
#if DEBUG
			builder.Logging.AddDebug();
#endif
			return builder.Build();
		}
	}
}