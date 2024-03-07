
using ElectionApp.Services;
using ElectionApp.Views;
using ElectionApp.Interfaces;
using ElectionApp.Models;
using ZXing.Net.Maui;
using System;
using Microsoft.Maui.Dispatching;

namespace ElectionApp;


public partial class MainPage : ContentPage
{
	
	public static int username = 0;    
	string password = string.Empty;
    private readonly IApiServices _services;
    public Boolean response { get; set; }
   public static Person person { get; private set; }

	public MainPage(IApiServices services)
	{
		InitializeComponent();
       _services = services;
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false
        };
        cameraBarcodeReaderView.CameraLocation = CameraLocation.Front;

    }

	

    private  async void LoginAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ManualMainPage));
    }



    protected void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        try
        {
            cameraBarcodeReaderView.IsDetecting = false;
            var result = e.Results.FirstOrDefault();
            if (result != null)
            {
                var text = result.Value;
                string[] qrcode = text.Split(',');
                var vid = Convert.ToInt32(qrcode[0]);
                var pin = qrcode[1];                
                Login(vid, pin);
                Thread.Sleep(3000);
                if (person.VFlag)
                {

                    Dispatcher.DispatchAsync(async () =>
                    {
                        await DisplayAlert("Alert", "You voted already or The Election is Closed !!!", "OK");
                        cameraBarcodeReaderView.IsDetecting = true;
                    });
                }
                else
                {
                    username = vid;
                    Dispatcher.DispatchAsync(async () =>
                    {
                        await DisplayAlert("Alert", "You have Successfully Logged In!", "OK");
                        await Shell.Current.GoToAsync(nameof(ElectionPage));
                    });
                }
            }
        } catch (Exception ex)  
        { 
            ex.Message.ToString();
        }
    }  
    public async void Login(int vid,  string pin)
    {
        response = await _services.VotersLogin(vid, pin);
        if(response)
        {
            person = await _services.GetPerson(vid);
        }
        else
        {
            await DisplayAlert("Alert", "Log in Unsuccessfully!", "OK");
            cameraBarcodeReaderView.IsDetecting = true;
        }        
    }

}

