using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class VolGenerique
    {
        #region VARIABLE
        private int _numero;
        private CompagnieAerienne _compagnie;
        private Aeroport _aeroportdepart;
        private Aeroport _aeroportarrivee;
        private TimeSpan _heuredepart;
        private TimeSpan _heurearrivee; 
        #endregion

        #region PROPRIETE
        public TimeSpan HeureArrivee { get => _heurearrivee; set => _heurearrivee = value; }
        public TimeSpan HeureDepart { get => _heuredepart; set => _heuredepart = value; }
        public Aeroport AeroportArrivee { get => _aeroportarrivee; set => _aeroportarrivee = value; }
        public Aeroport AeroportDepart { get => _aeroportdepart; set => _aeroportdepart = value; }
        public CompagnieAerienne Compagnie { get => _compagnie; set => _compagnie = value; }
        public int Numero { get => _numero; set => _numero = value; }
        public string Identifiant { get => Compagnie.Code.ToString() + Numero.ToString(); }
        public TimeSpan Duree { get => HeureArrivee - HeureDepart; }
        #endregion
    }
}
