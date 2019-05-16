using AeroportLibrary;
using Microsoft.Win32;
using System;
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
            Console.WriteLine("coucou");
        }

        public MainWindow(Profil c)
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
                if(login.Text=="admin" && password.Password == "admin")
                {
                    Manager.Init("");
                    new Options(Manager).ShowDialog();
                }
                else if (Manager.Connexion(code.Text, password.Password, login.Text))
                {
                    Profil comp;
                    Manager.Init(code.Text);
                    Manager.Mykey = Registry.CurrentUser.CreateSubKey("Software");
                    Manager.Mykey = Manager.Mykey.CreateSubKey("HEPL");
                    switch (code.Text.Length)
                    {
                        case 2:
                            comp = new CompagnieAerienne(code.Text);

                            //Récupère les données de la compagnie si elles existent
                            Manager.Mykey = Manager.Mykey.CreateSubKey("Code compagnie aerienne");
                            if (Manager.Mykey.GetSubKeyNames().Contains<string>(code.Text))
                            {
                                Manager.Mykey = Registry.CurrentUser.CreateSubKey("Software");
                                Manager.Mykey = Manager.Mykey.CreateSubKey("HEPL");
                                if ((string)Manager.Mykey.GetValue("Workspace") == null)
                                    Manager.Mykey.SetValue("Workspace", Directory.GetCurrentDirectory());

                                try
                                {
                                    comp.LoadFromXML((string)Manager.Mykey.GetValue("Workspace") + "\\" + Manager.Code + "Compagnie.xml");
                                }
                                catch (FileNotFoundException)
                                {
                                }
                            }
                            ProfilCompagnieAerienne fenprin = new ProfilCompagnieAerienne(Manager);
                            fenprin.Show();
                            break;
                        case 3:
                            comp = new Aeroport() { Code = code.Text };


                            //Récupère les données de l'aéroport si elles existent
                            Manager.Mykey = Manager.Mykey.CreateSubKey("Code aeroport");
                            if (Manager.Mykey.GetSubKeyNames().Contains<string>(code.Text))
                            {
                                Manager.Mykey = Registry.CurrentUser.CreateSubKey("Software");
                                Manager.Mykey = Manager.Mykey.CreateSubKey("HEPL");
                                if ((string)Manager.Mykey.GetValue("Workspace") == null)
                                    Manager.Mykey.SetValue("Workspace", Directory.GetCurrentDirectory());

                                try
                                {
                                    comp.LoadFromXML((string)Manager.Mykey.GetValue("Workspace") + "\\" + Manager.Code + "Aeroport.xml");
                                }
                                catch (FileNotFoundException)
                                {
                                }
                            }
                            var fenprin2 = new ProfilAeroport(Manager);
                            fenprin2.Show();
                            break;
                    }
                    Close();
                }
            }
            else //Si la fenêtre a été ouverte pour l'ajout d'un nouveau login
            {
                if (Manager.NouveauLog(code.Text, password.Password, login.Text))
                    Close();
            }
        }
    }
}
