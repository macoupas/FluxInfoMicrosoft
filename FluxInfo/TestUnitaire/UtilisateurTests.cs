using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluxInfo.Metier;
using System.Collections.Generic;

namespace TestUnitaire
{
    [TestClass]
    public class UtilisateurTests
    {
        [TestMethod]
        public void ChangerSelectCategorie()
        {
            Utilisateur user = new Utilisateur();
            Categorie categorie = new Categorie("Test")
            {
                IsSelect = true
            };
            user.DecocherCocherCategorie(categorie);
            Assert.AreEqual(false, categorie.IsSelect);
        }

        [TestMethod]
        public void AjouterItemFavorisTest()
        {
            Utilisateur user = new Utilisateur();
            Item item = new Item()
            {
                Titre = "item test",
                Description = "Item pour le test ajout au favoris",
                Lien = "http://test.fr"
            };

            user.AjouterItemFavoris(item);
            List<Item> items = new List<Item>();
            items.AddRange(user.ArticlesFavoris);
            Assert.IsTrue(items.Contains(item));
        }

        [TestMethod]
        public void SupprimerItemFavorisTest()
        {
            Utilisateur user = new Utilisateur();
            Item item = new Item()
            {
                Titre = "item test",
                Description = "Item pour le test ajout au favoris",
                Lien = "http://test.fr"
            };

            user.AjouterItemFavoris(item);
            List<Item> items = new List<Item>();
            items.AddRange(user.ArticlesFavoris);

            user.SupprimerItemFavoris(item);
            items.Clear();
            items.AddRange(user.ArticlesFavoris);

            Assert.IsTrue(!items.Contains(item));
        }
    }
}
