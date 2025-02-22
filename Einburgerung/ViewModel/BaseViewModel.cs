using CommunityToolkit.Mvvm.ComponentModel;

namespace Einburgerung.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string title = "";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool isBusy = false;

        public bool IsNotBusy => !IsBusy;
    }
}
