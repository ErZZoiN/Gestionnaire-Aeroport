using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public class ListeVolGenerique : ObservableCollection<VolGenerique>, IPersistent
    {
        public void Load(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(List<VolGenerique>));
            Clear();
            using (Stream fStream = File.OpenRead(path))
            {
                ((List<VolGenerique>)xmlFormat.Deserialize(fStream)).ForEach(item => Add(item));
            }
        }

        public void Save(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(List<VolGenerique>));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this.ToList());
            }
        }

        public void Sort()
        {
            var listmp = new List<VolGenerique>(this);
            listmp.Sort();
            Clear();

            foreach (VolGenerique v in listmp)
                Add(v);
        }
    }
}
