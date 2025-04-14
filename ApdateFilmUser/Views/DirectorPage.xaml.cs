using ApdateFilmUser.Models;
using ApdateFilmUser.Servieces;
using ApdateFilmUser.ViewModels;
using System.Threading.Tasks;

namespace ApdateFilmUser.Views;

[QueryProperty(nameof(DirectorsItem), "director")]
public partial class DirectorPage : ContentPage
{

    public List<Director> DirectorsItem { get; set; }
	public DirectorPage()
	{
		InitializeComponent();
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        foreach (var item in DirectorsItem)
        {
            var d = await MediaServiec.GetDirectorAsync(item.Id);
            item.Media = d.Media;
        }
        var vm = new DirectorsViewModel(DirectorsItem);
        BindingContext = vm;
    }
}