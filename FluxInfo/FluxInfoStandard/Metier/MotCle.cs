using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfo.Metier
{
    [DataContract]
    public abstract class MotCle
    {
        [DataMember]
        public string Mot { get; set; }
        
        public MotCle(String mot) => Mot = mot;

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

            return this.Equals(obj as MotCle);
        }

        public bool Equals(MotCle other)
        {
            return (this.Mot.Equals(other.Mot));
        }

        public override int GetHashCode()
        {
            return 666244843 + EqualityComparer<string>.Default.GetHashCode(Mot);
        }
    }
}
