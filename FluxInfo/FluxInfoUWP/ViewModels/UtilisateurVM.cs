using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoUWP.ViewModels
{
    class UtilisateurVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Utilisateur Model { get; set; }

        public IEnumerable<CategorieVM> Categories { get; set; }

        public IEnumerable<ItemVM> ArticlesFavoris { get; set; }

        public IEnumerable<MotCleVM> MotsCles { get; set; }

        public UtilisateurVM(Utilisateur model)
        {
            Model = model;
        }

        public void AjouterRss(RssVM rss)
        {
            Model.AjouterRSS(rss.Model);
        }

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
