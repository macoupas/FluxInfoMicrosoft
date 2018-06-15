using FluxInfo.Metier;
using System;
using System.Collections.Generic;
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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace FluxInfoUWP.UserControls
{
    public sealed partial class ArticleUC : UserControl
    {

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ArticleUC), new PropertyMetadata("Titre"));



        public string LienImage
        {
            get { return (string)GetValue(LienImageProperty); }
            set { SetValue(LienImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LienImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LienImageProperty =
            DependencyProperty.Register("LienImage", typeof(string), typeof(ArticleUC), new PropertyMetadata("https://cdn1.astuces-pratiques.fr/imagesarticles/55/erreur-404-0.jpg"));


        public ArticleUC()
        {
            this.InitializeComponent();
        }
    }
}
