using FluxInfo.Metier;
using System;
using System.Collections.Generic;

namespace FluxInfoApp.Affichage
{
    public class AfficheurCategories
    {
        public static void AfficherCategorie(IEnumerable<Categorie> categories)
        {
            List<Categorie> categoriesAAfficher = new List<Categorie>();
            categoriesAAfficher.AddRange(categories);
            Console.WriteLine("Voici la liste des catégories");
            foreach(Categorie categorie in categoriesAAfficher)
            {
                Console.Write($"{categoriesAAfficher.IndexOf(categorie)+1} - {categorie.Nom}");
                if (categorie.IsSelect)
                {
                    Console.WriteLine(" - Sélectionnée");
                } else
                {
                    Console.WriteLine(" - Non Sélectionnée");
                }
            }
            Console.WriteLine("");
        }

        public static void AfficherCategorie(Categorie categorie)
        {
            Console.Write(categorie.Nom);
            if (categorie.IsSelect)
            {
                Console.WriteLine(" - Sélectionnée");
            }
            else
            {
                Console.WriteLine(" - Non Sélectionnée");
            }
        }
    }
}
