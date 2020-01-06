using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketServer.Class
{
    /// <summary>
    /// 메세지 수신 이벤트
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="data"></param>
    /// <param name="originalMsg"></param>
    public delegate void ReceiveDelegate(string sender, int port, object data, byte[] originalMsg);
    /// <summary>
    /// 시스템 메세지 수신 이벤트
    /// </summary>
    /// <param name="SysMsg"></param>
    public delegate void SystemMSGSendDelegate(string SysMsg);
    /// <summary>
    /// 시스템 에러 메세지 수신 이벤트
    /// </summary>
    /// <param name="SysMsg"></param>
    public delegate void ErrorMSGSendDelegate(string SysMsg);
    /// <summary>
    /// 접속 종료 전달 이벤트
    /// </summary>
    /// <param name="IP"></param>
    /// <param name="Port"></param>
    public delegate void DisconnDelegate(string IP, int Port);
    /// <summary>
    /// Client 접속 시 발생 이벤트
    /// </summary>
    /// <param name="IP"></param>
    /// <param name="Port"></param>
    public delegate void ConnDelegate(string IP, int Port);

    class clsSocket
    {
        #region Private Member

        TcpListener tcpListen = null;
        Dictionary<string, clsClientService> clients = new Dictionary<string, clsClientService>();
        public event ReceiveDelegate OnReceiveDT = delegate { };
        public event SystemMSGSendDelegate OnSystemMsg = delegate { };
        public event ErrorMSGSendDelegate OnErrorMsg = delegate { };
        public event DisconnDelegate OnDisconn = delegate { };
        public event ConnDelegate OnConn = delegate { };

        TcpClient cliSocket = null;

        int ServerPort = 0;

        #endregion
        
        
        /// <summary>
        /// Client 접속 대기
        /// </summary>
        /// <param name="ar"></param>
        protected void AcceptClient(IAsyncResult ar)
        {
            if (tcpListen != null)
            {


                TcpClient cliSocket = new TcpClient();

                try
                {
                    if (tcpListen == null)
                        return;
                    if (!tcpListen.Server.IsBound)
                        return;

                    cliSocket = tcpListen.EndAcceptTcpClient(ar);



                    clsClientService client = new clsClientService();
                    client.OnDisconnected += new DisconnectedDelegate(client_OnDisconnected);
                    client.OnReceiveData += new ReceiveDataDelegate(client_OnReceiveData);


                    //IP 추출 후 해당 클라이언트의 키값으로 사용.
                    string temp = cliSocket.Client.RemoteEndPoint.ToString();
                    string[] tempA = temp.Split(':');
                    string IP = tempA[0];

                    client.StartService(cliSocket, IP, ServerPort);

                    bool DFlag = false;

                    string KEY = IP + ":" + ServerPort;

                    foreach (string cs in clients.Keys)
                    {
                        if (cs == KEY)
                            DFlag = true;
                    }

                    if (DFlag)
                    {
                        clients[KEY].Close();
                        clients.Remove(KEY);
                    }

                    clients.Add(KEY, client);

                    OnSystemMsg("CONNECTED (IP:" + IP + ")");
                    OnConn(IP, ServerPort);
                    
                    tcpListen.BeginAcceptSocket(AcceptClient, null);
                }
                catch (Exception ex)
                {
                    OnErrorMsg("AcceptClient CONNECTED ERROR : " + ex.ToString());
                    if (cliSocket != null)
                        cliSocket.Close();
                    try
                    {
                        tcpListen.BeginAcceptSocket(AcceptClient, null);
                    }
                    catch
                    {
                        return;
                    }
                    return;
                }
            }
        }

        //전체 접속된 Client에 대한 ReceiveData 처리
        void client_OnReceiveData(string IP, int port, object data, byte[] originalMsg)
        {
            OnReceiveDT(IP, port, data, originalMsg);
        }

        void client_OnDisconnected(object sender)
        {

            clsClientService client = sender as clsClientService;
            if (client != null)
            {
                //OnSystemMsg("CLIENT DISCONNECTED " + client.IP);
                OnDisconn(client.IP, client.PORT);

                string KEY = client.IP + ":" + client.PORT;
                if(clients != null)
                    clients.Remove(KEY);

                client.Close();
                //TraceManager.AddLog("Disconnect : IP = " + client.Name);
            }
        }

        /// <summary>
        /// 서버 생성
        /// </summary>
        /// <param name="Port"></param>
        /// <returns></returns>
        public bool Initialize(int Port)
        {

            try
            {
                tcpListen = new TcpListener(new IPEndPoint(IPAddress.Any, Port));
                tcpListen.Start();
                tcpListen.BeginAcceptSocket(AcceptClient, null);
                ServerPort = Port;

                OnSystemMsg("BEGIN LISTENING (PASSIVE: " + Port + ")");

                return true;
            }

            catch (Exception ex)
            {
                OnErrorMsg(" LISTENING ERROR (PASSIVE: " + Port + ")" + ex.ToString());

                return false;
            }
        }

        public void SendMSG(clsMsg NMSG)
        {
            try
            {
                if (NMSG.Port == 0)
                    NMSG.Port = ServerPort;

                string KEY = NMSG.CLIENT_IP + ":" + NMSG.Port;

                //if (clsCommon.Instance.isServerOpen)
                {
                    if (!clients.ContainsKey(KEY))
                    {
                        OnErrorMsg("MSG SEND ERROR " + KEY + "\t" + " 연결되어 있지 않습니다.");

                        return;
                    }

                    if (clients.ContainsKey(KEY))
                        clients[KEY].UQueue.Enqueue(NMSG);
                }

                //if(clsCommon.Instance.isClientConn)
                {
                    
                }
            }
            catch (Exception ex)
            {
                OnErrorMsg("MSG SEND ERROR " + ex.ToString());
            }


        }

        /// <summary>
        /// ACTIVE 로 동작
        /// </summary>
        /// <param name="IP">server IP</param>
        /// <param name="Port">server Port</param>
        /// <returns></returns>
        public bool Conn(string IP, int Port)
        {
            try
            {
                cliSocket = new TcpClient();
                cliSocket.Connect(IP, Port);

                clsCommon.Instance.SERVER_PORT = Port;

                clsClientService Connclient = new clsClientService();
                Connclient.OnDisconnected += new DisconnectedDelegate(client_OnDisconnected);
                Connclient.OnReceiveData += new ReceiveDataDelegate(client_OnReceiveData);

                Connclient.StartService(cliSocket, IP, Port);

                string KEY = IP + ":" + Port;

                clients.Add(KEY, Connclient);

                OnSystemMsg(" CONNECTED (ACTIVE: " + IP + "/" + Port + ")");
                return true;
            }
            catch (Exception ex)
            {
                OnSystemMsg(" CONNECT ERROR (ACTIVE: " + IP + "/" + Port + ") " + ex.ToString());

                return false;
            }
        }

        /// <summary>
        /// 전체 접속 해제 종료 시 호출
        /// </summary>
        /// <returns></returns>
        public bool DisConn()
        {
            try
            {
                try
                {
                    //tcpListen.EndAcceptSocket(null);
                    clsCommon.Instance.isServerOpen = false;
                    if(tcpListen.Server.Connected)
                        tcpListen.Server.Shutdown(SocketShutdown.Both);
                }
                catch (Exception ex)
                {
                    clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
                }
                finally
                {
                    if (tcpListen != null)
                    {
                        tcpListen.Server.Close(0);
                        tcpListen.Stop();
                    }
                }

                //if(Connclient != null)
                //   Connclient.Close();

                if (clients != null)
                {
                    while(clients.Values.Count > 0)
                    {
                        if(clients.Values.First().Connected)
                        {
                            clients.Values.First().Close();
                        }
                        Thread.Sleep(30);
                    }
                }

                //clients = null;

                OnSystemMsg("DISCONNECTED");

                return true;
            }
            catch (Exception ex)
            {
                OnErrorMsg("DISCONNECTED");
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
                return false;
            }
        }

        public bool Client_Disconnect()
        {
            try
            {
                cliSocket.Client.Disconnect(true);
                OnSystemMsg("DISCONNECTED");
                clsCommon.Instance.isClientConn = false;
                return true;
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
                return false;
            }
        }

    }
}
