namespace ElectionApp.Views;
using ElectionApp.Services;
using ElectionApp.Interfaces;
using ElectionApp.Models;


public partial class ManualMainPage : ContentPage
{
    public static int username = 0;
    string password = string.Empty;
    private readonly IApiServices _services;
    public static Person person { get; private set; }
    public ManualMainPage(IApiServices services)
	{
		InitializeComponent();
        _services = services;
    }

    private async void LoginAsync(object sender, EventArgs e)
    {
        username = Convert.ToInt32(txtUsername.Text);
        password = txtPin.Text;
        var response = await _services.VotersLogin(username, password);
        if (response)
        {
            person = await _services.GetPerson(username);
            if (person.VFlag)
            {
                await DisplayAlert("Alert", "You voted already or The Election is Closed !!!", "OK");
            }
            else 
            {
                await DisplayAlert("Alert", "You have Successfully Logged In!", "OK");
                await Shell.Current.GoToAsync(nameof(ElectionPage));
            }            
        }
        else
        {
            await DisplayAlert("Alert", "Log in Unsuccessfully!", "OK");
        }
    }
}