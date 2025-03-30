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

    public Media MediaItem { get; set; }

    public MediaPage()
	{
		InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        BindingContext = MediaItem;
    }




}