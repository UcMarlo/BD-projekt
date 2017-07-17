using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Napraw.InterfaceHelper
{
    public static class parsingHelper
    {

        public static bool checkIfEmpty(string result)
        {
            if (result == String.Empty)
            {
                return true;
            }
            else
                return false;
        }

    }
}
