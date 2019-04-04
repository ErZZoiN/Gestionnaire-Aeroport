using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProfilCompagnieAerienne.xaml
    /// </summary>
    public partial class ProfilCompagnieAerienne : Window
    {
        private CompagnieAerienne _compagnie;
        private ObservableCollection<VolGenerique> _volgencol;
        private ObservableCollection<VolProgramme> _volprogcol;

        public ProfilCompagnieAerienne()
        {
            Volgencol = new ObservableCollection<VolGenerique>();
            Volprogcol = new ObservableCollection<VolProgramme>();
            InitializeComponent();
            volProgramme.DataContext = Volprogcol;
            volGenerique.DataContext = Volgencol;
            Compagnie = new CompagnieAerienne
            {
                Nom = "Brussels airlines",
                Localisation = "Bruxelles",
                Image = "",
                Code = "BR"
            };
        }

        public CompagnieAerienne Compagnie { get => _compagnie; set => _compagnie = value; }
        public ObservableCollection<VolProgramme> Volprogcol { get => _volprogcol; set => _volprogcol = value; }
        public ObservableCollection<VolGenerique> Volgencol { get => _volgencol; set => _volgencol = value; }

        private void GenAjouter_Click(object sender, RoutedEventArgs e)
        {
            var mod = new ModificationVolGenerique(this);
            mod.Show();
        }
    }
}
