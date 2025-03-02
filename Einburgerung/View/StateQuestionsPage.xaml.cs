using Einburgerung.ViewModel;

namespace Einburgerung.View
{
	public partial class StateQuestionsPage : ContentPage
	{
		public StateQuestionsPage(StateQuestionsViewModel stateQuestionsViewModel)
		{
			InitializeComponent();
			BindingContext = stateQuestionsViewModel;
		}
	}
}