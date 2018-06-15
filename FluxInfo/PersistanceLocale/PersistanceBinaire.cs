using FluxInfo.persistance;
using System;
using FluxInfo.Metier;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PersistanceLocale
{
    public class PersistanceBinaire : IDataPersistance
    {
        private const string NOM_FICHIER = "utilisateur.bin";
        public Utilisateur Charger()
        {
            
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead(NOM_FICHIER))
            {
                return formatter.Deserialize(stream) as Utilisateur;
            }
        }

        public void Sauvegarder(Utilisateur utilisateur)
        {
            

            string file = NOM_FICHIER;
            IFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create(file))
            {
                formatter.Serialize(stream, utilisateur);
            }
        }
    }
}
