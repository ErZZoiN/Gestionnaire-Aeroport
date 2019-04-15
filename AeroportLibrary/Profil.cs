using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    public abstract class Profil : IPersistent
    {
        private string _code;

        public string Code { get => _code; set => _code = value; }

        public abstract void Load(string path);
        public abstract void Save(string path);
    }
}
