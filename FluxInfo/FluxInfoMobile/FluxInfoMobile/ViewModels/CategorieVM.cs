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
    public class CategorieVM : INotifyPropertyChanged
    {

        #region PROPERTIES

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public Categorie Model { get; set; }
        public string Nom {
            get
            {
                return Model.Nom;
            }
            set
            {
                Model.Nom = value; OnPropertyChanged();
            }
        }

        public bool IsSelect
        {
            get
            {
                return Model.IsSelect;
            }
            set
            {
                Model.IsSelect = value; OnPropertyChanged();
            }
        }

        #endregion

        public CategorieVM(Categorie model) => Model = model;
    }
}
