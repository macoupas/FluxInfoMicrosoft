using FluxInfo.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FluxInfo.Metier
{
    [DataContract]
    public class Utilisateur
    {
        private const string NOM_FICHIER = "donneesUtilisateur.txt";


        [DataMember]
        private List<Categorie> categories = new List<Categorie>();
        public IEnumerable<Categorie> Categories
        {
            get
            {
                return categories.AsReadOnly();
            }
        }

        [DataMember]
        private List<Item> articlesFavoris = new List<Item>();
        public IEnumerable<Item> ArticlesFavoris
        {
            get
            {
                return articlesFavoris.AsReadOnly();
            }
        }

        [DataMember]
        private List<MotCle> motsCles = new List<MotCle>();
        public IEnumerable<MotCle> MotsCles
        {
            get
            {
                return motsCles.AsReadOnly();
            }
        }

        [DataMember]
        private List<Rss> rssDispo = new List<Rss>();
        public IEnumerable<Rss> RssDispo
        {
            get
            {
                return rssDispo.AsReadOnly();
            }
        }

        internal bool AjouterRSS(Rss rss)
        {
            if (!rssDispo.Contains(rss))
            {
                rssDispo.Add(rss);
                if (!rss.Channel.Categorie.Equals(null))
                {
                    categories.Add(rss.Channel.Categorie);
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        internal bool SupprimerRSS(int index)
        {
            if (index <= 0 || index > rssDispo.Count)
            {
                return false;
            }
            else
            {
                if (!rssDispo.Remove(rssDispo[index - 1]))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        internal void DecocherCocherCategorie(Categorie categorie)
        {
            categorie.IsSelect = !categorie.IsSelect;
        }

        internal IEnumerable<Item> RecupererTousLesArticles()
        {
            List<Item> items = new List<Item>();

            foreach (Rss rss in RssDispo)
            {
                foreach (Item item in rss.Channel.Items)
                {
                    items.Add(item);
                }
            }
            return items.AsReadOnly();
        }

        internal IEnumerable<Item> RecupererArticles()
        {
            List<Item> items = new List<Item>();

            foreach (Rss rss in RssDispo)
            {
                if (rss.Channel.Categorie.IsSelect)
                {
                    foreach (Item item in rss.Channel.Items)
                    {
                        items.Add(item);
                    }
                }
            }
            return items.AsReadOnly();
        }

        internal void AjouterCategorie(Categorie categorie)
        {
            if (!categories.Contains(categorie))
            {
                categories.Add(categorie);
            }
        }

        internal IEnumerable<Item> RecupererArticlesFavoris()
        {
            return ArticlesFavoris;
        }

        internal void AjouterItemFavoris(Item item)
        {
            if (articlesFavoris.Contains(item))
            {
                throw new Exception("Article déjà existant");
            }
            else
            {
                item.EstDansFav = true;
                articlesFavoris.Add(item);
            }
        }

        internal void SupprimerItemFavoris(Item item)
        {
            if (!articlesFavoris.Remove(item))
            {
                throw new Exception("Article inexistant");
            }
            else
            {
                item.EstDansFav = false;
            }
        }

        internal void AjouterMotCle(MotCle mot)
        {
            if (motsCles.Contains(mot))
            {
                throw new Exception("Element déjà existant");
            }
            else
            {
                motsCles.Add(mot);
            }
        }

        internal void SupprimerMotCLe(int index)
        {
            if (index < 0 || index >= motsCles.Count)
            {
                throw new IndexOutOfRangeException("Index incorrect");
            }
            else
            {
                if (!motsCles.Remove(motsCles[index]))
                {
                    throw new Exception("Element inextistant");
                }
            }
        }

        internal void SupprimerMotCLe(MotCle motCle)
        {
            if (!motsCles.Remove(motCle))
            {
                throw new Exception("Problème lors de la suppression");
            }
        }
    }
}
