using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluxInfo.Metier;
using Trieur;
using FluxInfoApp.Affichage;

namespace FluxInfoApp.Controle
{
    class MenuMotCle : Menu
    {
        private MotCle motCle;

        public MenuMotCle(FacadeUtilisateur utilisateur, MotCle motCle) : base(utilisateur)
        {
            this.motCle = motCle;
        }

        public override void AfficherMenu()
        {
            AfficheurMotCle.AfficherMotCle(motCle);
            Console.WriteLine("Que voulez vous faire ? (0 pour annuler)");
            Console.WriteLine("1 - Supprimer ce mot clé");
            Console.WriteLine("2 - Afficher les articles affectés à ce mot clé");
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
                        SupprimerMotCle();
                        break;

                    case "2":
                        AfficherParMotCle();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            } while (!choix.Equals("0"));
        }

        private void SupprimerMotCle()
        {
            User.SupprimerMotCle(motCle);
            List<Item> items = new List<Item>();
            items.AddRange(TrieurArticle.TrierParMotCle(motCle, User));
            foreach(Item item in items){
                item.SupprimerMotCle(motCle);
            }
            Console.Clear();
            MenuPlusieursMotsCles menu = new MenuPlusieursMotsCles(User, User.RecupererMotsCles());
            menu.ControleMenu();
        }

        private void AfficherParMotCle()
        {
            List<Item> items = new List<Item>();
            items.AddRange(TrieurArticle.TrierParMotCle(motCle, User));

            AfficheurItems.AfficherItems(items);
            SelectionnerArticle(items);
        }
    }
}
