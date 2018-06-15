using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FluxInfo.Metier
{
    /// <summary>
    /// Channel est le composant pricipal d'un Rss <see cref="Rss"/>.
    /// Le Channel est composé d'une liste d'Item <see cref="Item"/>,
    /// d'un Titre, d'une Description, d'un Lien et d'une Categorie <see cref="Categorie"/>.
    /// </summary>
    [DataContract]
    public class Channel
    {
        public const string XML_CHANNEL_TITLE = "title";
        public const string XML_CHANNEL_DESCRIPTION = "description";
        public const string XML_CHANNEL_LINK = "link";
        public const string XML_CHANNEL_ITEM = "item";

        [DataMember]
        public string Title { get; internal set; } = "";

        [DataMember]
        public string Description { get; internal set; } = "";

        [DataMember]
        public string Lien { get; internal set; } = "";

        [DataMember]
        public Categorie Categorie { get; internal set; } = new Categorie("Aucune catégorie");

        [DataMember]
        public bool IsSelect { get; } = true;

        public IEnumerable<Item> Items {
            get
            {
                return items;
            }
        }
        [DataMember]
        private List<Item> items = new List<Item>();

        public void AjouterItems(List<Item> items)
        {
            this.items.AddRange(items);
        }

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

            return this.Equals(obj as Channel);
        }

        private bool Equals(Channel other)
        {
            return (this.Lien.Equals(other.Lien));
        }

        public override int GetHashCode()
        {
            var hashCode = 1836590185;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lien);
            hashCode = hashCode * -1521134295 + EqualityComparer<Categorie>.Default.GetHashCode(Categorie);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<Item>>.Default.GetHashCode(Items);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Item>>.Default.GetHashCode(items);
            return hashCode;
        }
    }
}
