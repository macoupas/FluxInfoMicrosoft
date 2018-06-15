using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoMobile.ViewModels
{
    public class ChannelVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public Channel Model { get; }

        public string Title {
            get
            {
                return Model.Title;
            }
            set
            {
                Model.Title = value; OnPropertyChanged();
            }
        }

        public string Description {
            get
            {
                return Model.Description;
            }
            set
            {
                Model.Description = value; OnPropertyChanged();
            }
        }

        public string Lien {
            get
            {
                return Model.Lien;
            }
            set
            {
                Model.Lien = value; OnPropertyChanged();
            }
        }

        public CategorieVM CategorieVM { get; set; }

        private ObservableCollection<ItemVM> itemsVM = new ObservableCollection<ItemVM>();
        public ObservableCollection<ItemVM> ItemsVM { get { return itemsVM; } }

        public ChannelVM(Channel channel)
        {
            Model = channel;
            CategorieVM = new CategorieVM(Model.Categorie);
            foreach(Item i in Model.Items)
            {
                itemsVM.Add(new ItemVM(i));
            }
        }
    }
}
