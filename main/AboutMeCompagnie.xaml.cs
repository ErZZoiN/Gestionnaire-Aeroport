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
    public partial class AboutMeCompagnie : Window
    {
        public AboutMeCompagnie(CompagnieAerienne c)
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
    }
}
