using FluxInfo.Metier;
using FluxInfoApp.Controle;
using PersistanceLocale;

namespace FluxInfoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FacadeUtilisateur facadeUtilisateur = new FacadeUtilisateur(new PersistanceBinaire());

            Menu menu = new MenuPrincipale(facadeUtilisateur);
            menu.ControleMenu();
        }
    }
}
