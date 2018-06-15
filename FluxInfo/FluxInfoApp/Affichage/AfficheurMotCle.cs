using FluxInfo.Metier;
using System;
using System.Collections.Generic;

namespace FluxInfoApp.Affichage
{
    class AfficheurMotCle
    {
        public static void AfficherMotCles(List<MotCle> motsCles)
        {
            Console.WriteLine("Voici la liste des mots-clés :");
            foreach(MotCle mot in motsCles)
            {
                Console.WriteLine($"{motsCles.IndexOf(mot)+1} - {mot.Mot}");
            }
            Console.WriteLine("");
        }

        public static void AfficherMotCle(MotCle mot)
        {
            Console.WriteLine($"Mot clé sélectionné : {mot.Mot} \n");
        }
    }
}
