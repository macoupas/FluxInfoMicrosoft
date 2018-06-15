using FluxInfo.Metier;
using FluxInfoApp.Affichage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoApp.Controle
{
    class MenuItem : Menu
    {
        private Item item;

        public MenuItem(Item item, FacadeUtilisateur utilisateur) : base(utilisateur) {
            this.item = item;
        }
        public override void AfficherMenu()
        {
            AfficheurItems.AfficherItem(item);
            Console.WriteLine("Que voulez vous faire ? (0 pour annuler)");
            if (item.EstDansFav)
            {
                Console.WriteLine("1 - Supprimer l'article des favoris");
            } else
            {
                Console.WriteLine("1 - Ajouter l'article aux favoris");
            }
            Console.WriteLine("2 - Affecter un mot clé");
            
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
                        if (item.EstDansFav)
                        {
                            SupprimerFavoris();
                            break;
                        } else
                        {
                            AjouterFavoris();
                            break;
                        }
                    case "2":
                        AffecterMotCle();
                        break;
                    default:
                        Console.Clear();
                        break;
                }

            } while (!choix.Equals("0"));
        }

        private void SupprimerFavoris()
        {
            try
            {
                User.SupprimerItemFavoris(item);
                Console.WriteLine("Article supprimé des favoris avec succès");
            } catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                Console.WriteLine("Echec lors de la suppression de l'article des favoris");
            }
            Console.WriteLine(Menu.PHRASE_CONTINUER);
            Console.ReadKey();
        }

        private void AjouterFavoris()
        {
            try
            {
                User.AjouterItemFavoris(item);
                Console.WriteLine("Article ajouté aux favoris avec succès");
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.StackTrace);
                Console.WriteLine("Echec lors de l'ajout de l'article aux favoris");
            }
            Console.WriteLine(Menu.PHRASE_CONTINUER);
            Console.ReadKey();
        }

        private void AffecterMotCle() {
            List<MotCle> motsCles = new List<MotCle>();
            motsCles.AddRange(User.RecupererMotsCles());
            MenuPlusieursMotsCles menuMot = new MenuPlusieursMotsCles(User, motsCles, item);
            menuMot.ControleMenu();
        }
    }
}
