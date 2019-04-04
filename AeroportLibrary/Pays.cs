using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class Pays
    {
        private string _nom;
        private char[] _code = new char[3];
        private int _fuseauGMT;

        public string Nom { get => _nom; set => _nom = value; }
        public char[] Code { get => _code; set => _code = value; }
        public int FuseauGMT { get => _fuseauGMT; set => _fuseauGMT = value; }

        public Pays(string nom, string code, int fuseau)
        {
            Nom = nom;
            Code = code.ToCharArray();
            FuseauGMT = fuseau;
        }

        public static Pays BELGIQUE = new Pays("Belgique", "BEL", 2);
        public static Pays ALLEMAGNE = new Pays("Allemagne", "GER", 2);
        public static Pays ETATSUNIS = new Pays("Etats-unis", "USA", -4);
        public static Pays ESPAGNE = new Pays("Espagne", "ESP", 2);
        public static Pays MAROC = new Pays("Maroc", "MAR", 1);
        public static Pays FRANCE = new Pays("France", "FRA", 2);
    }
}
