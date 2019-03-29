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
        private string _nomination;
        private Pays _pays;
        #endregion

        #region PROPRIETE
        public string Code { get => _code; set => _code = value; }
        public string Ville { get => _ville; set => _ville = value; }
        public Pays Pays { get => _pays; set => _pays = value; }
        public string Nomination { get => _nomination; set => _nomination = value; }
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
        public static Aeroport[] LISTEAEROPORT = { BRUXELLES, CHARLEROI, KENNEDY };
    }
}
