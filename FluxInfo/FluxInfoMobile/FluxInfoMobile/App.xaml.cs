using FluxInfoMobile.View;
using FluxInfoMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FluxInfoMobile
{
    public partial class App : Application
	{
        internal FacadeUtilisateurVM FacadeUtilisateurVM { get; set; } = new FacadeUtilisateurVM(new Stub.Stub());

        public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
