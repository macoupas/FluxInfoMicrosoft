using FluxInfo.Metier;
using FluxInfo.Parser;
using FluxInfoApp.Affichage;
using PersistanceLocale;
using RssParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoApp.Controle
{
    class MenuPrincipale : Menu
    {
        public MenuPrincipale(FacadeUtilisateur facadeUtilisateur) : base(facadeUtilisateur)
        {

        }

        public override void AfficherMenu()
        {
            Console.WriteLine("Bienvenue dans votre espace FluxInfo");
            Console.WriteLine("Choisissez une action à faire : (0 pour quitter)");
            Console.WriteLine("1 - Ajouter un flux rss");
            Console.WriteLine("2 - Supprimer un flux rss");
            Console.WriteLine("3 - Afficher les articles");
            Console.WriteLine("4 - Afficher les articles favoris");
            Console.WriteLine("5 - Afficher les catégories");
            Console.WriteLine("6 - Afficher les mots-clés");
            Console.WriteLine("7 - Sauvegarder");
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
                        AjouterRSS();
                        break;
                    case "2":
                        SupprimerRSS();
                        break;
                    case "3":
                        AfficherArticles();
                        break;
                    case "4":
                        AfficherArticlesFavoris();
                        break;
                    case "5":
                        AfficherCategories();
                        break;
                    case "6":
                        AfficherMotCles();
                        break;
                    case "7":
                        Sauvegarder();
                        break;
                    default:
                        Console.Clear();
                        break;
                }

            } while (!choix.Equals("0"));
        }

        private void Sauvegarder()
        {
            PersistanceBinaire persistance = new PersistanceBinaire();
            persistance.Sauvegarder(User.Utilisateur);
        }

        private void AjouterRSS()
        {
            Console.Clear();
            Console.WriteLine("Entrez l'url du flux rss :");
            string url = Console.ReadLine();
            IRssParser parser = new RssParserWithLinQ();
            try
            {
                Rss rss = parser.ParserRSS(url);
                if (User.AjouterRSS(rss))
                {
                    DetailRss(rss);
                    Console.WriteLine("Ajout du flux avec succès !");
                }
                else
                {
                    Console.WriteLine("L'ajout du flux est un échec");
                }
                Console.WriteLine(PHRASE_CONTINUER);
                Console.ReadKey();
                Console.Clear();
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine("Problème lors de l'ajout du flux");
                Console.WriteLine(PHRASE_CONTINUER);
                Console.ReadKey();
            }
            
        }

        private void DetailRss(Rss rss)
        {
            Console.Clear();
            Console.WriteLine("Voici les détails du flux que vous avez ajouté :");
            AfficheurRss.AfficherRss(rss);
            Console.WriteLine("Entrez le nom de la catégorie du flux rss");
            Categorie categorie = new Categorie(Console.ReadLine());
            User.AjouterCategorieRss(rss, categorie);
            User.AjouterCategorieUtilisateur(categorie);
        }

        private void SupprimerRSS()
        {
            Console.Clear();
            Console.WriteLine("Voici la liste des flux rss:");
            List<Rss> listRss = new List<Rss>();
            listRss.AddRange(User.RecupererRss());
            foreach (Rss rss in listRss)
            {

                Console.WriteLine($"{listRss.IndexOf(rss) + 1} - {rss.Channel.Lien}");
            }
            Console.WriteLine("Entrez le numéro du flux rss à supprimer : (0 pour annuler)");
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
                SupprimerRSS();
                Debug.WriteLine(fe.StackTrace);
            }
            if (num == 0)
            {
                Console.Clear();
                return;
            }

            if (User.SupprimerRSS(num))
            {
                Console.WriteLine("Suppression effectuée avec succès");
            }
            else
            {
                Console.WriteLine("Echec lors de la suppression");
            }

            Console.WriteLine(PHRASE_CONTINUER);
            Console.ReadKey();
            Console.Clear();
        }

        private void AfficherArticles()
        {
            List<Item> itemsAAfficher = new List<Item>();
            itemsAAfficher.AddRange(User.RecupererArticles());
            Console.WriteLine("Voici la liste des articles : \n");
            AfficheurItems.AfficherItems(itemsAAfficher);
            SelectionnerArticle(itemsAAfficher);
            Console.Clear();
        }

        private void AfficherArticlesFavoris()
        {
            List<Item> itemsAAfficher = new List<Item>();
            itemsAAfficher.AddRange(User.RecupererArticlesFavoris());
            Console.WriteLine("Voici la liste des articles : \n");
            AfficheurItems.AfficherItems(itemsAAfficher);
            SelectionnerArticle(itemsAAfficher);
            Console.Clear();
        }

        

        private void AfficherCategories()
        {
            Console.Clear();
            List<Categorie> categoriesAAfficher = new List<Categorie>();
            categoriesAAfficher.AddRange(User.RecupererCategorie());
            Console.WriteLine("Voici la liste des catégories : \n");
            AfficheurCategories.AfficherCategorie(categoriesAAfficher);
            SelectionnerCategorie(categoriesAAfficher);
            Console.Clear();
        }

        private void AfficherMotCles()
        {
            Console.Clear();
            List<MotCle> motCleAAfficher = new List<MotCle>();
            motCleAAfficher.AddRange(User.RecupererMotsCles());
            MenuPlusieursMotsCles menuMotCle = new MenuPlusieursMotsCles(User, motCleAAfficher);
            menuMotCle.ControleMenu();
        }

        private void SelectionnerCategorie(List<Categorie> categories)
        {
            Console.WriteLine("Sélectionnez une catégorie avec son numéro : (0 pour annuler)");
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
                SelectionnerCategorie(categories);
                Debug.WriteLine(fe.StackTrace);
            }

            if (num == 0)
            {
                Console.Clear();
                return;
            }
            else
            {
                if (num > categories.Count)
                {
                    Console.WriteLine("Veuillez entrez un nombre inférieur au nombre d'articles.");
                    Console.WriteLine(PHRASE_CONTINUER);
                    Console.ReadKey();
                    SelectionnerCategorie(categories);
                }
                else
                {
                    Console.Clear();
                    Menu menuCategorie = new MenuCategorie(categories[num - 1], User);
                    menuCategorie.ControleMenu();
                }
            }
        }
    }
}
