using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoMobile.ViewModels
{
    public class ItemVM : INotifyPropertyChanged
    {

        #region PROPERTIES

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public Item Model { get; }

        public string Titre
        {
            get
            {
                return Model.Titre;
            }
            set
            {
                Model.Titre = value; OnPropertyChanged();
            }
        }

        public string Description
        {
            get
            {
                return Model.Description;
            }
            set
            {
                Model.Description = value; OnPropertyChanged();
            }
        }

        public string LienImage
        {
            get
            {
                if(Model.LienImage == null)
                {
                    return "";
                }
                return Model.LienImage;         
            }
            set
            {
                Model.LienImage = value; OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get
            {
                return Model.Date;
            }
            set
            {
                Model.Date = value; OnPropertyChanged();
            }
        }

        public string Lien
        {
            get
            {
                return Model.Lien;
            }
            set
            {
                Model.Lien = value; OnPropertyChanged();
            }
        }

        public bool EstDansFavoris
        {
            get
            {
                return Model.EstDansFav;
            }
            set
            {
                Model.EstDansFav = value; OnPropertyChanged();
            }
        }

        #endregion

        public ItemVM(Item item) => Model = item;

    }
}
