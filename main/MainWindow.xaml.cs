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
        private RegistryKey HEPLkey;

        public MainWindow()
        {
            RegistryKey mk = Registry.CurrentUser.CreateSubKey("SubKey");
            HEPLkey = mk.CreateSubKey("HEPL");

            foreach(Aeroport a in Aeroport.LISTEAEROPORT)
            {
                RegistryKey temp = HEPLkey.CreateSubKey(a.Code);
            }



            InitializeComponent();
        } 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach(Aeroport a in Aeroport.LISTEAEROPORT)
            {
                RegistryKey temp = HEPLkey.CreateSubKey(a.Code);
            }

            var tmp = new ProfilCompagnieAerienne();
            tmp.Show();
            this.Close();
        }
    }
}
