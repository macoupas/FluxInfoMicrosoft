using FluxInfo.Metier;
using FluxInfo.persistance;
using FluxInfoUWP.Command;
using FluxInfoUWP.View;
using RssParser;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluxInfoUWP.ViewModels
{
    /// <summary>
    /// La classe <see cref="FacadeUtilisateurVM" /> permet de faire le lien entre les vues
    /// et le model <see cref="FacadeUtilisateur"/>.
    /// </summary>
    class FacadeUtilisateurVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Représente la méthode qui sera appelée quand l'évenement <see cref="RssChangedEvent"/> sera lancé.
        /// </summary>
        public delegate void RssChanged();
        public event RssChanged RssChangedEvent;

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
                    async (lien) =>
                    {
                        RssParserWithLinQ parser = new RssParserWithLinQ();
                        try
                        {
                            Rss rss = parser.ParserRSS(lien);
                            TmpRssVM = new RssVM(rss);
                            RssChangedEvent?.Invoke();
                            RssValid = true;
                        }
                        catch (Exception e)
                        {
                            var dialog = new MessageDialog("Url invalide");
                            await dialog.ShowAsync();
                            RssValid = false;
                        }
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
                                AjouterRss(TmpRssVM);
                                rssVM.Add(TmpRssVM);
                            }
                            else
                            {
                                rssVM[selectedRssIndex] = TmpRssVM;
                            }
                            UpdateArticle();
                            GoBack();
                        }
                        else
                        {
                            var dialog = new MessageDialog("Flux invalide ou incomplet");
                            await dialog.ShowAsync();
                        }
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
                        UtilisateurVM.Model.SupprimerRSS(rssVM.IndexOf(SelectedRss) + 1);
                        rssVM.Remove(rss);
                        itemVM = UtilisateurVM.RecupererArticles();
                        GoBack();
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

        public bool RssValid { get; private set; }

        /// <summary>
        /// <see cref="TmpRssVM"/> est le RssVM qui est utilisé quand l'utilisateur veut ajouter ou modifier un Rss.
        /// </summary>
        public RssVM TmpRssVM { get; set; } = new RssVM(null);

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
                return itemVM[SelectedArticleIndex];
            }
        }

        public int SelectedRssIndex
        {
            get { return selectedRssIndex; }
            set
            {
                selectedRssIndex = value;
                OnPropertyChanged();
                RssChangedEvent?.Invoke();
            }
        }
        private int selectedRssIndex = -1;

        public RssVM SelectedRss
        {
            get
            {
                if (selectedRssIndex < 0)
                {
                    return TmpRssVM;
                }
                return rssVM[SelectedRssIndex];
            }
        }

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

        private ObservableCollection<ItemVM> itemVM;
        public ObservableCollection<ItemVM> ItemsVM { get { return itemVM; } }

        private ObservableCollection<RssVM> rssVM;
        public ObservableCollection<RssVM> RssVM { get { return rssVM; } }

        /// <summary>
        /// Permet de savoir si l'utilisateur modifie ou ajoute un Rss.
        /// </summary>
        public bool Ajout { get; private set; }

        #endregion

        public FacadeUtilisateurVM(IDataPersistance persistance)
        {
            Model = new FacadeUtilisateur(persistance);
            UtilisateurVM = new UtilisateurVM(Model.Utilisateur);
            itemVM = UtilisateurVM.RecupererArticles();
            rssVM = UtilisateurVM.RssDispo();
        }

        #region METHODS

        private void AjouterRss(RssVM rss)
        {
            UtilisateurVM.AjouterRss(rss);
            itemVM = UtilisateurVM.RecupererArticles();
        }

        private void UpdateArticle()
        {
            itemVM.Clear();
            foreach (ItemVM item in UtilisateurVM.RecupererArticles())
            {
                itemVM.Add(item);
            }
        }

        public void GoToDetailArticle()
        {
            ((Frame)Window.Current.Content).Navigate(typeof(DetailArticle));
        }

        public void GoToAjoutRSS()
        {
            Ajout = true;
            selectedRssIndex = -1;
            ((Frame)Window.Current.Content).Navigate(typeof(AjoutRSS));
        }

        public void GoToModifRSS()
        {
            Ajout = false;
            ((Frame)Window.Current.Content).Navigate(typeof(AjoutRSS));
        }

        public void GoBack()
        {
            TmpRssVM = new RssVM(null);
            ((Frame)Window.Current.Content).GoBack();
        }

        #endregion
    }
}
