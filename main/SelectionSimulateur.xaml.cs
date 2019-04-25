using System;
using System.Collections.Generic;
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
using Xceed.Wpf.Toolkit;
using AeroportLibrary;

namespace main
{
    /// <summary>
    /// Logique d'interaction pour SelectionSimulateur.xaml
    /// </summary>
    public partial class SelectionSimulateur : Window
    {
        private Simulateur _monsim;

        public Simulateur Monsim { get => _monsim; set => _monsim = value; }

        public SelectionSimulateur(Simulateur sim)
        {
            Monsim = sim;
            InitializeComponent();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Monsim.Debut = debut.Value.Value;
            Monsim.Vitesse = Int32.Parse(vitesse.Text);
            Monsim.Duree = Int32.Parse(duree.Text);
            Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Monsim.Close();
            Close();
        }
    }
}
