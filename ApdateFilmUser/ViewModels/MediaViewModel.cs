using ApdateFilmUser.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApdateFilmUser.ViewModels
{
    public partial class MediaViewModel : ObservableObject
    {
        [ObservableProperty]
        Media media;
        public MediaViewModel(Media media)
        {
            Media = media;
        }
    }
}
