using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;

namespace AeroportLibrary
{
    public class ObservableSortableSerializableList<T> : ObservableCollection<T>, IPersistent
    {
        public void LoadFromXML(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlFormat = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            Clear();
            using (Stream fStream = File.OpenRead(path))
            {
                ((List<T>)xmlFormat.Deserialize(fStream)).ForEach(item => Add(item));
            }
        }

        public void SaveInXML(string path)
        {
            System.Xml.Serialization.XmlSerializer xmlformat = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlformat.Serialize(fStream, this.ToList());
            }
        }

        public void SaveInCSV(string path)
        {
            using (Stream fStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fStream.WriteCsv(this);
            }
        }

        public void LoadFromCSV(string filename)
        {
            string tmp;
            Clear();
            using (Stream fStream = File.OpenRead(filename))
            {
                StreamReader sr = new StreamReader(fStream);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    tmp = sr.ReadLine();
                    Console.WriteLine(tmp);
                    Add(CsvSerializer.DeserializeFromString<T>(tmp));
                }
            }
        }

        public void Sort()
        {
            var listmp = new List<T>(this);
            listmp.Sort();
            Clear();

            foreach (T v in listmp)
                Add(v);
        }
    }
}
