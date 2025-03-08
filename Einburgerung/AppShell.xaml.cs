using Einburgerung.View;

namespace Einburgerung
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(StateQuestionsPage), typeof(StateQuestionsPage));
        }
    }
}