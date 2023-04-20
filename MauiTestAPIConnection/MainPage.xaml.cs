using MauiTestAPIConnection.Interfaces;
using MauiTestAPIConnection.Services;
using MauiTestAPIConnection.ViewModels;

namespace MauiTestAPIConnection;

public partial class MainPage : ContentPage
{
	public MainPage(IRestService restService)
	{
		InitializeComponent();

        BindingContext = new MainPageViewModel(restService);
    }
}

