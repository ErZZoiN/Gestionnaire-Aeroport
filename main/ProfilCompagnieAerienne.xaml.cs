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
    /// Interaction logic for ProfilCompagnieAerienne.xaml
    /// </summary>
    public partial class ProfilCompagnieAerienne : Window
    {
        private CompagnieAerienne _compagnie;

        public ProfilCompagnieAerienne()
        {
            InitializeComponent();
        }

        public CompagnieAerienne Compagnie { get => _compagnie; set => _compagnie = value; }
    }
}
