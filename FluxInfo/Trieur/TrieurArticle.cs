using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trieur
{
    public class TrieurArticle
    {
        public static List<Item> TrierParCategorie(Categorie categorie, FacadeUtilisateur utilisateur)
        {
            IEnumerable<Rss> listeRss = utilisateur.Utilisateur.RssDispo.Where(rss => rss.Channel.Categorie.Equals(categorie));

            List<Item> itemsTrie = new List<Item>();
            foreach(Rss rss in listeRss)
            {
                itemsTrie.AddRange(rss.Channel.Items);
            }

            return itemsTrie;
        }

        public static IEnumerable<Item> TrierParMotCle(MotCle mot, FacadeUtilisateur utilisateur)
        {
            IEnumerable<Item> items = utilisateur.Utilisateur.RecupererTousLesArticles();
            return items.Where(item => item.MotCles.Contains(mot));
        }
    }
}
