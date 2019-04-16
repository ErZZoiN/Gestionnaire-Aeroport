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
using AeroportLibrary;

namespace main
{
    /// <summary>
    /// Logique d'interaction pour AboutMeCompagnie.xaml
    /// </summary>
    public partial class AboutMe : Window
    {
        public AboutMe(CompagnieAerienne c)
        {
            InitializeComponent();
            Title = c.Nom;
            nom.Content = c.Nom;
            localisation.Content = c.Localisation;
            code.Content = c.Code;
            try
            {
                logo.Source = new BitmapImage(new Uri(c.Image));
            }
            catch (System.IO.FileNotFoundException)
            {

            }
        }

        public AboutMe(Aeroport a)
        {
            InitializeComponent();
            Title = a.Nomination;
            nom.Content = a.Nomination;
            localisation.Content = a.Ville + " " + a.Pays.Nom;
            code.Content = a.Code;
        }
    }
}
