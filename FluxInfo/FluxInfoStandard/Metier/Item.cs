using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("FluxInfoUWP")]
namespace FluxInfo.Metier
{
    [DataContract]
    public class Item : IEquatable<Item>      
    {
        public const string XML_ITEM_TITLE = "title";
        public const string XML_ITEM_DESCRIPTION = "description";
        public const string XML_ITEM_LINK = "link";

        [DataMember]
        public string Titre { get; internal set; }

        [DataMember]
        public string Description { get; internal set; }

        [DataMember]
        public string Lien { get; internal set; }

        [DataMember]
        public string LienImage { get; internal set; }

        [DataMember]
        public DateTime Date { get; internal set; }

        [DataMember]
        public bool EstDansFav { get; set; } = false;

        [DataMember]
        private List<MotCle> motsCles = new List<MotCle>();
        public IEnumerable<MotCle> MotCles
        {
            get
            {
                return motsCles.AsReadOnly();
            }
        }

        /// <summary>
        /// Permet d'ajouter un mot clé à la liste des mots-clés.
        /// </summary>
        /// <param name="motCle">MotCle <see cref="MotCle"/> à ajouter.</param>
        public void AjouterMotCle(MotCle motCle)
        {
            if (motsCles.Contains(motCle))
            {
                throw new Exception("Mot cle existant");
            } else
            {
                motsCles.Add(motCle);
            }
        }

        public void SupprimerMotCle(MotCle motCle)
        {
            if (!motsCles.Remove(motCle))
            {
                throw new Exception("Mot inexistant");
            }
        }

        public override bool Equals(object obj)
        {
            if(object.ReferenceEquals(obj, null))
            {
                return false;
            }

            if(object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if(this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals(obj as Item);
        }

        public bool Equals(Item other)
        {
            return (this.Lien.Equals(other.Lien));
        }

        public override int GetHashCode()
        {
            var hashCode = 1919742335;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Titre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lien);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LienImage);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EstDansFav.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<MotCle>>.Default.GetHashCode(motsCles);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEnumerable<MotCle>>.Default.GetHashCode(MotCles);
            return hashCode;
        }
    }
}
