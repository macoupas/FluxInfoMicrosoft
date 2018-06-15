using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace FluxInfo.Metier
{
    [DataContract]
    public class Rss
    {
        [DataMember]
        public String Version { get; internal set; }

        [DataMember]
        public Channel Channel { get; internal set; }

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

            return this.Equals(obj as Rss);
        }

        public override int GetHashCode()
        {
            var hashCode = 23261971;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Version);
            hashCode = hashCode * -1521134295 + EqualityComparer<Channel>.Default.GetHashCode(Channel);
            return hashCode;
        }

        private bool Equals(Rss other)
        {
            return (this.Channel.Equals(other.Channel));
        }
    }
}
