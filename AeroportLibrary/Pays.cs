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

        public string Nom { get => _nom; set => _nom = value; }
        public char[] Code { get => _code; set => _code = value; }

        public Pays(string nom, string code)
        {
            Nom = nom;
            Code = code.ToCharArray();
        }

        public static Pays BELGIQUE = new Pays("Belgique", "BEL");
        public static Pays ALLEMAGNE = new Pays("Allemagne", "GER");
        public static Pays ETATSUNIS = new Pays("Etats-unis", "USA");
        public static Pays ESPAGNE = new Pays("Espagne", "ESP");
        public static Pays MAROC = new Pays("Maroc", "MAR");
        public static Pays FRANCE = new Pays("France", "FRA");
    }
}
