using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Class
{
    public class QueueManager
    {
        #region Private Member

        private Queue<clsMsg> _SendQueue = new Queue<clsMsg>();
        private Queue<clsMsg> _RecvQueue = new Queue<clsMsg>();

        private Queue<string> _UISendQueue = new Queue<string>();

        #endregion

        #region Singletone Instance

        private static QueueManager _Instance = new QueueManager();

        /// <summary>
        /// 싱글톤 Instance 생성
        /// </summary>
        public static QueueManager Instance
        {
            get
            {
                return _Instance;
            }
        }

        #endregion

        #region Method

        #region 전송 큐 작업

        /// <summary>
        /// 전송할 Data를 Enqueue한다.
        /// </summary>
        public void SendEnqueue(clsMsg sendobj)
        {
            lock (_SendQueue)
            {
                _SendQueue.Enqueue(sendobj);
            }
        }

        /// <summary>
        /// SendQueue의 갯수를 가져온다.
        /// </summary>
        /// <returns>int = Queue의 갯수</returns>
        public int SendQueueCount()
        {
            return _SendQueue.Count();
        }

        /// <summary>
        /// SendQueue에서 하나씩 빼온다.
        /// </summary>
        /// <returns>clsSendXComMsg</returns>
        public clsMsg SendDequeue()
        {
            lock (_SendQueue)
            {
                if (_SendQueue.Count() > 0)
                {
                    return _SendQueue.Dequeue();
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// 전송할 Data를 큐에 넣는다.
        /// </summary>
        public void SetTransmit(clsMsg SendMsg)
        {
            try
            {
                SendEnqueue(SendMsg);
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }

        #endregion

        #region 수신 큐 작업 ( 바로 처리하는 것이 좋을 듯.. )

        /// <summary>
        /// 수신 Data를 ReceiveQueue에 넣는다.
        /// </summary>
        /// <param name="sendobj">clsSendXComMsg</param>
        public void ReceiveEnqueue(clsMsg Rcvobj)
        {
            lock (_RecvQueue)
            {
                _RecvQueue.Enqueue(Rcvobj);
            }
        }

        /// <summary>
        /// ReceiveQueue의 갯수를 가져온다.
        /// </summary>
        /// <returns>int = Queue의 갯수</returns>
        public int ReceiveQueueCount()
        {
            return _RecvQueue.Count();
        }

        /// <summary>
        /// ReceiveQueue에서 하나씩 빼온다.
        /// </summary>
        /// <returns>clsSendXComMsg</returns>
        public clsMsg ReceiveDequeue()
        {
            lock (_RecvQueue)
            {
                if (_RecvQueue.Count() > 0)
                {
                    return _RecvQueue.Dequeue();
                }
                else
                    return null;
            }
        }

        #endregion

        #region Main UI 갱신 큐 작업

        /// <summary>
        /// MainView로 전송할 Data를 큐에 넣는다.
        /// </summary>
        /// <param name="sendobj"></param>
        public void MainUISendEnqueue(string sendobj)
        {
            lock (_UISendQueue)
            {
                _UISendQueue.Enqueue(sendobj);
            }
        }

        /// <summary>
        /// MainView로 전송할 Queue의 갯수를 가져온다.
        /// </summary>
        /// <returns></returns>
        public int MainUISendQueueCount()
        {
            return _UISendQueue.Count;
        }

        /// <summary>
        /// MainView로 전송할 Class를 가져온다.
        /// </summary>
        /// <returns></returns>
        public string MainUISendDequeue()
        {
            lock (_UISendQueue)
            {
                if (MainUISendQueueCount() > 0)
                {
                    return _UISendQueue.Dequeue();
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// MainView로 전송할 Data를 큐에 넣는다.
        /// </summary>
        /// <param name="cmd">메세지 타이틀</param>
        /// <param name="args">전송할 Data</param>
        public void SetUITransmit(string message)
        {
            try
            {
                MainUISendEnqueue(message);
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }
        #endregion

        #endregion
    }
}
