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

        public bool Ajout { get => _ajout; set => _ajout = value; }
        public VolGenerique Vol { get => _vol; set => _vol = value; }
        public ProfilCompagnieAerienne Fenetreprincipale { get => _fenetreprincipale; set => _fenetreprincipale = value; }
        public ObservableCollection<Aeroport> ListeAeroportObs { get => _listeAeroportObs; set => _listeAeroportObs = value; }

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
            aeroarr.DataContext = ListeAeroportObs;
        }

        public ModificationVolGenerique(ProfilCompagnieAerienne win, VolGenerique vol)
        {
            Ajout = false;
            Vol = vol;
            Fenetreprincipale = win;
            foreach (Aeroport a in Aeroport.LISTEAEROPORT)
                ListeAeroportObs.Add(a);
            InitializeComponent();
            titre.Content = "Modification d'un vol générique";
            code.Content = Vol.Compagnie.Code;
            aerodep.DataContext = ListeAeroportObs;
            aeroarr.DataContext = ListeAeroportObs;
        }

        private void Appliquer_Click(object sender, RoutedEventArgs e)
        {
            if (Ajout)
            {
                Fenetreprincipale.Volgencol.Add(new VolGenerique
                {
                    Numero = Int32.Parse(numero.Text),
                    Compagnie = Fenetreprincipale.Compagnie,
                    AeroportDepart = (Aeroport)aeroarr.SelectedItem,
                    AeroportArrivee = (Aeroport)aerodep.SelectedItem,
                    HeureArrivee = heurearr.Value.Value.TimeOfDay,
                    HeureDepart = heuredep.Value.Value.TimeOfDay
                });
            }
            else
            {
                Vol.Numero = Int32.Parse(numero.Text);
                Vol.Compagnie = Fenetreprincipale.Compagnie;
                Vol.AeroportDepart = (Aeroport)aeroarr.SelectedItem;
                Vol.AeroportArrivee = (Aeroport)aerodep.SelectedItem;
                Vol.HeureArrivee = heurearr.Value.Value.TimeOfDay;
                Vol.HeureDepart = heuredep.Value.Value.TimeOfDay;
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Appliquer_Click(sender, e);
            Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
