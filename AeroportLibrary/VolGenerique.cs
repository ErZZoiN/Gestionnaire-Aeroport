using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace AeroportLibrary
{
    public class VolGenerique : IComparable<VolGenerique>, INotifyPropertyChanged
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

        //Les timespan ne se sérialisent pas correctement.
        [XmlIgnore]
        public TimeSpan HeureArrivee { get => _heurearrivee; set => _heurearrivee = value; }

        [XmlIgnore]
        public TimeSpan HeureDepart { get => _heuredepart; set { _heuredepart = value; OnPropertyChanged("HeureDepart"); } }

        public Aeroport AeroportArrivee { get => _aeroportarrivee; set => _aeroportarrivee = value; }
        public Aeroport AeroportDepart { get => _aeroportdepart; set => _aeroportdepart = value; }
        public CompagnieAerienne Compagnie { get => _compagnie; set => _compagnie = value; }
        public int Numero { get => _numero; set { _numero = value; OnPropertyChanged("Numero"); } }
        public string Identifiant { get => Compagnie.Code.ToString() + Numero.ToString();
            set
            {
                Numero = Int32.Parse(value.Substring(2));
            }
        }
        public TimeSpan Duree { get
                { TimeSpan tmp = new TimeSpan((AeroportDepart.PaysLocal.FuseauGMT - AeroportArrivee.PaysLocal.FuseauGMT), 0, 0);
                if (HeureDepart < HeureArrivee)
                    return (HeureArrivee - HeureDepart) - tmp;
                else
                    return (HeureArrivee - HeureDepart) - tmp + (new TimeSpan(24, 0, 0));
            }
        }

        public bool IsNextDay {
            get{
                if (HeureDepart > HeureArrivee)
                    return true;
                return false;
            } }

        public string AffichageArrivee
        {
            get
            {
                if(IsNextDay)
                    return HeureArrivee.ToString() + "+1";
                return HeureArrivee.ToString();
            }
        }

        //On créé des propriétés factices pour sérialiser les timeSpan via des longs
        [XmlElement("HeureArrivee")]
        public long HeureArriveeTicks
        {
            get { return HeureArrivee.Ticks; }
            set { HeureArrivee = new TimeSpan(value); }
        }

        [XmlElement("HeureDepart")]
        public long HeureDepartTicks
        {
            get { return HeureDepart.Ticks; }
            set { HeureDepart = new TimeSpan(value); }
        }

        #endregion
        public int CompareTo(VolGenerique v)
        {
            return HeureDepart.CompareTo(v.HeureDepart);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
