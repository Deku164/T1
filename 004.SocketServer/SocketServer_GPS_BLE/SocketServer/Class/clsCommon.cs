using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Class
{
    class clsCommon
    {
        #region Private Member

        private bool _ProgramRUN = true;
        private int _SERVER_PORT = 7070;
        private bool _isServerOpen = false;
        private bool _isClientConn = false;
        private string _ServerIP = string.Empty;

        #endregion

        #region Instance

        private static clsCommon _instance = new clsCommon();

        public static clsCommon Instance
        {
            get
            {
                return _instance;
            }
        }

        public bool ProgramRUN { get => _ProgramRUN; set => _ProgramRUN = value; }
        public int SERVER_PORT { get => _SERVER_PORT; set => _SERVER_PORT = value; }
        public bool isServerOpen { get => _isServerOpen; set => _isServerOpen = value; }
        public bool isClientConn { get => _isClientConn; set => _isClientConn = value; }
        public string ServerIP { get => _ServerIP; set => _ServerIP = value; }

        #endregion // Instance

        #region Property

        #endregion

        private clsCommon()
        {
        }
    }
}
