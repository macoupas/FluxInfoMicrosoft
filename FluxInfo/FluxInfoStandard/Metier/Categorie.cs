using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FluxInfo.Metier
{
    /// <summary>
    /// Catégorie permet d'identifier le type d'un RSS.
    /// Tout les articles du flux RSS <see cref="Rss"/> auront comme Categorie celle du flux.
    /// Cela permettra de pouvoir trier les Item <see cref="Item"/> par Categorie ou afficher
    /// les articles en fonction des Categorie sélectionnée par l'Utilisateur <see cref="Utilisateur"/>
    /// </summary>
    [DataContract]
    public class Categorie
    {
        [DataMember]
        public string Nom { get; internal set; }

        [DataMember]
        public bool IsSelect { get; internal set; } = true;
        
        public Categorie(string nom) => Nom = nom;

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals(obj as Categorie);
        }

        private bool Equals(Categorie other)
        {
            return (this.Nom.Equals(other.Nom));
        }

        public override int GetHashCode()
        {
            var hashCode = -1900909542;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nom);
            hashCode = hashCode * -1521134295 + IsSelect.GetHashCode();
            return hashCode;
        }
    }
}
