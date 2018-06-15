using FluxInfoMobile.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FluxInfoMobile.View
{
    public partial class DetailArticle : ContentPage
    {
        public FacadeUtilisateurVM FacadeUtilisateurVM => (Application.Current as App).FacadeUtilisateurVM;
        public ItemVM ItemVM { get; internal set; }

        public DetailArticle(ItemVM article)
        {
            InitializeComponent();
            ItemVM = article;
            BindingContext = this;
        }
    }
}
