
namespace SimpleChat.Presentation;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void NavViewLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is NavigationView navView &&
            navView.SettingsItem is FrameworkElement settingsItem)
        {
            Region.SetName(settingsItem, "Settings");
        }
    }
}
