using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer.Class
{
    public delegate void DisconnectedDelegate(object sender);
    public delegate void ReceiveDataDelegate(string IP, int port, object data, byte[] originalMsg);


    public class clsClientService
    {
        public event DisconnectedDelegate OnDisconnected;
        public event ReceiveDataDelegate OnReceiveData;


        NetworkStream networkStream;

        /// <summary>
        /// 범용 메세지큐
        /// </summary>
        Queue<clsMsg> Uqueue = new Queue<clsMsg>();

        public Queue<clsMsg> UQueue
        {
            get { return Uqueue; }
            set { Uqueue = value; }
        }

        Queue<byte[]> _ReciveQueue = new Queue<byte[]>();

        public Queue<byte[]> ReciveQueue
        {
            get { return _ReciveQueue; }
            set { _ReciveQueue = value; }
        }


        TcpClient clientSocket;
        string _IP;

        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }

        int _Port;

        public int PORT
        {
            get { return _Port; }
            set { _Port = value; }
        }

        bool connected;

        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        string command;

        public string Command
        {
            get { return command; }
            set { command = value; }
        }


        public void Close()
        {
            this.clientSocket.Close();
            this.thProcess.Abort();
        }



        public void StartService(TcpClient inClientSocket, string clineNo, int Port)
        {
            this.clientSocket = inClientSocket;
            this.IP = clineNo;
            this.PORT = Port;

            //ASync
            //We are connected successfully.
            networkStream = clientSocket.GetStream();
            byte[] buffer = new byte[clientSocket.ReceiveBufferSize];
            //Now we are connected start asyn read operation.
            networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);


            thProcess = new Thread(PollCheck);
            thProcess.Start();

            this.connected = true;
        }

        // String을 바이트 배열로 변환 
        private byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str); return StrByte;
        }



        Thread thProcess;
        void PollCheck()
        {
            clsMsg UMSG = null;

            while (true)
            {

                if (clientSocket.Connected)
                {
                    try
                    {
                        UMSG = null;
                        
                        //송신부
                        if (Uqueue.Count > 0)
                        {
                            UMSG = Uqueue.Dequeue();
                            byte[] CmdTemp = null;
                            int nbufCnt = 0;

                            if (UMSG != null)
                            {
                                //CmdTemp = StringToByte(UMSG.S_DATA + "\n\0");
                                CmdTemp = Encoding.UTF8.GetBytes(UMSG.S_DATA);
                                nbufCnt = Encoding.UTF8.GetByteCount(UMSG.S_DATA);
                            }
                            else continue;

                            Thread.Sleep(100);
                            networkStream.Write(CmdTemp, 0, nbufCnt);

                            clsLOG.SetLOG(LOGTYPE.Message, string.Format(" [SEND->>] :{0}\t{1}", UMSG.CLIENT_IP, UMSG.S_DATA));
                        }

                        //수신부
                        if (ReciveQueue.Count > 0)
                        {
                            // GPS / BLE Data
                            // 이번에는 구분자가 STX, ETX 재대로 들어오니까 수정.
                            byte[] data = ReciveQueue.Dequeue();

                            string sdata = Encoding.ASCII.GetString(data).Trim('\0');

                            string[] arr_sdata = sdata.Split(new char[] { '\u0002', '\u0003' },
                                 StringSplitOptions.RemoveEmptyEntries);

                            foreach(string temp in arr_sdata)
                            {
                                OnReceiveData(IP, PORT, temp, data);
                            }


                            //int nStartIdx = -1;
                            //int nEndIdx = -1;

                            //byte[] data = ReciveQueue.Dequeue();

                            //string sdata = Encoding.ASCII.GetString(data).Trim('\0');

                            //if (OnReceiveData != null)
                            //{
                            //    for(int nloop = 0; nloop < sdata.Length; nloop+=2)
                            //    {
                            //        if(sdata[nloop].ToString() + sdata[nloop + 1].ToString() == "02")
                            //        {
                            //            nStartIdx = nloop;
                            //        }
                            //        if (sdata[nloop].ToString() + sdata[nloop + 1].ToString() == "03")
                            //        {
                            //            nEndIdx = nloop + 1;
                            //        }

                            //        if(nStartIdx != -1 && nEndIdx != -1)
                            //        {
                            //            OnReceiveData(IP, PORT, sdata.Substring(nStartIdx, (nEndIdx + 1) - nStartIdx), data);
                            //            nStartIdx = -1;
                            //            nEndIdx = -1;
                            //        }
                            //    }

                            //string[] msgCode = sdata.Split(new string[] { "02", "03" }, StringSplitOptions.RemoveEmptyEntries);

                            //for (int nloop = 0; nloop < msgCode.Length; ++nloop)
                            //{
                            //    msgCode[nloop] = msgCode[nloop].Replace("-", "");
                            //    if (msgCode[nloop] != string.Empty)
                            //    {
                            //        if (msgCode[nloop].Length > 10)
                            //        {
                            //            OnReceiveData(IP, PORT, msgCode[nloop], data);
                            //        }
                            //    }
                            //}                                

                            _recvBuffer.Clear();
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + ex.StackTrace);
                    }
                }
                else
                {
                    connected = false;
                    break;
                }
                Thread.Sleep(150);
            }

            connected = false;

            clientSocket.Close();

            if (OnDisconnected != null) OnDisconnected(this);
        }

        /// <summary>
        /// Byte 배열의 공백 제거
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }


        List<byte> _recvBuffer = new List<byte>();

        private void ReadCallback(IAsyncResult result)
        {
            int read;
            NetworkStream networkStream;
            try
            {
                networkStream = clientSocket.GetStream();
                read = networkStream.EndRead(result);
            }
            catch(Exception ex)
            {
                //clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
                //An error has occured when reading...
                clientSocket.Close();
                if (OnDisconnected != null) OnDisconnected(this);
                return;
            }

            if (read == 0)
            {
                //The connection has been closed.
                clientSocket.Close();

                if (OnDisconnected != null) OnDisconnected(this);

                return;
            }

            byte[] buffer = result.AsyncState as byte[];
            byte[] ResizedBuffer = TrimEnd(buffer);
            //string sdata = Encoding.ASCII.GetString(buffer).Trim('\0');
            _recvBuffer.AddRange(ResizedBuffer);
            
            byte[] data = new byte[_recvBuffer.Count];
            _recvBuffer.CopyTo(0, data, 0, _recvBuffer.Count);

            ReciveQueue.Enqueue(data);

            buffer = new byte[clientSocket.ReceiveBufferSize];

            networkStream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
        }

        /// <summary>
        /// Writes a string to the network using the defualt encoding.
        /// </summary>
        /// <param name="data">The string to write</param>
        /// <returns>A WaitHandle that can be used to detect
        /// when the write operation has completed.</returns>
        public void Write(string data)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data);

            Write(bytes);
        }

        /// <summary>
        /// Writes an array of bytes to the network.
        /// </summary>
        /// <param name="bytes">The array to write</param>
        /// <returns>A WaitHandle that can be used to detect
        /// when the write operation has completed.</returns>
        public void Write(byte[] bytes)
        {
            if (clientSocket == null) return;
            if (!clientSocket.Connected) return;


            NetworkStream networkStream = clientSocket.GetStream();
            //Start async write operation
            networkStream.BeginWrite(bytes, 0, bytes.Length, WriteCallback, null);
        }

        /// <summary>
        /// Callback for Write operation
        /// </summary>
        /// <param name="result">The AsyncResult object</param>
        private void WriteCallback(IAsyncResult result)
        {
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.EndWrite(result);
        }

        bool bAlive = false;

        public bool IsAlive
        {
            get { return bAlive; }
            set { bAlive = value; }
        }
    }
}
