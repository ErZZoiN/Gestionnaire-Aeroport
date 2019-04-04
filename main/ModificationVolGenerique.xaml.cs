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
    /// Interaction logic for ModificationVolGenerique.xaml
    /// </summary>
    public partial class ModificationVolGenerique : Window
    {
        private bool _ajout;
        private VolGenerique _vol;
        private ProfilCompagnieAerienne _fenetreprincipale;
        private ObservableCollection<Aeroport> _listeAeroportObs = new ObservableCollection<Aeroport>();

        public ModificationVolGenerique(ProfilCompagnieAerienne win)
        {
            Ajout = true;
            Fenetreprincipale = win;
            foreach (Aeroport a in Aeroport.LISTEAEROPORT)
                ListeAeroportObs.Add(a);
            InitializeComponent();
            code.Content = win.Compagnie.Code;
            numero.IsReadOnly = false;
            aerodep.DataContext = ListeAeroportObs;
        }

        public ModificationVolGenerique(ProfilCompagnieAerienne win, VolGenerique vol)
        {
            Ajout = false;
            Vol = vol;
            Fenetreprincipale = win;
            InitializeComponent();
            titre.Content = "Modification d'un vol générique";
            code.Content = Vol.Compagnie.Code;
        }

        public bool Ajout { get => _ajout; set => _ajout = value; }
        public VolGenerique Vol { get => _vol; set => _vol = value; }
        public ProfilCompagnieAerienne Fenetreprincipale { get => _fenetreprincipale; set => _fenetreprincipale = value; }
        public ObservableCollection<Aeroport> ListeAeroportObs { get => _listeAeroportObs; set => _listeAeroportObs = value; }

        private void Appliquer_Click(object sender, RoutedEventArgs e)
        {
            if(Ajout)
            {
                Fenetreprincipale.Volgencol.Add(new VolGenerique
                {
                    Numero = Int32.Parse(numero.Text),
                    Compagnie = Fenetreprincipale.Compagnie,
                });
            }
        }
    }
}
