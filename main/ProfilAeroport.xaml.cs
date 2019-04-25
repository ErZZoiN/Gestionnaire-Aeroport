using AeroportLibrary;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

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
        private ObservableCollection<VolProgramme> _volprogconcerne;
        #endregion
        public ProfilAeroport(FlightAndAirportManager m)
        {
            Manager = m;
            Volprogcol = new ListeVols<VolProgramme>();
            Volprogaffiche = new ObservableCollection<VolProgramme>();
            Volprogconcerne = new ObservableCollection<VolProgramme>();
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
                Volprogcol.Sort();
                foreach (VolProgramme v in Volprogcol)
                {
                    if (v.Vol.AeroportDepart.Code == Monaeroport.Code)
                    {
                        Volprogconcerne.Add(v);
                    }
                }

                foreach (VolProgramme v in Volprogconcerne)
                {
                    Volprogcol.Remove(v);
                    Volprogaffiche.Add(v);
                }

            }
            catch (FileNotFoundException) { }

            Closed += ProfilAeroport_Closed;
        }

        private void ProfilAeroport_Closed(object sender, System.EventArgs e)
        {
            Monaeroport.Save(Manager.Datapath + "\\" + Monaeroport.Code + "Compagnie.xml");
            foreach (VolProgramme v in Volprogaffiche)
            {
                Volprogcol.Add(v);
            }
            Volprogcol.Save(Manager.Datapath + "\\" + "Volprog.xml");
        }

        #region PROPRIETE
        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }
        public ListeVols<VolProgramme> Volprogcol { get => _volprogcol; set => _volprogcol = value; }
        public ObservableCollection<VolProgramme> Volprogaffiche { get => _volprogaffiche; set => _volprogaffiche = value; }
        public Aeroport Monaeroport { get => _monaeroport; set => _monaeroport = value; }
        public ObservableCollection<VolProgramme> Volprogconcerne { get => _volprogconcerne; set => _volprogconcerne = value; }
        #endregion

        //TODO : réparer ce code de marde
        private void Retarder_Click(object sender, RoutedEventArgs e)
        {
            if (volProgramme.SelectedItem != null)
            {
                foreach (VolProgramme v in volProgramme.SelectedItems.Cast<VolProgramme>().ToList())
                {
                    v.Retard += 5;
                }
            }
        }
        private void MenuNouveauLog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nl = new MainWindow(Monaeroport);
            nl.Show();
        }

        private void MenuDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nl = new MainWindow();
            nl.Show();
            Close();
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutMe(Monaeroport).ShowDialog();
        }

        private void MenuOption_Click(object sender, RoutedEventArgs e)
        {
            var tmp = new Options(Manager);
            tmp.Show();

            tmp.Valider += Tmp_Valider;
        }

        private void Tmp_Valider(FlightAndAirportManager m)
        {
            Manager = m;
        }

        //On modifie l'affichage
        private void DateVols_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Volprogaffiche.Clear();
            foreach (VolProgramme v in Volprogconcerne)
                if (v.DateDepart.Equals(dateVols.SelectedDate.Value))
                    Volprogaffiche.Add(v);
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            if (volProgramme.SelectedItem != null)
            {
                foreach (VolProgramme v in volProgramme.SelectedItems.Cast<VolProgramme>().ToList())
                {
                    Volprogaffiche.Remove(v);
                }
            }
        }

        private void Simulateur_Click(object sender, RoutedEventArgs e)
        {
            var tmp = new Simulateur(Manager);
            tmp.ShowDialog();
        }
    }
}
