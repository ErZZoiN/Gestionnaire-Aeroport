﻿using System;
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
        private string _code;
        private string _datapath;
        private string _imagepath;
        private string _workspace;

        public RegistryKey Mykey { get => _mykey; set => _mykey = value; }
        public bool Confirmation { get => _confirmation; set => _confirmation = value; }
        public string Code { get => _code; set => _code = value; }
        public string Datapath { get => _datapath; set => _datapath = value; }
        public string Imagepath { get => _imagepath; set => _imagepath = value; }
        public string Workspace { get => _workspace; set => _workspace = value; }

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

            if (Mykey.GetValue(login) == null && Mykey.ValueCount < 2) //Si on est le premier utilisateur de la compagnie, on créé un login
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

        //Cette fonction initialise les différentes variables du manager
        //En fonction du code rentré : il récupère le workspace global,
        //Le dossier d'image et le workspace du compte utilisateur.
        public void Init(string code)
        {
            Code = code;
            Mykey = Registry.CurrentUser.CreateSubKey("Software");
            Mykey = Mykey.CreateSubKey("HEPL");
            if ((string)Mykey.GetValue("Workspace") == null)
                Mykey.SetValue("Workspace", Directory.GetCurrentDirectory());

            Datapath = (string)Mykey.GetValue("Workspace");

            if((string)Mykey.GetValue("Image") == null)
                Mykey.SetValue("Image", Directory.GetCurrentDirectory());

            Imagepath = (string)Mykey.GetValue("Image");
            switch (Code.Length)
            {
                case 2:
                    Mykey = Mykey.CreateSubKey("Code compagnie aerienne");
                    break;
                case 3:
                    Mykey = Mykey.CreateSubKey("Code aeroport");
                    break;
                default: return; //Cette possibilité arrive lorsqu'on se connecte avec le login admin
            }
            Mykey = Mykey.CreateSubKey(Code);

            if ((string)Mykey.GetValue("Workspace") == null)
            {
                var dialog = new FolderBrowserDialog
                {
                    ShowNewFolderButton = true,
                    SelectedPath = Directory.GetCurrentDirectory()
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Mykey.SetValue("Workspace", dialog.SelectedPath);
                }
            }

            Workspace = (string)Mykey.GetValue("Workspace");
        }


        //Est appelé lorsqu'un nouveau compte doit être créé. Il demande
        //une confirmation du mot de passe, puis créé une nouvelle entrée
        //dans la Registry.
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
                default: return false; //Ne devrait JAMAIS arriver
            }
            Mykey = Mykey.CreateSubKey(code);

            new PasswordConfirm(password, this).ShowDialog();
            if (Confirmation) //Confirmation est modifié par le PasswordConfirm
            {
                Mykey.SetValue(login, password);
                return true;
            }
            else return false;
        }
    }
}
