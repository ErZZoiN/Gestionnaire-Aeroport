using AeroportLibrary;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;

namespace main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightAndAirportManager manager;
        private readonly bool ajout;

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
                    Manager.Init(code.Text);
                    Manager.Mykey = Registry.CurrentUser.CreateSubKey("Software");
                    Manager.Mykey = Manager.Mykey.CreateSubKey("HEPL");
                    CompagnieAerienne comp = new CompagnieAerienne();
                    switch (code.Text.Length)
                    {
                        case 2:
                            Manager.Mykey = Manager.Mykey.CreateSubKey("Code compagnie aerienne");
                            if (Manager.Mykey.GetSubKeyNames().Contains<string>(code.Text))
                            {
                                Manager.Mykey = Registry.CurrentUser.CreateSubKey("Software");
                                Manager.Mykey = Manager.Mykey.CreateSubKey("HEPL");
                                if ((string)Manager.Mykey.GetValue("Workspace") == null)
                                    Manager.Mykey.SetValue("Workspace", Directory.GetCurrentDirectory());

                                try
                                {
                                    comp.Load((string)Manager.Mykey.GetValue("Workspace") + "\\" + Manager.Code + "Compagnie.xml");
                                }
                                catch (FileNotFoundException)
                                {
                                }
                            }
                            break;
                        case 3:
                            Manager.Mykey = Manager.Mykey.CreateSubKey("Code aeroport");
                            break;
                    }

                    ProfilCompagnieAerienne fenprin = new ProfilCompagnieAerienne(Manager);
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
