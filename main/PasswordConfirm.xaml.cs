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

namespace main
{
    /// <summary>
    /// Interaction logic for PasswordConfirm.xaml
    /// </summary>
    public partial class PasswordConfirm : Window
    {
        private string pwd;
        private FlightAndAirportManager manager;

        public PasswordConfirm(string p, FlightAndAirportManager f)
        {
            pwd = p;
            manager = f;
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (confirmation.Password == pwd)
                manager.Confirmation = true;
            else
                manager.Confirmation = false;

            Close();
        }
    }
}
