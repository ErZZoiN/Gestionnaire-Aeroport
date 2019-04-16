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
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace main
{
    /// <summary>
    /// Logique d'interaction pour Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private FlightAndAirportManager _manager;
        public Options(FlightAndAirportManager m)
        {
            Manager = m;
            InitializeComponent();
            data.Text = m.Datapath;
            image.Text = m.Imagepath;
        }

        public FlightAndAirportManager Manager { get => _manager; set => _manager = value; }

        private void Data_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (FolderBrowserDialog tmp = new FolderBrowserDialog())
            {
                tmp.SelectedPath = data.Text;

                if (tmp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    data.Text = tmp.SelectedPath;
                }
            }
        }

        private void Image_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (FolderBrowserDialog tmp = new FolderBrowserDialog())
            {
                tmp.SelectedPath = ((System.Windows.Controls.TextBox)sender).Text;

                if (tmp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    image.Text = tmp.SelectedPath;
                }
            }
        }

        public delegate void optionDone(FlightAndAirportManager m);
        public event optionDone Valider;

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            Manager.Datapath = data.Text;
            Manager.Imagepath = image.Text;

            Manager.Mykey = Registry.CurrentUser.CreateSubKey("Software");
            Manager.Mykey = Manager.Mykey.CreateSubKey("HEPL");
            Manager.Mykey.SetValue("Workspace", Manager.Workspace);
            Manager.Mykey.SetValue("Image", Manager.Imagepath);

            Valider(Manager);
        }
    }
}
