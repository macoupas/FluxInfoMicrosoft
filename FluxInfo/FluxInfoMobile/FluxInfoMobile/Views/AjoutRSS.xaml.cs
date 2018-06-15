using System;
using FluxInfoMobile.ViewModels;

using Xamarin.Forms;

namespace FluxInfoMobile.View
{
    public partial class AjoutRSS : ContentPage
    {
        FacadeUtilisateurVM FacadeUtilisateurVM = (Application.Current as App).FacadeUtilisateurVM;

        public AjoutRSS()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Permet à la facade de pouvoir faire un PopModal()
            FacadeUtilisateurVM.Navigation = Navigation;
            // Permet à la façade de pouvoir afficher une Alerte sur la page courante
            FacadeUtilisateurVM.Parent = this;
        }
    }
}
