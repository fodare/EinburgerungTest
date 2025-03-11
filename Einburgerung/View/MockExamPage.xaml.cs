using Einburgerung.ViewModel;

namespace Einburgerung.View
{
	public partial class MockExamPage : ContentPage
	{
		public MockExamPage(MockExamPageViewModel mockExamPageViewModel)
		{
			InitializeComponent();
			BindingContext = mockExamPageViewModel;
		}
	}
}