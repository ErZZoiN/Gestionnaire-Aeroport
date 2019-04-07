using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AeroportLibrary;

namespace main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightAndAirportManager manager;
        private bool ajout;

        public MainWindow()
        {
            ajout = false;
            Manager = new FlightAndAirportManager();
            InitializeComponent();
        }

        public MainWindow(CompagnieAerienne c)
        {
            ajout = true;
            Manager = new FlightAndAirportManager();
            InitializeComponent();
            code.Text = c.Code;
            code.IsReadOnly = true;
            Title = "Nouveau login";
        }

        public FlightAndAirportManager Manager { get => manager; set => manager = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ajout)
            {
                if (Manager.Connexion(code.Text, password.Password, login.Text))
                {
                    var fenprin = new ProfilCompagnieAerienne(Manager);
                    fenprin.Show();
                    Close();
                }
            }
            else
            {
                if (Manager.NouveauLog(code.Text, password.Password, login.Text))
                    Close();
            }
        }
    }
}
