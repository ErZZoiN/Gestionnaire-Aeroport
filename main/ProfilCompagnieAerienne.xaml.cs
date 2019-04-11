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
        private ObservableCollection<VolProgramme> _volprogaffiche;
        private FlightAndAirportManager _manager;
        private string _workspace; 
        #endregion

        public ProfilCompagnieAerienne(FlightAndAirportManager m)
        {
            Manager = m;
            Workspace = Manager.Workspace;
            Compagnie = new CompagnieAerienne();

            Volgencol = new ListeVols<VolGenerique>();
            Volprogcol = new ListeVols<VolProgramme>();
            Volprogaffiche = new ObservableCollection<VolProgramme>();
            InitializeComponent();
            volProgramme.DataContext = Volprogaffiche;
            volGenerique.DataContext = Volgencol;

            try
            {
                Compagnie.Load(Manager.Datapath + "\\" + Manager.Code + "Compagnie.xml");
            }
            catch(FileNotFoundException)
            {
                Compagnie.Code = Manager.Code;
            }

            try
            {
                Volprogcol.Load(Manager.Datapath + "\\" + "Volprog.xml");
                foreach(VolProgramme v in Volprogcol)
                {
                    if(v.Vol.Compagnie.Code==Compagnie.Code)
                    {
                        Volprogaffiche.Add(v);
                    }
                }

                foreach(VolProgramme v in Volprogaffiche)
                {
                    Volprogcol.Remove(v);
                }

            }
            catch(FileNotFoundException){}

            Closed += ProfilCompagnieAerienne_Closed;
        }

        #region PROPRIETE
        public CompagnieAerienne Compagnie { get => _compagnie; set => _compagnie = value; }
        public ListeVols<VolProgramme> Volprogcol { get => _volprogcol; set => _volprogcol = value; }
        public ListeVols<VolGenerique> Volgencol { get => _volgencol; set => _volgencol = value; }
        public string Workspace { get => _workspace; set => _workspace = value; }
        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }
        public ObservableCollection<VolProgramme> Volprogaffiche { get => _volprogaffiche; set => _volprogaffiche = value; }
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

        public void ModVolGen(bool ajout, VolGenerique vol)
        {
            if (!ajout)
            {
                Volgencol.Remove((VolGenerique)volGenerique.SelectedItem);
            }
            Volgencol.Add(vol);
            Volgencol.Sort();
        }

        private void GenSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (volGenerique.SelectedItem != null)
                ((ObservableCollection<VolGenerique>)volGenerique.DataContext).Remove((VolGenerique)volGenerique.SelectedItem);
        } 
        #endregion

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

        #region TOOLBAR
        private void Programmer_Click(object sender, RoutedEventArgs e)
        {
            if (dateProg.SelectedDate != null && volGenerique.SelectedItems != null)
            {
                foreach (VolGenerique v in volGenerique.SelectedItems.Cast<VolGenerique>().ToList())
                {
                    Volprogaffiche.Add(new VolProgramme(v, dateProg.SelectedDate.Value));
                }
            }
        }

        private void EditCompagnie_Click(object sender, RoutedEventArgs e)
        {
            var mod = new CreationCompagnie(Compagnie, Workspace);
            mod.Valider += Mod_Valider;
            mod.Show();
        }

        private void Mod_Valider(CompagnieAerienne c)
        {
            Compagnie = c;
        }
        #endregion

        private void ProfilCompagnieAerienne_Closed(object sender, EventArgs e)
        {
            Compagnie.Save(Manager.Datapath + "\\" + Compagnie.Code + "Compagnie.xml");
            Volgencol.Save(Manager.Workspace + "\\" + Compagnie.Code + "Volgen.xml");

            foreach(VolProgramme v in Volprogaffiche)
            {
                Volprogcol.Add(v);
            }

            Volprogcol.Save(Manager.Datapath + "\\" + "Volprog.xml");
        }
        #endregion


    }
}
