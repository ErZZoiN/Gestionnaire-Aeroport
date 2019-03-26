using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    interface IPersistent
    {
        void Save(string path);
        void Load(string path);
    }
}
