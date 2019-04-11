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
    /// Interaction logic for CreationCompagnie.xaml
    /// </summary>
    public partial class CreationCompagnie : Window
    {
        private string workspace;

        public CreationCompagnie(CompagnieAerienne c, string ws)
        {
            workspace = ws;
            InitializeComponent();
            nom.Text = c.Nom;
            image.Text = c.Image;
            localisation.Text = c.Localisation;
            code.Text = c.Code;
        }

        public delegate void ModComp(CompagnieAerienne c);
        public event ModComp Valider;

        private void Image_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = workspace;
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                image.Text = dialog.FileName;
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            var comp = new CompagnieAerienne();
            comp.Code = code.Text;
            comp.Localisation = localisation.Text;
            comp.Image = image.Text;
            comp.Nom = nom.Text;

            Valider(comp);
            Close();
        }
    }
}
