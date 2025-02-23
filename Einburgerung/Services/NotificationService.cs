using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Einburgerung.Services
{
    public class NotificationService
    {
        public async Task SnakbarNotification(string notificationMessage)
        {
            CancellationTokenSource cancellationTokenSource = new();

            SnackbarOptions snackbarOptions = new()
            {
                BackgroundColor = Colors.Black,
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.White,
                CornerRadius = new CornerRadius(10),
                CharacterSpacing = 0.5
            };

            TimeSpan notificationDuration = TimeSpan.FromSeconds(2);
            var snackbar = Snackbar.Make(message: notificationMessage, duration: notificationDuration, visualOptions: snackbarOptions);
            await snackbar.Show(cancellationTokenSource.Token);
        }
    }
}
