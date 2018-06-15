using FluxInfo.Metier;
using FluxInfoApp.Affichage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trieur;

namespace FluxInfoApp.Controle
{
    class MenuCategorie : Menu
    {

        private Categorie categorie;

        public MenuCategorie(Categorie categorie, FacadeUtilisateur utilisateur) : base(utilisateur)
        {
            this.categorie = categorie;
        }

        public override void AfficherMenu()
        {
            AfficheurCategories.AfficherCategorie(categorie);
            Console.WriteLine("Que voulez vous faire ? (0 pour annuler)");
            Console.WriteLine("1 - Cocher / Décocher");
            Console.WriteLine("2 - Afficher les articles en rapport avec cette catégorie");
        }

        public override void ControleMenu()
        {
            string choix;
            do
            {
                AfficherMenu();
                choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        CocherDecocher();
                        break;
                    case "2":
                        TrierParCategorie();
                        break;
                    default:
                        Console.Clear();
                        break;
                }

            } while (!choix.Equals("0"));
        }

        private void CocherDecocher()
        {
            User.DecocherCocherCategorie(categorie);
            Console.Clear();
        }

        private void TrierParCategorie()
        {
            List<Item> items = TrieurArticle.TrierParCategorie(categorie, User);

            AfficheurItems.AfficherItems(items);
            SelectionnerArticle(items);
        }
    }
}
