using AeroportLibrary;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

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
                Compagnie.LoadFromXML(Manager.Datapath + "\\" + Manager.Code + "Compagnie.xml");
            }
            catch (FileNotFoundException)
            {
                Compagnie.Code = Manager.Code;
            }

            //Pour afficher uniquement les vols programmés désirés,
            //On prend la liste complète, dont on retire les éléments
            //voulus pour les rajouter dans la liste effectivement
            //affichée.
            //Pour enregistrer la liste, on ajoute les éléments de la
            //collections affichées à la collections complète modifiée
            //au lancement.
            try
            {
                Volprogcol.LoadFromXML(Manager.Datapath + "\\" + "Volprog.xml");
                Volprogcol.Sort();
                foreach (VolProgramme v in Volprogcol)
                {
                    if (v.Vol.Compagnie.Code == Compagnie.Code)
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

            volGenerique.ItemsSource = Volgencol;

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
            ModificationVolGenerique mod = new ModificationVolGenerique(this);
            mod.Valider += ModVolGen;
            mod.Show();
        }

        private void GenModifier_Click(object sender, RoutedEventArgs e)
        {
            if (volGenerique.SelectedItem != null)
            {

                ModificationVolGenerique mod = new ModificationVolGenerique(this, (VolGenerique)volGenerique.SelectedItem);
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
            foreach (VolGenerique v in volGenerique.SelectedItems.Cast<VolGenerique>().ToList())
            {
                Volgencol.Remove(v);
            }
        }
        #endregion

        #region PROGRAMME DATAGRID
        private void ProgSupprimer_Click(object sender, RoutedEventArgs e)
        {
            foreach (VolProgramme v in volProgramme.SelectedItems.Cast<VolProgramme>().ToList())
            {
                Volprogaffiche.Remove(v);
            }
        } 
        #endregion

        #region MENU

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutMe(Compagnie).ShowDialog();
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
            using (SaveFileDialog dialog = new SaveFileDialog
            {
                InitialDirectory = Workspace
            })
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = ".xml";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Volgencol.SaveInXML(dialog.FileName);
                }
            }
        }

        private void MenuCharger_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = Workspace
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Volgencol.LoadFromXML(dialog.FileName);
            }
        }

        private void MenuImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = Workspace
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Volgencol.LoadFromCSV(dialog.FileName);
            }
        }

        private void MenuExport_Click(object sender, RoutedEventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog
            {
                InitialDirectory = Workspace
            })
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = ".csv";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Volgencol.SaveInCSV(dialog.FileName);
                }
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
            CreationCompagnie mod = new CreationCompagnie(Compagnie, Workspace);
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
            Compagnie.SaveInXML(Manager.Datapath + "\\" + Compagnie.Code + "Compagnie.xml");
            Volgencol.SaveInXML(Manager.Workspace + "\\" + Compagnie.Code + "Volgen.xml");

            foreach (VolProgramme v in Volprogaffiche)
            {
                Volprogcol.Add(v);
            }

            Volprogcol.SaveInXML(Manager.Datapath + "\\" + "Volprog.xml");
        }

        #endregion

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
    }
}
