using FluxInfoMobile.ViewModels;
using Xamarin.Forms;

namespace FluxInfoMobile.View
{
    public partial class Actualites : ContentPage
    {
        FacadeUtilisateurVM FacadeUtilisateurVM = (Application.Current as App).FacadeUtilisateurVM;

        public Actualites()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = FacadeUtilisateurVM;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e.Item is ItemVM)
            {
                DetailArticle detailArticle = new DetailArticle(e.Item as ItemVM);
                NavigationPage.SetHasBackButton(detailArticle, true);
                Navigation.PushModalAsync(new NavigationPage(detailArticle));
            }
        }
    }
}
