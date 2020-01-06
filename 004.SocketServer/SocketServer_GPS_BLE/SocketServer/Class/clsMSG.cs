using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Class
{
    public class clsMsg
    {
        private string _CLIENT_IP = string.Empty;
        private int _Port = 0;
        private string _Target = "SERVER";
        private string _S_DATA = string.Empty;
        private string _S_DATE = string.Empty;

        /// <summary>
        /// 대상 IP (1:1 통신일 경우 사용하지 않아도 됨
        /// </summary>
        public string CLIENT_IP
        {
            get { return _CLIENT_IP; }
            set { _CLIENT_IP = value; }
        }

        /// <summary>
        /// Data Item
        /// </summary>
        public string S_DATA
        {
            get { return _S_DATA; }
            set { _S_DATA = value; }
        }

        public string TARGET { get => _Target; set => _Target = value; }
        public int Port { get => _Port; set => _Port = value; }
        public string S_DATE { get => _S_DATE; set => _S_DATE = value; }
    }
}
