using ApdateFilmUser.Models;
using ApdateFilmUser.Services.API;
using ApdateFilmUser.Servieces;
using ApdateFilmUser.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Maui.Devices;

namespace ApdateFilmUser.Views;

[QueryProperty(nameof(MediaItem), "media")]
public partial class MediaPage : ContentPage
{
    private int _selectedRating = 0;
    private bool _checkFavorite = false;
    public Media MediaItem { get; set; }

    public MediaPage()
    {
        InitializeComponent();
        InitializeRatingControls();

        // Подписываемся на изменение ориентации
        DeviceDisplay.Current.MainDisplayInfoChanged += OnDisplayInfoChanged;
        UpdateWebViewHeight();
    }

    private void OnDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        UpdateWebViewHeight();
    }

    private void UpdateWebViewHeight()
    {
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        TrailerView.HeightRequest = displayInfo.Orientation == DisplayOrientation.Portrait ? 290 : 490;
    }

    private void InitializeRatingControls()
    {
        for (int i = 1; i <= 10; i++)
        {
            var button = new Button
            {
                Text = i.ToString(),
                FontSize = 16,
                Padding = 3,
                WidthRequest = 40,
                HeightRequest = 40,
                CornerRadius = 20,
                Margin = new Thickness(3),
                BackgroundColor = Color.FromArgb("#E5E7EB"),
                TextColor = Colors.Black
            };

            button.Clicked += (sender, e) =>
            {
                // Сбрасываем цвет всех кнопок
                foreach (var child in RatingContainer.Children)
                {
                    if (child is Button btn)
                    {
                        btn.BackgroundColor = Color.FromArgb("#E5E7EB");
                        btn.TextColor = Colors.Black;
                    }
                }

                // Устанавливаем цвет для выбранной кнопки
                var selectedButton = (Button)sender;
                selectedButton.BackgroundColor = Color.FromArgb("#2563EB");
                selectedButton.TextColor = Colors.White;

                _selectedRating = int.Parse(selectedButton.Text);
            };

            RatingContainer.Children.Add(button);
        }
    }

    private async Task InitializeFavorite()
    {


        foreach (var item in MediaItem.Review)
        {
            item.User.Avatar = item.User.Avatar.Contains("assets")
                ? $"{ApiClient.GetURL()}/{item.User.Avatar}"
                : $"{ApiClient.GetURL()}/storage/{item.User.Avatar}";
        }

        _checkFavorite = await MediaServiec.CheckFavoriteAsync(MediaItem.Id);

        CheckFavorite.Source = (_checkFavorite) ? "checkfavorites.png" : "close.png";
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // Отписываемся от события при закрытии страницы
        DeviceDisplay.Current.MainDisplayInfoChanged -= OnDisplayInfoChanged;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("../");
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        MediaServiec.AddReviewAsync(MediaItem.Id, ReviewEntry.Text, _selectedRating);
        MediaItem = await MediaServiec.GetMediaAsync(MediaItem.Id);
        BindingContext = MediaItem;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (!_checkFavorite)
        {
            MediaServiec.AddToFavoriteAsync(MediaItem.Id);
            _checkFavorite = true;
        }
        else
        {
            MediaServiec.DeleteFromFavoriteAsync(MediaItem.Id);
            _checkFavorite = false;
        }

        CheckFavorite.Source = (_checkFavorite) ? "checkfavorites.png" : "close.png";
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("actors",
                 new Dictionary<string, object> { { "actors", MediaItem.Actors } });
    }

    private async void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("director",
                 new Dictionary<string, object> { { "director", MediaItem.Directors } });
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = MediaItem;
        InitializeFavorite();
    }
}