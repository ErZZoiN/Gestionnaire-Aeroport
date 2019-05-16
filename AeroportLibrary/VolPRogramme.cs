using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AeroportLibrary
{
    public enum STATE { SCHEDULED, BOARDING, LASTCALL, GATE_CLOSED, AIRBORNE, FLYING };

    static class StateMethods
    {

        public static String ToString(this STATE s1)
        {
            switch (s1)
            {
                case STATE.SCHEDULED:
                    return "SCHEDULED";
                case STATE.BOARDING:
                    return "BOARDING";
                case STATE.FLYING:
                    return "FLYING";
                case STATE.GATE_CLOSED:
                    return "GATE_CLOSED";
                case STATE.LASTCALL:
                    return "LASTCALL";
                case STATE.AIRBORNE:
                    return "AIRBORNE";
                default:
                    return "What?!";
            }
        }
    }

    public class VolProgramme : IComparable<VolProgramme>, INotifyPropertyChanged
    {

        #region VARIABLE
        private VolGenerique _vol;
        private DateTime _datedepart;
        private int _nombrepassager;
        private int _retard;
        private STATE _status;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region PROPRIETE
        public VolGenerique Vol { get => _vol; set => _vol = value; }
        public DateTime DateDepart { get => _datedepart; set => _datedepart = value; }
        public int NombrePassager { get => _nombrepassager; set => _nombrepassager = value; }
        public DateTime DateArrivee { get => DateDepart.Add(Vol.Duree); }
        public int Retard { get => _retard; set { _retard = value; OnPropertyChanged("retard"); } }
        public STATE Status { get => _status; }
        private STATE setStatus { set { _status = value; OnPropertyChanged("status");} }

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
            DateDepart = date.Add(vol.HeureDepart);
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
            if (DateDepart.CompareTo(v.DateDepart) == 0)
            {
                return Vol.HeureDepart.CompareTo(v.Vol.HeureDepart);
            }
            else return DateDepart.CompareTo(v.DateDepart);
        }

        public void setState(DateTime tempsCourant)
        {
            long diffTicks = DateDepart.Ticks - tempsCourant.Ticks;
            TimeSpan diff = new TimeSpan(diffTicks);
            if (diff.TotalMinutes < -5)
                setStatus = STATE.FLYING;
            else if (diff.TotalMinutes < 0)
                setStatus = STATE.AIRBORNE;
            else if (diff.TotalMinutes < 5)
                setStatus = STATE.GATE_CLOSED;
            else if (diff.TotalMinutes < 10)
                setStatus = STATE.LASTCALL;
            else if (diff.TotalMinutes < 30)
                setStatus = STATE.BOARDING;
            else
                setStatus = STATE.SCHEDULED;

        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
