using System;
using Xamarin.Forms;

using FluxInfo.Metier;
using FluxInfo.persistance;
using RssParser;
using FluxInfoMobile.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluxInfoMobile.View;

namespace FluxInfoMobile.ViewModels
{
    /// <summary>
    /// La classe <see cref="FacadeUtilisateurVM" /> permet de faire le lien entre les vues
    /// et le model <see cref="FacadeUtilisateur"/>.
    /// </summary>
    public class FacadeUtilisateurVM : INotifyPropertyChanged
    { 
        #region COMMANDS

        /// <summary>
        /// Commande qui permet de parser un flux <see cref="Rss"/> donné par un lien.
        /// </summary>
        public RelayCommand<String> ParserCommand
        {
            get
            {
                return new RelayCommand<String>(
                    (lien) =>
                    {
                        try
                        {
                            parser.ParserRSS(lien);
                            
                            
                        }
                        catch (Exception)
                        {
                            if (Parent != null)
                            Parent.DisplayAlert("Erreur", "Url invalide", "OK");
                            RssValid = false;
                        }
                    },
                    (lien) => {
                        return lien != null && lien != "";
                    }
                );
            }
        }

        /// <summary>
        /// Commande qui permet d'ouvrir une page web dans le navigateur du téléphone en passant le lien en paramètre
        /// </summary>
        public RelayCommand<String> OpenUrl
        {
            get
            {
                return new RelayCommand<String>(
                    (url) =>
                    {
                        Uri uri = new Uri(url);
                        Device.OpenUri(uri);
                    },
                    (url) => {
                        return url != null && url != "";
                    } 
                );
            }
        }

        /// <summary>
        /// Commande qui permet d'ajouter un flux <see cref="Rss"/> à la liste <see cref="RssVM"/>
        /// </summary>
        public RelayCommand AjoutCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        if(RssValid) {
                            if (Ajout)
                            {
                                AjouterRss(SelectedRss);
                                rssVM.Add(SelectedRss);
                            }
                            else
                            {
                                rssVM[selectedRssIndex] = SelectedRss;
                            }
                            UpdateArticle();
                            GoBackModal();
                        }
                    }
                );
            }
        }

        /// <summary>
        /// Commande qui permet de faire un retour à la vue précedente
        /// Reinitialise l'object en cour de modification 
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        SelectedRss.ChannelVM.Lien = oldLien;
                        SelectedRss.ChannelVM.CategorieVM.Nom = oldCategorieNom;
                        SelectedRss.ChannelVM.Title = oldTitle;
                        SelectedRss.ChannelVM.Description = oldDescription;

                        GoBackModal();
                    }
                );
            }
        }

        /// <summary>
        /// Commande permetant de revenir en arrière dans la hiérarchie des vues dans la vue <see cref="DetailArticle"/>
        /// </summary>
        public RelayCommand CancelArticleCommand
        {
            get
            {
                return new RelayCommand(
                    () => 
                    {
                        GoBackModal();
                    }
                );
            }
        }

        /// <summary>
        /// Commande qui permet de supprimer un flux <see cref="Rss"/> de la liste <see cref="RssVM"/>
        /// </summary>
        public RelayCommand<RssVM> DeleteCommand
        {
            get
            {
                return new RelayCommand<RssVM>(
                    (rss) =>
                    {
                        UtilisateurVM.Model.SupprimerRSS(rssVM.IndexOf(rss) + 1);
                        rssVM.Remove(rss);
                        ItemsVM = UtilisateurVM.RecupererArticles();
                    },(rss) => {
                        return rss != null;
                    }
                );
            }
        }

        #endregion

        #region PROPERTIES

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] String property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        RssParserWithLinQ parser = new RssParserWithLinQ();

        public FacadeUtilisateur Model
        {
            get; set;
        }

        public INavigation Navigation { get; set; } = null;
        public ContentPage Parent { get; set; } = null;

        public Boolean RssValid { get; set; } = false;

        /// <summary>
        /// <see cref="SelectedRss"/> est le RssVM qui est utilisé quand l'utilisateur veut ajouter ou modifier un Rss.
        /// </summary>
        RssVM selectedRSS = new RssVM(null);
        public RssVM SelectedRss {
            get { return selectedRSS; }
            set {
                selectedRSS = value;
                oldLien = selectedRSS.ChannelVM.Lien;
                oldTitle = selectedRSS.ChannelVM.Title;
                oldCategorieNom = selectedRSS.ChannelVM.CategorieVM.Nom;
                oldDescription = selectedRSS.ChannelVM.Description;
                OnPropertyChanged();
            }
        }
        
        String oldLien;
        String oldTitle;
        String oldCategorieNom;
        String oldDescription;

        public int SelectedRssIndex
        {
            get { return selectedRssIndex; }
            set
            {
                if (selectedRssIndex != value)
                    selectedRssIndex = value;
                
                OnPropertyChanged();
            }
        }
        private int selectedRssIndex = -1;

        public UtilisateurVM UtilisateurVM
        {
            get; set;
        }

        private ObservableCollection<ItemVM> itemsVM;
        public ObservableCollection<ItemVM> ItemsVM { get { return itemsVM; } set { itemsVM = value; OnPropertyChanged(); }}

        private ObservableCollection<RssVM> rssVM;
        public ObservableCollection<RssVM> RssVM { get { return rssVM; } }

        /// <summary>
        /// Permet de savoir si l'utilisateur modifie ou ajoute un Rss.
        /// </summary>
        public bool Ajout { get; set; }

        #endregion

        public FacadeUtilisateurVM(IDataPersistance persistance)
        {
            Model = new FacadeUtilisateur(persistance);
            UtilisateurVM = new UtilisateurVM(Model.Utilisateur);
            ItemsVM = UtilisateurVM.RecupererArticles();
            rssVM = UtilisateurVM.RssDispo();
            parser.rssChanged += RssChanged;
        }

        #region METHODS

        /// <summary>
        /// Méthode appelée lorsqu'un flux <see cref="Rss"/> a été chargé par le parserRSS.
        /// </summary>
        /// <param name="sender">Objet envoyant l'évenement</param>
        /// <param name="rss"> Le <see cref="Rss"/> qui a été changé</param>
        public void RssChanged(object sender, Rss rss)
        {
            SelectedRss = new RssVM(rss);
            RssValid = true;
        }

        /// <summary>
        /// Permet d'ajouter un flux <see cref="Rss"/> et de récuperer les articles
        /// </summary>
        /// <param name="rss"></param>
        private void AjouterRss(RssVM rss)
        {
            UtilisateurVM.AjouterRss(rss);
            ItemsVM = UtilisateurVM.RecupererArticles();
        }

        /// <summary>
        /// Permet de vider la liste des <see cref="ItemVM"/> pour aller récuperer les nouveaux articles
        /// </summary>
        public void UpdateArticle()
        {
            itemsVM.Clear();
            foreach (ItemVM item in UtilisateurVM.RecupererArticles())
            {
                itemsVM.Add(item);
            }
        }

        /// <summary>
        /// Permet de revenir à la vue précédente de la modal
        /// </summary>
        public void GoBackModal()
        {
            RssValid = false;
            SelectedRss = new RssVM(null);
            if (Navigation != null)
                Navigation.PopModalAsync();
        }

        #endregion
    }
}
