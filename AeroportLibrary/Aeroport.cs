using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class Aeroport
    {
        #region VARIABLE
        private char[] _code = new char[3];
        private string _ville;
        private Pays _pays;
        #endregion

        #region PROPRIETE
        public char[] Code { get => _code; set => _code = value; }
        public string Ville { get => _ville; set => _ville = value; }
        public Pays Pays { get => _pays; set => _pays = value; } 
        #endregion

        public Aeroport(string code, string ville, Pays pays)
        {
            Code = code.ToCharArray();
            Ville = ville;
            Pays = pays;
        }
    }
}
