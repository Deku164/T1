using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace SocketServer.Class
{
    public interface ITransmissionn
    {
        Boolean Open();
        void Server_Open();
        void Client_Open(string Server_IP, int Server_Port);
        void Close();
    }

    public class clsTransmissionn : ITransmissionn
    {
        #region Private Member

        clsSocket clsSocket = new clsSocket();
        Thread SocketSendThread;
        Thread QueueRecvThread;
        //MySqlConnection connection;
        SqlConnection sqlConn;
        SqlCommand cmd;

        #endregion

        public Boolean Open()
        {
            try
            {
                string Connection_String = clsConfig.ReadAppConfig("CONNECTION_STRING");

                sqlConn = new SqlConnection(Connection_String);
                sqlConn.Open();
                cmd = new SqlCommand("PD_DATA_INSERT", sqlConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@P_DEVICE_ID", SqlDbType.VarChar, 10);
                cmd.Parameters.Add("@P_DATA_TYPE", SqlDbType.VarChar, 1);
                cmd.Parameters.Add("@P_RAW_DATA", SqlDbType.VarChar, 1000);

                //connection = new MySqlConnection(str);
                //connection.Open();

                SocketSendThread = new Thread(new ThreadStart(SendThread))
                {
                    Name = "Socket Send Thread",
                    Priority = ThreadPriority.Highest
                };
                SocketSendThread.Start();

                QueueRecvThread = new Thread(new ThreadStart(RecvThread))
                {
                    Name = "Queue Recv Thread",
                    Priority = ThreadPriority.Highest
                };
                QueueRecvThread.Start();

                clsSocket.OnReceiveDT += ClsSocket_OnReceiveDT;
                clsSocket.OnConn += ClsSocket_OnConn;
                clsSocket.OnDisconn += ClsSocket_OnDisconn;
                clsSocket.OnSystemMsg += ClsSocket_OnSystemMsg;
                clsSocket.OnErrorMsg += ClsSocket_OnErrorMsg;                

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public void Server_Open()
        {
            #region Passive 서버 오픈
            clsCommon.Instance.SERVER_PORT = Convert.ToInt32(clsConfig.ReadAppConfig("SERVER_PORT"));

            if (clsSocket.Initialize(clsCommon.Instance.SERVER_PORT))
            {
                clsLOG.SetLOG(LOGTYPE.Message, "TCP Server Open (Port : " + clsCommon.Instance.SERVER_PORT.ToString() + ")");
                clsCommon.Instance.isServerOpen = true;
            }
            else
            {
                clsLOG.SetLOG(LOGTYPE.Message, "TCP Server Open Fail");
                clsCommon.Instance.isServerOpen = false;
            }
            #endregion
        }

        public void Client_Open(string Server_IP,int Server_Port)
        {
            #region Client 접속
            if (clsSocket.Conn(Server_IP, Server_Port))
            {
                clsLOG.SetLOG(LOGTYPE.Message, "TCP Client Connect (Port : " + Server_Port.ToString() + ")");
                clsCommon.Instance.isClientConn = true;
                clsCommon.Instance.ServerIP = Server_IP;
            }
            else
            {
                clsLOG.SetLOG(LOGTYPE.Message, "TCP Client Connect Fail");
                clsCommon.Instance.isClientConn = false;                
            }
            #endregion
        }

        private void ClsSocket_OnErrorMsg(string SysMsg)
        {
            clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, SysMsg);
        }

        private void ClsSocket_OnSystemMsg(string SysMsg)
        {
            clsLOG.SetLOG(LOGTYPE.SYSTEM, SysMsg);
        }

        private void ClsSocket_OnConn(string ip, int Port)
        {
            //접속시 EVENT
            clsLOG.SetLOG(LOGTYPE.Message, string.Format(" [CONNECT] :{0},\t{1}", ip, "CONNECTED SOCKET"));
        }

        private void ClsSocket_OnDisconn(string ip, int Port)
        {
            //접속종료시 EVENT
            clsLOG.SetLOG(LOGTYPE.Message, string.Format(" [DISCONN] :{0},\t{1}", ip, "DISCONNECTED SOCKET"));
        }

        private void ClsSocket_OnReceiveDT(string ip, int port, object data, byte[] originalMsg)
        {
            string msg = string.Empty;
            try
            {
                //수신된 Original MSG를 로그 및 UI에 남긴다.
                clsLOG.SetLOG(LOGTYPE.Message, string.Format(" [<<-RECV] :{0}, \t{1}", ip, data));

                //메세지 처리부 enQueue
                clsMsg Msg = new clsMsg
                {
                    CLIENT_IP = ip,
                    Port = port,
                    S_DATA = data.ToString()
                };
                QueueManager.Instance.ReceiveEnqueue(Msg);
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }

        private void SendThread()
        {
            clsMsg sendMsg = null;

            try
            {
                while (clsCommon.Instance.ProgramRUN)
                {
                    if (QueueManager.Instance.SendQueueCount() > 0)
                    {
                        //딜레이가 있을 경우 현재 시간과 비교해서 해당 시간이 지난경우 보냄.
                        sendMsg = QueueManager.Instance.SendDequeue();
                    }
                    if (sendMsg != null)
                        TransmitMsg_STD(sendMsg);
                    sendMsg = null;

                    Thread.Sleep(10);
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch { }
        }

        private void TransmitMsg_STD(clsMsg msg)
        {
            try
            {
                clsSocket.SendMSG(msg);                
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }

        private void RecvThread()
        {
            clsMsg recvMsg = null;

            try
            {
                while (clsCommon.Instance.ProgramRUN)
                {
                    if (QueueManager.Instance.ReceiveQueueCount() > 0)
                    {
                        recvMsg = QueueManager.Instance.ReceiveDequeue();
                    }
                    if (recvMsg != null)
                        RecvMsg_STD(recvMsg);
                    recvMsg = null;

                    Thread.Sleep(10);
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch { }
        }

        public string ConvertHex(string hexString)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hs = hexString.Substring(i, 2);
                sb.Append(System.Convert.ToChar(System.Convert.ToUInt32(hs, 16)).ToString());
            }
            return sb.ToString();
        }

        private void RecvMsg_STD(clsMsg msg)
        {
            try
            {
                QueueManager.Instance.MainUISendEnqueue(msg.S_DATA);

                //0001B50:8d:6f:b0:02:11,-71
                //DB 저장 프로시저 호출
                if (msg.S_DATA.Length > 10)
                {
                    cmd.Parameters["@P_DEVICE_ID"].Value = msg.S_DATA.Substring(0, 4);
                    cmd.Parameters["@P_DATA_TYPE"].Value = msg.S_DATA.Substring(4, 1);
                    cmd.Parameters["@P_RAW_DATA"].Value = msg.S_DATA.Substring(5);

                    cmd.ExecuteNonQuery();
                }
                
            }
            catch (Exception)
            {

                throw;
            }

            #region "gas data 주석처리"
            //string parseData = string.Empty;

            //string groupID = string.Empty;            
            //string sensorID = string.Empty;
            //string data_1 = string.Empty;
            //string data_2 = string.Empty;
            //string data_3 = string.Empty;
            //string unit_1 = string.Empty;
            //string unit_2 = string.Empty;
            //string unit_3 = string.Empty;

            //string str = string.Empty;

            //int imsi_DeviceID = 0;
            //int imsi_FormatIDX = 0;
            //string imsi_row = string.Empty;

            //MySqlCommand cmd;

            //try
            //{
            //    parseData = ConvertHex(msg.S_DATA.Substring(2, msg.S_DATA.Length - 4));
            //    //parseData = msg.S_DATA.Substring(2,msg.S_DATA.Length -4);

            //    clsLOG.SetLOG(LOGTYPE.Message, string.Format(" [<<-RECV] :{0}\t{1}", "변환 Data", parseData));

            //    //파싱
            //    groupID = parseData.Substring(0, 2);
            //    //임시

            //    switch(groupID)
            //    {
            //        case "FF":
            //            imsi_DeviceID = 2;
            //            break;
            //        case "FE":
            //            imsi_DeviceID = 3;
            //            break;
            //        case "FD":
            //            imsi_DeviceID = 4;
            //            break;
            //    }

            //    //groupID = "10";
            //    sensorID = parseData.Substring(2, 2);

            //    switch (sensorID)
            //    {
            //        case "01":
            //            imsi_FormatIDX = 7;
            //            break;
            //        case "02":
            //            imsi_FormatIDX = 8;
            //            break;
            //        case "03":
            //            imsi_FormatIDX = 9;
            //            break;
            //        case "05":
            //            imsi_FormatIDX = 10;
            //            break;
            //    }


            //    unit_1 = parseData.Substring(4, 1);



            //    switch(unit_1)
            //    {
            //        //황화수소
            //        case "S":
            //            data_1 = parseData.Substring(5, 6);
            //            unit_2 = parseData.Substring(11, 1);
            //            data_2 = parseData.Substring(12, 2);

            //            str = "INSERT INTO data " +
            //                "(format_idx, device_idx, raw) VALUES (7, '" +
            //                groupID + "', '" +
            //                msg + "')";

            //            break;

            //        //산소
            //        case "O":
            //            //임시로 차트에 백분위 표시로 변경
            //            //data_1 = parseData.Substring(5, 6);
            //            //unit_2 = parseData.Substring(11, 1);
            //            //data_2 = parseData.Substring(12, 6);
            //            data_2 = parseData.Substring(5, 6);
            //            unit_2 = parseData.Substring(11, 1);
            //            data_1 = parseData.Substring(12, 6);

            //            str = "INSERT INTO data " +
            //                "(format_idx, device_idx, raw) VALUES (8, '" +
            //                groupID + "', '" +
            //                msg + "')";

            //            break;
            //        //이산화탄소
            //        case "C":
            //            data_1 = parseData.Substring(5, 5);
            //            unit_2 = parseData.Substring(10, 1);
            //            data_2 = parseData.Substring(11, 5);

            //            str = "INSERT INTO data " +
            //                "(format_idx, device_idx, raw) VALUES (9, '" +
            //                groupID + "', '" +
            //                msg + "')";

            //            break;
            //        //분진 사용 X
            //        case "X":
            //            data_1 = parseData.Substring(5, 5);
            //            unit_2 = parseData.Substring(10, 1);
            //            data_2 = parseData.Substring(11, 5);
            //            unit_3 = parseData.Substring(16, 1);
            //            data_3 = parseData.Substring(17, 5);
            //            break;
            //        //온습도
            //        case "T":
            //            data_1 = parseData.Substring(5, 4);
            //            unit_2 = parseData.Substring(9, 1);
            //            data_2 = parseData.Substring(10, 4);

            //            str = "INSERT INTO data " +
            //                "(format_idx, device_idx, raw) VALUES (10, '" +
            //                groupID + "', '" +
            //                msg + "')";

            //            break;

            //    }



            //    //cmd = new MySqlCommand(str, connection);
            //    //int result = cmd.ExecuteNonQuery();

            //    cmd = new MySqlCommand();
            //    cmd.Connection = connection;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.CommandText = "INSERT_DATA";

            //    MySqlParameter pin1 = new MySqlParameter("_DEVICE_IDX", MySqlDbType.Int32);
            //    pin1.Direction = ParameterDirection.Input;
            //    pin1.Value = 0;
            //    cmd.Parameters.Add(pin1);

            //    MySqlParameter pin2 = new MySqlParameter("_FORMAT_IDX", MySqlDbType.Int32);
            //    pin2.Direction = ParameterDirection.Input;
            //    pin2.Value = imsi_FormatIDX;
            //    cmd.Parameters.Add(pin2);

            //    MySqlParameter pin3 = new MySqlParameter("_RAW", MySqlDbType.VarChar);
            //    pin3.Direction = ParameterDirection.Input;
            //    pin3.Value = groupID + "," + sensorID + "," + data_1 + "," + data_2;
            //    cmd.Parameters.Add(pin3);

            //    MySqlParameter pout1 = new MySqlParameter("RESULT", MySqlDbType.Int32, 1);
            //    pout1.Direction = ParameterDirection.Output;
            //    cmd.Parameters.Add(pout1);


            //    int rst = cmd.ExecuteNonQuery();

            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.ToString());
            //}
            #endregion
        }

        public void Close()
        {
            //connection.Close();  
            sqlConn.Close();
            if(clsCommon.Instance.isServerOpen)
                clsSocket.DisConn();
            if (clsCommon.Instance.isClientConn)
                clsSocket.Client_Disconnect();

            int nIndex = 0;

            while (true)
            {
                if (QueueRecvThread != null && !QueueRecvThread.IsAlive)
                {
                    QueueRecvThread.Abort();
                    QueueRecvThread = null;
                }

                if (QueueRecvThread == null)
                {
                    break;
                }

                Thread.Sleep(1000);
                nIndex++;

                if (nIndex > 3)
                {
                    if (QueueRecvThread != null) QueueRecvThread.Abort();
                        QueueRecvThread = null;
                    break;
                }
            }
        }
    }
}
