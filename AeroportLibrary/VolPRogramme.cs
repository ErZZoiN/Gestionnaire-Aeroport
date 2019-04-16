using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class VolProgramme : IComparable<VolProgramme>
    {
        #region VARIABLE
        private VolGenerique _vol;
        private DateTime _datedepart;
        private int _nombrepassager;
        private int _retard;
        #endregion

        #region PROPRIETE
        public VolGenerique Vol { get => _vol; set => _vol = value; }
        public DateTime DateDepart { get => _datedepart.Add(Vol.HeureDepart); set => _datedepart = value; }
        public int NombrePassager { get => _nombrepassager; set => _nombrepassager = value; }
        public DateTime DateArrivee { get => DateDepart.Add(Vol.Duree); }
        public int Retard { get => _retard; set => _retard = value; }

        public bool IsRetarded { get
            {
                if (Retard == 0)
                    return false;
                return true;
            } }
        #endregion

        public VolProgramme()
        {
            Vol = null;
            DateDepart = new DateTime();
            NombrePassager = 0;
            Retard = 0;
        }

        public VolProgramme(VolGenerique vol, DateTime date)
        {
            Vol = vol;
            DateDepart = date;
            NombrePassager = 0;
            Retard = 0;
        }

        #region METHODES
        public void AddPassenger()
        {
            NombrePassager++;
        }

        public void AddPassenger(int n)
        {
            NombrePassager += n;
        }
        public int CompareTo(VolProgramme v)
        {
            return Vol.HeureDepart.CompareTo(v.Vol.HeureDepart);
        }
        #endregion
    }
}
