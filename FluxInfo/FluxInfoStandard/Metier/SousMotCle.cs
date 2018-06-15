using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfo.Metier
{

    public class SousMotCle : MotCle
    {
        public IEnumerable<SousMotCle> SousMotCles
        {
            get
            {
                return sousMotCles.AsReadOnly();
            }
        }

        private List<SousMotCle> sousMotCles = new List<SousMotCle>();

        public SousMotCle(string mot) : base(mot)
        {
        }

        public void AjouterSousMotCle(SousMotCle mot)
        {
            if (!sousMotCles.Contains(mot))
            {
                sousMotCles.Add(mot);
            } else
            {
                throw new Exception("Element déjà présent dans la liste");
            }
        }

        public void SupprimerSousMotCle(int index)
        {
            if(index < 0 || index >= sousMotCles.Count)
            {
                throw new IndexOutOfRangeException("Index incorrect");
            } else
            {
                if (!sousMotCles.Remove(sousMotCles[index]))
                {
                    throw new Exception("Element inextistant");
                }
            }
        }
    }
}
