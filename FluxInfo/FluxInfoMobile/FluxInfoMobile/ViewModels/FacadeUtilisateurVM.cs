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
        void HandleFunc(string arg)
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Représente la méthode qui sera appelée quand l'évenement <see cref="RssChangedEvent"/> sera lancé.
        /// </summary>

        RssParserWithLinQ parser = new RssParserWithLinQ();

        protected void OnPropertyChanged([CallerMemberName] String property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #region COMMANDS


        /// <summary>
        /// Commande qui permet de parser un flux <see cref="Rss"/> donné par un lien.
        /// </summary>
        public RelayCommand<string> ParserCommand
        {
            get
            {
                return new RelayCommand<string>(
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
                    async () =>
                    {
                        if (RssValid)
                        {
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
                        else
                        {
                            if (Parent != null)
                                await Parent.DisplayAlert("Erreur", "Flux invalide ou incomplet", "OK");
                        }
                    },
                    () => {
                        return true;
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

        public FacadeUtilisateur Model
        {
            get; set;
        }
        public INavigation Navigation { get; set; } = null;
        public ContentPage Parent { get; set; } = null;

        public bool RssValid { get; set; }

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

        public int SelectedArticleIndex
        {
            get { return selectedArticleIndex; }
            set
            {
                selectedArticleIndex = value;
                OnPropertyChanged();
            }
        }
        private int selectedArticleIndex = -1;

        public ItemVM SelectedItem
        {
            get
            {
                if (selectedArticleIndex < 0)
                {
                    return null;
                }
                return itemsVM[SelectedArticleIndex];
            }
        }

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

        /// <summary>
        /// Permet de récupérer l'image d'une étoile pleine ou vide si l'article est dans les
        /// favoris ou pas.
        /// </summary>
        public string ImageFav
        {
            get
            {
                if (SelectedItem.EstDansFavoris)
                {
                    return "ms-appx:///Assets/star.png";
                }
                else
                {
                    return "ms-appx:///Assets/starvide.png";
                }
            }
        }

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

        public void RssChanged(object sender, Rss rss)
        {
            SelectedRss = new RssVM(rss);
            RssValid = true;
        }

        private void AjouterRss(RssVM rss)
        {
            UtilisateurVM.AjouterRss(rss);
            ItemsVM = UtilisateurVM.RecupererArticles();
        }

        public void UpdateArticle()
        {
            itemsVM.Clear();
            foreach (ItemVM item in UtilisateurVM.RecupererArticles())
            {
                itemsVM.Add(item);
            }
        }

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
