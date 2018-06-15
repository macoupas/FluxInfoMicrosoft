using System;
using FluxInfo.Metier;
using FluxInfo.persistance;
using FluxInfo.Parser;
using RssParser;

namespace Stub
{
    public class Stub : IDataPersistance
    { 

        public Utilisateur Charger()
        {
            Utilisateur utilisateur = new Utilisateur();
            IRssParser parser = new RssParserWithLinQ();

            Rss rss1 = parser.ParserRSS("http://www.lemonde.fr/jeux-video/rss_full.xml");
            rss1.Channel.Categorie = new Categorie("Jeux vidéo");

            utilisateur.AjouterRSS(rss1);

            return utilisateur;
        }

        public void Sauvegarder(Utilisateur utilisateur)
        {
            throw new NotImplementedException();
        }
    }
}
