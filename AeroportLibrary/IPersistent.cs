using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroportLibrary
{
    interface IPersistent
    {
        void SaveInXML(string path);
        void LoadFromXML(string path);
    }
}
