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
        #region VARIABLE
        private CompagnieAerienne _compagnie;
        private ListeVols<VolGenerique> _volgencol;
        private ListeVols<VolProgramme> _volprogcol;
        private FlightAndAirportManager _manager;
        private string _workspace; 
        #endregion

        public ProfilCompagnieAerienne(FlightAndAirportManager m)
        {
            Manager = m;
            Workspace = Manager.Workspace;

            Volgencol = new ListeVols<VolGenerique>();
            Volprogcol = new ListeVols<VolProgramme>();
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
            this.Closed += ProfilCompagnieAerienne_Closed;
        }

        #region PROPRIETE
        public CompagnieAerienne Compagnie { get => _compagnie; set => _compagnie = value; }
        public ListeVols<VolProgramme> Volprogcol { get => _volprogcol; set => _volprogcol = value; }
        public ListeVols<VolGenerique> Volgencol { get => _volgencol; set => _volgencol = value; }
        public string Workspace { get => _workspace; set => _workspace = value; }
        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }
        #endregion

        #region LISTENERS
        #region GENERIQUE DATAGRID
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
        #endregion

        public void ModVolGen(bool ajout, VolGenerique vol)
        {
            if (!ajout)
            {
                Volgencol.Remove((VolGenerique)volGenerique.SelectedItem);
            }
            Volgencol.Add(vol);
            Volgencol.Sort();
        }

        private void ProfilCompagnieAerienne_Closed(object sender, EventArgs e)
        {
            Compagnie.Save(Manager.Datapath + "\\" + Compagnie.Code + "Compagnie.xml");
            Volgencol.Save(Manager.Workspace + "\\" + Compagnie.Code + "Volgen.xml");
        }

        #region MENU
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
            dialog.InitialDirectory = Workspace;
            Console.WriteLine(Workspace);
            Console.WriteLine("COUCUO");
            dialog.AddExtension = true;
            dialog.DefaultExt = ".xml";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Volgencol.Save(dialog.FileName);
            }
        }

        private void MenuCharger_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Workspace;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Volgencol.Load(dialog.FileName);
            }
        }
        #endregion

        #endregion

        private void Programmer_Click(object sender, RoutedEventArgs e)
        {
            if(dateProg.SelectedDate!=null && volGenerique.SelectedItems!=null)
            {
                foreach(VolGenerique v in volGenerique.SelectedItems.Cast<VolGenerique>().ToList())
                {
                    Volprogcol.Add(new VolProgramme(v, dateProg.SelectedDate.Value));
                }
            }
        }
    }
}
