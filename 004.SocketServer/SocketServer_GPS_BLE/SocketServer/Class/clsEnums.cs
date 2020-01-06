using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Class
{
    public static class clsEnums
    {
        public enum DATA_TYPE
        {
            UNKNOWN = 0,
            H2S = 7,
            O2 = 8,
            CO = 9,
            TEMP = 10,
        }
    }
}
