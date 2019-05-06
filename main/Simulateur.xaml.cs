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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AeroportLibrary;

namespace main
{
    /// <summary>
    /// Logique d'interaction pour Simulateur.xaml
    /// </summary>
    public partial class Simulateur : Window
    {
        #region VARIABLES
        private int _duree;
        private int _vitesse;
        private DateTime _debut;
        private DateTime _currenttime;
        private Aeroport _monaeroport;
        private FlightAndAirportManager _manager;
        private ListeVols<VolProgramme> _volprogcol;
        private ListeVols<VolProgramme> _volprogaffiche;
        private Timer _ticker;
        static int nbrtick=0;
        #endregion
        public Simulateur(FlightAndAirportManager mana)
        {
            #region Initialisation
            Manager = mana;
            Debut = new DateTime();
            Currenttime = new DateTime();
            new SelectionSimulateur(this).ShowDialog();
            InitializeComponent();

            Monaeroport = new Aeroport();
            Volprogcol = new ListeVols<VolProgramme>();
            Volprogaffiche = new ListeVols<VolProgramme>();

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
                        Volprogaffiche.Add(v);
                    }
                }
            }
            catch (FileNotFoundException) { }
            #endregion

            Volprogaffiche.Sort();
            mainGrid.DataContext = Volprogaffiche;
            Currenttime = Debut;
            aeroport.Content = Monaeroport.Nomination.ToUpper();
            Date.Content = "DATE : " + Debut.ToLongDateString();
            vitesse.Content = "VITESSE : " + Vitesse.ToString() + "minute par seconde";
            time.Content = Debut.ToShortTimeString();
            Ticker = new Timer();
            Ticker.Interval = 1000;
            Ticker.Tick += Ticker_Tick;
            Ticker.Start();


        }

        private void Ticker_Tick(object sender, EventArgs e)
        {
            nbrtick += 1;
            Currenttime = Currenttime.AddMinutes(Vitesse);
            time.Content = Currenttime.ToShortTimeString();

            List<VolProgramme> tmp = new List<VolProgramme>();

            foreach (VolProgramme v in Volprogaffiche)
            {
                v.setState(Currenttime);
                if (v.Status == VolProgramme.STATE.FLYING)
                    tmp.Add(v);
            }

            //On supprime les vols partis
            //l'utilisation d'une liste tampon est
            //obligatoire : on ne peut pas
            //modifier une liste pendant qu'on la parcourt.
            foreach(VolProgramme v in tmp)
            {
                Volprogaffiche.Remove(v);
            }

            Volprogaffiche.Sort();

            mainGrid.DataContext = null;
            mainGrid.DataContext = Volprogaffiche;

            if ((nbrtick * Vitesse) / 60 >= Duree)
            {
                Console.WriteLine("FIN SIMULATION");
                Ticker.Stop();
            }
        }

        #region PROPRIETE
        public int Duree { get => _duree; set => _duree = value; }
        public int Vitesse { get => _vitesse; set => _vitesse = value; }
        public DateTime Debut { get => _debut; set => _debut = value; }
        public ListeVols<VolProgramme> Volprogcol { get => _volprogcol; set => _volprogcol = value; }
        public ListeVols<VolProgramme> Volprogaffiche { get => _volprogaffiche; set => _volprogaffiche = value; }
        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }
        public Aeroport Monaeroport { get => _monaeroport; set => _monaeroport = value; }
        public Timer Ticker { get => _ticker; set => _ticker = value; }
        public DateTime Currenttime { get => _currenttime; set => _currenttime = value; }
        #endregion
    }
}
