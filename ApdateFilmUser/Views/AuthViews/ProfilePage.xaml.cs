
using ApdateFilmUser.Services.API;

namespace ApdateFilmUser.Views.AuthViews;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (String.IsNullOrEmpty(ApdateFilmUserClient.GetToken()))
        {
            // ���� ������������ �� �����������, �������������� �� �������� �����������
            await Shell.Current.GoToAsync("auth");
        }
    }
}