using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AeroportLibrary
{
    public class CompagnieAerienne : IPersistent
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


        // !!!!! NE FONCTIONNE PAS !!!!!!
        public void Load(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(CompagnieAerienne));
            using (Stream fStream = File.OpenRead(path))
            {
                CompagnieAerienne tmp = (CompagnieAerienne)xmlFormat.Deserialize(fStream);
                Nom = tmp.Nom;
                Localisation = tmp.Localisation;
                Image = tmp.Image;
                Code = tmp.Code;
            }
        }

        public void Save(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(CompagnieAerienne));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this);
            }
        }
        #endregion
    }
}
