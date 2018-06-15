using FluxInfo.Metier;

namespace FluxInfo.persistance
{
    public interface IDataPersistance
    {
        void Sauvegarder(Utilisateur utilisateur);
        Utilisateur Charger();
    }
}
