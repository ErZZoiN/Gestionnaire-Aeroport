using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class Aeroport
    {
        #region VARIABLE
        private string _code;
        private string _ville;
        private Pays _pays;
        #endregion

        #region PROPRIETE
        public string Code { get => _code; set => _code = value; }
        public string Ville { get => _ville; set => _ville = value; }
        public Pays Pays { get => _pays; set => _pays = value; }
        public string Nomination { get => Code + " " + Ville; }
        #endregion

        public Aeroport(string code, string ville, Pays pays)
        {
            Code = code;
            Ville = ville;
            Pays = pays;
        }

        public Aeroport()
        {
            Code = "";
            Ville = "";
        }

        public static Aeroport BRUXELLES = new Aeroport("BRU", "Bruxelles", Pays.BELGIQUE);
        public static Aeroport CHARLEROI = new Aeroport("CRL", "Charleroi", Pays.BELGIQUE);
        public static Aeroport KENNEDY = new Aeroport("JFK", "New York", Pays.ETATSUNIS);
        public static Aeroport BERLIN = new Aeroport("BER", "Berlin", Pays.ALLEMAGNE);
        public static Aeroport TANGER = new Aeroport("TNG", "Tanger", Pays.MAROC);
        public static Aeroport MARRAKESH = new Aeroport("RAK", "Marrakesh", Pays.MAROC);
        public static Aeroport ORLY = new Aeroport("ORY", "Paris", Pays.FRANCE);
        public static Aeroport MARSEILLE = new Aeroport("MRS", "Marseille", Pays.FRANCE);
        public static Aeroport MADRID = new Aeroport("MAD", "Madrid", Pays.ESPAGNE);
        public static Aeroport BARCELONE = new Aeroport("BCN", "Barcelone", Pays.ESPAGNE);
        public static Aeroport[] LISTEAEROPORT = { BRUXELLES, CHARLEROI, KENNEDY, BERLIN, TANGER, MARRAKESH, ORLY, MARSEILLE, MADRID, BARCELONE };
    }
}
