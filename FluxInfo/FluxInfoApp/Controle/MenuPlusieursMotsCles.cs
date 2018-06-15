using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluxInfo.Metier;
using System.Diagnostics;
using FluxInfoApp.Affichage;

namespace FluxInfoApp.Controle
{
    class MenuPlusieursMotsCles : Menu
    {
        private List<MotCle> motCles = new List<MotCle>();
        private Item item;

        public MenuPlusieursMotsCles(FacadeUtilisateur utilisateur, IEnumerable<MotCle> motCles) : base(utilisateur)
        {
            this.motCles.AddRange(motCles);
        }

        public MenuPlusieursMotsCles(FacadeUtilisateur utilisateur, IEnumerable<MotCle> motCles, Item item) : base(utilisateur)
        {
            this.motCles.AddRange(motCles);
            this.item = item;
        }

        public override void AfficherMenu()
        {
            AfficheurMotCle.AfficherMotCles(motCles);
            Console.WriteLine("Que voulez-vous faire ? (0 pour annuler)");
            if(motCles.Count == 0)
            {
                Console.WriteLine("1 - Ajouter un mot clé");
            } else
            {
                Console.WriteLine("1 - Selectionner un mot clé");
                Console.WriteLine("2 - Ajouter un mot clé");
            }
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
                        if (motCles.Count == 0)
                        {
                            AjouterMotCle();
                        }
                        else
                        {
                            SelectionnerMotCle();
                        }
                        break;
                    case "2":
                        if(motCles.Count != 0)
                        {
                            AjouterMotCle();
                        }
                        break;
                    default:
                        if(item == null)
                        {
                            Console.Clear();
                        }
                        
                        break;
                }
            } while (!choix.Equals("0"));
        }

        private void SelectionnerMotCle()
        {
            Console.WriteLine("Sélectionnez un mot clé avec son numéro : (0 pour annuler)");
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
                SelectionnerMotCle();
                Debug.WriteLine(fe.StackTrace);
            }

            if (num == 0)
            {
                Console.Clear();
                return;
            }
            else
            {
                if (num > motCles.Count)
                {
                    Console.WriteLine("Veuillez entrez un nombre inférieur au nombre d'articles.");
                    Console.WriteLine(PHRASE_CONTINUER);
                    Console.ReadKey();
                    SelectionnerMotCle();
                }
                else
                {
                    Console.Clear();
                    if(item != null)
                    {
                        try
                        {
                            item.AjouterMotCle(motCles[num - 1]);
                            Console.WriteLine("Mot clé affecté avec succés");
                        } catch (Exception e)
                        {
                            Console.WriteLine("Echec de l'affectation");
                            Debug.WriteLine(e.StackTrace);
                        }  
                    } else
                    {
                        Menu menuMotCle = new MenuMotCle(User, motCles[num - 1]);
                        menuMotCle.ControleMenu();
                    }
                    
                }
            }
        }

        private void AjouterMotCle()
        {
            Console.WriteLine("Entrez le mot clé à ajouter :");
            String mot;
            mot = Console.ReadLine();

            MotCle motCle = new SousMotCle(mot);

            try
            {
                User.AjouterMotCle(motCle);
                motCles.Add(motCle);
                Console.WriteLine("Mot clé ajouté avec succés");
            } catch (Exception e)
            {
                Console.WriteLine("Mot clé déjà existant");
                Debug.WriteLine(e.StackTrace);
            }

            if(item != null)
            {
                item.AjouterMotCle(motCle);
                Console.WriteLine("Le mot clé à été automatiquement ajouté à l'article\n");
            }

            Console.WriteLine(Menu.PHRASE_CONTINUER);
            Console.ReadKey();
            Console.Clear();
        }
    }
}
