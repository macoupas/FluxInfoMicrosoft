using System;
using System.Collections.Generic;
using FluxInfoMobile.ViewModels;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace FluxInfoMobile.View
{
    public partial class FluxRss : ContentPage
    {
        FacadeUtilisateurVM FacadeUtilisateurVM = (Application.Current as App).FacadeUtilisateurVM;

        public FluxRss()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Permet à la facade de pouvoir faire un PopModal()
            FacadeUtilisateurVM.Navigation = Navigation;
            BindingContext = FacadeUtilisateurVM;
        }

        // Event lors du clique sur un Flux Rss
        // Permet d'afficher la vue de modification d'un FluxRss
        void OnListItemTapped(object sender, ItemTappedEventArgs args) {
            if (args.Item is RssVM)
            {
                FacadeUtilisateurVM.Ajout = false;
                FacadeUtilisateurVM.SelectedRss = args.Item as RssVM;
                Navigation.PushModalAsync(new NavigationPage(new AjoutRSS
                {
                    BindingContext = FacadeUtilisateurVM
                }));
            }
		}

        // Event sur le bouton '+'
        // Permet d'afficher la vue d'ajout d'un Flux Rss
		async void OnItemAdded(object sender, EventArgs args) {
            FacadeUtilisateurVM.Ajout = true;
            FacadeUtilisateurVM.SelectedRssIndex = -1;
            await Navigation.PushModalAsync(new NavigationPage(new AjoutRSS
            {
                BindingContext = FacadeUtilisateurVM
            }));
		}

        // Event lors de la selection d'un item dans le list
        // Modifie le SelectedRssIndex de la façade
        void OnItemSelected(object sender, SelectedPositionChangedEventArgs args) {
            var index = (listView.ItemsSource as ObservableCollection<RssVM>).IndexOf((args.SelectedPosition as RssVM));
            FacadeUtilisateurVM.SelectedRssIndex = index;
        }
    }
}
