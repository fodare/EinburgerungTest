using Einburgerung.ViewModel;

namespace Einburgerung.View
{
	public partial class MainPage : ContentPage
	{
		public MainPage(MainPageViewModel mainPageViewModel)
		{
			InitializeComponent();
			BindingContext = mainPageViewModel;
		}
	}

}