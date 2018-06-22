using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoMobile.ViewModels
{
    public class UtilisateurVM : INotifyPropertyChanged
    {

        #region PROPERTIES

        public event PropertyChangedEventHandler PropertyChanged;

        public Utilisateur Model { get; set; }

        public IEnumerable<ItemVM> ArticlesFavoris { get; set; }

        #endregion

        public UtilisateurVM(Utilisateur model)
        {
            Model = model;
        }

        /// <summary>
        /// Permet d'ajouter un <see cref="RssVM"/> au model
        /// </summary>
        /// <param name="rss"></param>
        public void AjouterRss(RssVM rss)
        {
            Model.AjouterRSS(rss.Model);
        }

        /// <summary>
        /// Permet de récuperer les articles en prenant tous les flux <see cref="Rss"/>
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ItemVM> RecupererArticles()
        {
            IEnumerable<Item> tmp =  Model.RecupererArticles();

            ObservableCollection<ItemVM> result = new ObservableCollection<ItemVM>();
            foreach(Item i in tmp)
            {
                result.Add(new ItemVM(i));
            }
            result = new ObservableCollection<ItemVM>(result.OrderByDescending(item => item.Date));
            return result;
        }

        /// <summary>
        /// Permet de récuperer tous les flux <see cref="RssVM"/> de l'utilisateur
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<RssVM> RssDispo()
        {
            IEnumerable<Rss> tmp = Model.RssDispo;

            ObservableCollection<RssVM> result = new ObservableCollection<RssVM>();
            foreach (Rss i in tmp)
            {
                result.Add(new RssVM(i));
            }
            return result;
        }
    }
}
