using ApdateFilmUser.Models;
using ApdateFilmUser.Servieces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApdateFilmUser.ViewModels
{
    public partial class CatalogViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Media> medias;

        public CatalogViewModel()
        {
            Medias = new ObservableCollection<Media>();
            LoadMedia();
        }

        async Task LoadMedia()
        {
            var list = await MediaServiec.GetMediaAsync();

            foreach (var media in list)
            {
                Medias.Add(media);
            }
            
        }
        
    }
}
