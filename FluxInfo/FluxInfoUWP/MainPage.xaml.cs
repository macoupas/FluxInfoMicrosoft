using FluxInfo.Metier;
using FluxInfoUWP.View;
using FluxInfoUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FluxInfoUWP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        FacadeUtilisateurVM FacadeUtilisateurVM = (Application.Current as App).FacadeUtilisateurVM;

        public MainPage()
        {
            DataContext = FacadeUtilisateurVM;
            this.InitializeComponent();
        }


        private void GoToRss(object sender, RoutedEventArgs e)
        {   
            Frame.Navigate(typeof(RssView));
        }

        private void onGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var columns = Math.Ceiling(ActualWidth / 500);
            ((ItemsWrapGrid)articlesGrille.ItemsPanelRoot).ItemWidth = e.NewSize.Width / columns - 15;
        }
       
    }
}
