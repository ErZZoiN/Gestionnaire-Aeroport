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
    /// Interaction logic for ProfilCompagnieAerienne.xaml
    /// </summary>
    public partial class ProfilCompagnieAerienne : Window
    {
        private CompagnieAerienne _compagnie;
        private ListeVolGenerique _volgencol;
        private ObservableCollection<VolProgramme> _volprogcol;
        private FlightAndAirportManager _manager;
        private string _workspace;

        public ProfilCompagnieAerienne(FlightAndAirportManager m)
        {
            Manager = m;
            Manager.Init();
            Workspace = (string)Manager.Mykey.GetValue("Workspace");

            Volgencol = new ListeVolGenerique();
            Volprogcol = new ObservableCollection<VolProgramme>();
            InitializeComponent();
            volProgramme.DataContext = Volprogcol;
            volGenerique.DataContext = Volgencol;
            Compagnie = new CompagnieAerienne
            {
                Nom = "",
                Localisation = "",
                Image = "",
                Code = "SN"
            };
        }

        public CompagnieAerienne Compagnie { get => _compagnie; set => _compagnie = value; }
        public ObservableCollection<VolProgramme> Volprogcol { get => _volprogcol; set => _volprogcol = value; }
        public ListeVolGenerique Volgencol { get => _volgencol; set => _volgencol = value; }
        public string Workspace { get => _workspace; set => _workspace = value; }
        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }

        private void GenAjouter_Click(object sender, RoutedEventArgs e)
        {
            var mod = new ModificationVolGenerique(this);
            mod.Valider += ModVolGen;
            mod.Show();
        }

        private void GenModifier_Click(object sender, RoutedEventArgs e)
        {
            if (volGenerique.SelectedItem != null)
            {

                var mod = new ModificationVolGenerique(this, (VolGenerique)volGenerique.SelectedItem);
                mod.Valider += ModVolGen;
                mod.ShowDialog();
            }
        }

        private void GenSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (volGenerique.SelectedItem != null)
                ((ObservableCollection<VolGenerique>)volGenerique.DataContext).Remove((VolGenerique)volGenerique.SelectedItem);
        }

        public void ModVolGen(bool ajout, VolGenerique vol)
        {
            if (!ajout)
            {
                Volgencol.Remove((VolGenerique)volGenerique.SelectedItem);
            }
            Volgencol.Add(vol);
            Volgencol.Sort();
        }

        private void MenuNouveauLog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nl = new MainWindow(Compagnie);
            nl.Show();
        }

        private void MenuDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow nl = new MainWindow();
            nl.Show();
            Close();
        }

        private void MenuSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();

            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Volgencol.Save(dialog.FileName);
            }
        }

        private void MenuCharger_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Volgencol.Load(dialog.FileName);
            }
        }
    }
}
