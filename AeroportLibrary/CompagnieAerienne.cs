using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class CompagnieAerienne
    {
        #region VARIABLES
        private string _nom;
        private string _localisation;
        private string _image;
        private char[2] _code;
        #endregion

        #region PROPRIETE
        public string Nom { get => _nom; set => _nom = value; }
        public string Localisation { get => _localisation; set => _localisation = value; }
        public string Image { get => _image; set => _image = value; }
        public char[] Code { get => _code; set => _code = value; }
        #endregion
    }
}
