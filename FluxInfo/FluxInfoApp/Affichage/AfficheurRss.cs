using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoApp.Affichage
{
    public class AfficheurRss
    {
        public static void AfficherRss(Rss rss)
        {
            Console.WriteLine(rss.Channel.Title);
            Console.WriteLine(rss.Channel.Description);
            Console.WriteLine($"{rss.Channel.Lien}\n");
        }
    }
}
