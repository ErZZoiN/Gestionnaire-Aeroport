using AeroportLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace main
{
    /// <summary>
    /// Logique d'interaction pour ProfilAeroport.xaml
    /// </summary>
    public partial class ProfilAeroport : Window
    {
        #region VARIABLES
        private Aeroport _monaeroport;
        private FlightAndAirportManager _manager;
        private ListeVols<VolProgramme> _volprogcol;
        private ObservableCollection<VolProgramme> _volprogaffiche; 
        #endregion
        public ProfilAeroport(FlightAndAirportManager m)
        {
            Manager = m;
            Volprogcol = new ListeVols<VolProgramme>();
            Volprogaffiche = new ObservableCollection<VolProgramme>();
            Monaeroport = new Aeroport();
            InitializeComponent();
            volProgramme.DataContext = Volprogaffiche;

            try
            {
                Monaeroport.Load(Manager.Datapath + "\\" + Manager.Code + "Aeroport.xml");
            }
            catch (FileNotFoundException)
            {
                Monaeroport.Code = Manager.Code;
            }

            try
            {
                Volprogcol.Load(Manager.Datapath + "\\" + "Volprog.xml");
                foreach (VolProgramme v in Volprogcol)
                {
                    if (v.Vol.AeroportArrivee.Code == Monaeroport.Code)
                    {
                        Volprogaffiche.Add(v);
                    }
                }

                foreach (VolProgramme v in Volprogaffiche)
                {
                    Volprogcol.Remove(v);
                }

            }
            catch (FileNotFoundException) { }
        }

        #region PROPRIETE
        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }
        public ListeVols<VolProgramme> Volprogcol { get => _volprogcol; set => _volprogcol = value; }
        public ObservableCollection<VolProgramme> Volprogaffiche { get => _volprogaffiche; set => _volprogaffiche = value; }
        public Aeroport Monaeroport { get => _monaeroport; set => _monaeroport = value; } 
        #endregion
    }
}
