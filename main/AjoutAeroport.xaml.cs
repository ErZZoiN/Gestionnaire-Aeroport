using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AeroportLibrary;

namespace main
{
    /// <summary>
    /// Logique d'interaction pour AjoutAeroport.xaml
    /// </summary>
    public partial class AjoutAeroport : Window
    {
        private ObservableSortableSerializableList<Aeroport> _listeaeroport;
        private FlightAndAirportManager _manager;

        public ObservableSortableSerializableList<Aeroport> Listeaeroport { get => _listeaeroport; set => _listeaeroport = value; }
        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }

        public AjoutAeroport(FlightAndAirportManager m)
        {
            Manager = m;
            Listeaeroport = new ObservableSortableSerializableList<Aeroport>();
            try
            {
                Listeaeroport.LoadFromXML(Manager.Datapath + "/" + "listeAeroport.xml");
            }
            catch (FileNotFoundException ex)
            {
                foreach (Aeroport a in Aeroport.LISTEAEROPORT)
                    Listeaeroport.Add(a);
            }
            InitializeComponent();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Aeroport tmp = new Aeroport()
            {
                Code = code.Text,
                Ville = nom.Text,
                PaysLocal = new Pays(pays.Text, "", Int32.Parse(horaire.Text))
            };
            Listeaeroport.Add(tmp);
            Listeaeroport.SaveInXML(Manager.Datapath + "/" + "listeAeroport.xml");
            Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
