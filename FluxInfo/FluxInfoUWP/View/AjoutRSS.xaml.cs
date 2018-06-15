using FluxInfoUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace FluxInfoUWP.View
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AjoutRSS : Page
    {
        FacadeUtilisateurVM FacadeUtilisateurVM = (App.Current as App).FacadeUtilisateurVM;
        RssVM RssVM { get; set; }
        public AjoutRSS()
        {
            this.InitializeComponent();
            RssVM = FacadeUtilisateurVM.SelectedRss;
            FacadeUtilisateurVM.RssChangedEvent += RssChanged;
        }

        private void RssChanged()
        {
            RssVM = FacadeUtilisateurVM.SelectedRss;
            lien.Text = RssVM.ChannelVM.Lien;
            titre.Text = RssVM.ChannelVM.Title;
            description.Text = RssVM.ChannelVM.Description;
        }
    }
}
