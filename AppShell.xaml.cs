using ElectionApp.Views;

namespace ElectionApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
        Routing.RegisterRoute(nameof(ManualMainPage), typeof(ManualMainPage));
        Routing.RegisterRoute(nameof(ElectionPage), typeof(ElectionPage));
    }
}
