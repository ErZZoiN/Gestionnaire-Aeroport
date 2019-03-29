using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AeroportLibrary
{
    public class CompagnieAerienne
    {
        #region VARIABLES
        private string _nom;
        private string _localisation;
        private string _image;
        private string _code;
        #endregion

        #region PROPRIETE
        public string Nom { get => _nom; set => _nom = value; }
        public string Localisation { get => _localisation; set => _localisation = value; }
        public string Image { get => _image; set => _image = value; }
        public string Code { get => _code; set => _code = value; }
        #endregion
    }
}
