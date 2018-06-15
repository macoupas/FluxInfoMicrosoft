using FluxInfo.Metier;
using FluxInfo.Parser;
using FluxInfoApp.Affichage;
using RssParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoApp.Controle
{
    abstract class Menu
    {
        public const string PHRASE_CONTINUER = "Appuyez sur une touche pour continuer...";

        internal FacadeUtilisateur User { get; set; }

        public Menu(FacadeUtilisateur utilisateur) => User = utilisateur;

        public virtual void AfficherMenu() { }
        public virtual void ControleMenu() { }

        internal void SelectionnerArticle(List<Item> items)
        {
            Console.WriteLine("Sélectionnez un article avec son numéro : (0 pour annuler)");
            int num = 0;
            try
            {
                num = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Veuillez entrez un numéro");
                Console.WriteLine(PHRASE_CONTINUER);
                Console.ReadKey();
                SelectionnerArticle(items);
                Debug.WriteLine(fe.StackTrace);
            }

            if (num == 0)
            {
                Console.Clear();
                return;
            }
            else
            {
                if (num > items.Count)
                {
                    Console.WriteLine("Veuillez entrez un nombre inférieur au nombre d'articles.");
                    Console.WriteLine(PHRASE_CONTINUER);
                    Console.ReadKey();
                    SelectionnerArticle(items);
                }
                else
                {
                    Console.Clear();
                    Menu menuItem = new MenuItem(items[num - 1], User);
                    menuItem.ControleMenu();

                }
            }
        }
    }
}
