using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoApp.Affichage
{
    public class AfficheurItems
    {
        public static void AfficherItems(List<Item> items)
        {
            if(items.Count == 0)
            {
                Console.WriteLine("Aucun article disponible");
            } else
            {
                Console.WriteLine("Voici les articles :");
            }
            
            foreach (Item item in items)
            {
                
                Console.WriteLine($"{items.IndexOf(item)+1} - {item.Titre}");
                Console.WriteLine(item.Description);
                Console.WriteLine(item.Lien);
                Console.WriteLine("Mots-clés :");
                foreach (MotCle mot in item.MotCles)
                {
                    Console.WriteLine($"\t - {mot.Mot}");
                }
                Console.WriteLine("");
            }
        }

        public static void AfficherItem(Item item)
        {
            Console.WriteLine(item.Titre);
            Console.WriteLine(item.Description);
            Console.WriteLine($"{item.Lien}");
            Console.WriteLine("Mots-clés :");
            foreach(MotCle mot in item.MotCles)
            {
                Console.WriteLine($"\t - {mot.Mot}");
            }
        }
    }
}
