using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using AeroportLibrary;
using System.IO;
using System.Windows.Forms;

namespace main
{
    public class FlightAndAirportManager
    {
        private RegistryKey _mykey;
        private bool _confirmation;

        public RegistryKey Mykey { get => _mykey; set => _mykey = value; }
        public bool Confirmation { get => _confirmation; set => _confirmation = value; }

        public FlightAndAirportManager()
        {
            Mykey = Registry.CurrentUser.CreateSubKey("Software");
            Mykey = Mykey.CreateSubKey("HEPL");
            Confirmation = false;
        }

        public bool Connexion(string code, string password, string login)
        {
            Mykey = Registry.CurrentUser.CreateSubKey("Software");
            Mykey = Mykey.CreateSubKey("HEPL");
            Confirmation = false;

            switch (code.Length)
            {
                case 2: Mykey = Mykey.CreateSubKey("Code compagnie aerienne");
                    break;
                case 3: Mykey = Mykey.CreateSubKey("Code aeroport");
                    break;
                default: return false;
            }

            Mykey = Mykey.CreateSubKey(code);

            if (Mykey.GetValue(login) == null && Mykey.ValueCount < 2)
            {
                return NouveauLog(code, password, login);
            }
            else if ((string)Mykey.GetValue(login) != password)
            {
                System.Windows.MessageBox.Show("Mot de passe incorrect.");
                return false;
            }
            else if ((string)Mykey.GetValue(login) == password)
                return true;

            return false;
        }

        public void Init()
        {
            if((string)Mykey.GetValue("Workspace") == null)
            {
                var dialog = new FolderBrowserDialog();
                dialog.ShowNewFolderButton = true;

                dialog.SelectedPath = Directory.GetCurrentDirectory();

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    Mykey.SetValue("Workspace", dialog.SelectedPath);
                }
            }
        }

        public bool NouveauLog(string code,string password, string login)
        {
            Mykey = Registry.CurrentUser.CreateSubKey("Software");
            Mykey = Mykey.CreateSubKey("HEPL");
            Confirmation = false;

            switch (code.Length)
            {
                case 2:
                    Mykey = Mykey.CreateSubKey("Code compagnie aerienne");
                    break;
                case 3:
                    Mykey = Mykey.CreateSubKey("Code aeroport");
                    break;
                default: return false;
            }
            Mykey = Mykey.CreateSubKey(code);

            PasswordConfirm pw = new PasswordConfirm(password, this);
            pw.ShowDialog();
            if (Confirmation)
            {
                Mykey.SetValue(login, password);
                return true;
            }
            else return false;
        }
    }
}
