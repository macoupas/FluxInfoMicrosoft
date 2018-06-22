using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FluxInfoMobile.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleCell : ViewCell
	{

        /// <summary>
        /// Titre de l'article bindé avec la vue principale
        /// </summary>
        public static readonly BindableProperty TitreArticleProperty =
        BindableProperty.Create("TitreArticle", typeof(string), typeof(ArticleCell), "TitreArticle");

        public string TitreArticle
        {
            get { return (string)GetValue(TitreArticleProperty); }
            set { SetValue(TitreArticleProperty, value); }
        }

        /// <summary>
        /// Lien de l'image de l'article bindé avec la vue principale
        /// </summary>
        public static readonly BindableProperty LienImageArticleProperty =
        BindableProperty.Create("LienImageArticle", typeof(string), typeof(ArticleCell), "LienImageArticle");

        public string LienImageArticle
        {
            get { return (string)GetValue(LienImageArticleProperty); }
            set { SetValue(LienImageArticleProperty, value); }
        }

        public ArticleCell ()
		{
			InitializeComponent ();
		}
    }
}