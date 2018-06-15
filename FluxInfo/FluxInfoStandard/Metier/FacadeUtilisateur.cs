using FluxInfo.persistance;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RssParserStandard")]
[assembly:InternalsVisibleTo("StubStandard")]
[assembly: InternalsVisibleTo("Trieur")]
[assembly: InternalsVisibleTo("TestUnitaire")]
[assembly: InternalsVisibleTo("FluxInfoUWP")]
[assembly: InternalsVisibleTo("FluxInfoMobile")]
namespace FluxInfo.Metier
{
    /// <summary>
    /// FacadeUtilisateur est une façade permettant de déléguer les méthodes à
    /// l'Utilisateur <see cref="Utilisateur"/> que les programmes extérieurs vont utiliser.
    /// </summary>
    
    public class FacadeUtilisateur
    {
        public Utilisateur Utilisateur { get; internal set; }

        public IEnumerable<Item> LesArticles {
            get
            {
                return RecupererArticles();
            }
        }
        
        public FacadeUtilisateur(IDataPersistance persistance)
        {
            Utilisateur = persistance.Charger();
            if (Utilisateur == null)
            {
                Utilisateur = new Utilisateur();
            }
        }

        public bool AjouterRSS(Rss rss)
        {
            return Utilisateur.AjouterRSS(rss);
        }

        public bool SupprimerRSS(int index)
        {
            return Utilisateur.SupprimerRSS(index);
        }

        public IEnumerable<Item> RecupererArticles()
        {
            return Utilisateur.RecupererArticles();
        }

        public IEnumerable<Item> RecupererArticlesFavoris()
        {
            return Utilisateur.RecupererArticlesFavoris();
        }

        public void AjouterItemFavoris(Item item)
        {
            Utilisateur.AjouterItemFavoris(item);
        }

        public void SupprimerItemFavoris(Item item)
        {
            Utilisateur.SupprimerItemFavoris(item);
        }

        public void AjouterCategorieUtilisateur(Categorie categorie)
        {
            Utilisateur.AjouterCategorie(categorie);
        }

        public void AjouterMotCle(MotCle mot)
        {
            Utilisateur.AjouterMotCle(mot);
        }

        public void SupprimerMotCle(int index)
        {
            Utilisateur.SupprimerMotCLe(index);
        }

        public void SupprimerMotCle(MotCle motCle)
        {
            Utilisateur.SupprimerMotCLe(motCle);
        }

        public void DecocherCocherCategorie(Categorie categorie)
        {
            Utilisateur.DecocherCocherCategorie(categorie);
        }

        public ReadOnlyCollection<MotCle> RecupererMotsCles()
        {
            return Utilisateur.MotsCles as ReadOnlyCollection<MotCle>;
        }

        public void AjouterCategorieRss(Rss rss, Categorie categorie)
        {
            rss.Channel.Categorie = categorie;
        }

        public ReadOnlyCollection<Rss> RecupererRss()
        {
            return Utilisateur.RssDispo as ReadOnlyCollection<Rss>;
        }

        public ReadOnlyCollection<Categorie> RecupererCategorie()
        {
            return Utilisateur.Categories as ReadOnlyCollection<Categorie>;
        }
    }
}
