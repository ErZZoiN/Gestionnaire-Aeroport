using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class Aeroport : Profil, IPersistent
    {
        #region VARIABLE
        private string _ville;
        private Pays _pays;
        #endregion

        #region PROPRIETE
        public string Ville { get => _ville; set => _ville = value; }
        public Pays PaysLocal { get => _pays; set => _pays = value; }
        public string Nomination { get => Code + " " + Ville; }
        #endregion

        public Aeroport(string code, string ville, Pays pays)
        {
            Code = code;
            Ville = ville;
            PaysLocal = pays;
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

        public override void LoadFromXML(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(Aeroport));
            using (Stream fStream = File.OpenRead(path))
            {
                Aeroport tmp = (Aeroport)xmlFormat.Deserialize(fStream);
                Ville = tmp.Ville;
                PaysLocal = tmp.PaysLocal;
                Code = tmp.Code;
            }
        }

        public override void SaveInXML(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(Aeroport));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this);
            }
        }
    }
}
