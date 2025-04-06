using ApdateFilmUser.Models;
using ApdateFilmUser.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Web;

namespace ApdateFilmUser.Views;

[QueryProperty(nameof(MediaItem), "media")]
public partial class MediaPage : ContentPage
{
    private int _selectedRating = 0;
    public Media MediaItem { get; set; }

    public MediaPage()
	{
		InitializeComponent();
        InitializeRatingControls();
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

    private void OnSubmitReviewClicked(object sender, EventArgs e)
    {
        
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = MediaItem;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("../");
    }
}