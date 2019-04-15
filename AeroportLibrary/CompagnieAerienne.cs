using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AeroportLibrary
{
    public class CompagnieAerienne : Profil
    {
        #region VARIABLES
        private string _nom;
        private string _localisation;
        private string _image;
        #endregion

        #region PROPRIETE
        public string Nom { get => _nom; set => _nom = value; }
        public string Localisation { get => _localisation; set => _localisation = value; }
        public string Image { get => _image; set => _image = value; }

        public CompagnieAerienne()
        {
            Code = "";
            Nom = "";
            Localisation = "";
            Image = "";
        }

        public CompagnieAerienne(string c)
        {
            Code = c;
            Nom = "";
            Localisation = "";
            Image = "";
        }

        public override void Load(string path)
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

        public override void Save(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(CompagnieAerienne));
            Console.WriteLine(path);
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this);
            }
        }
        #endregion
    }
}
