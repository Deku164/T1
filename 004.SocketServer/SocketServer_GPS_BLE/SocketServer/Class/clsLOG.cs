using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Configuration;

namespace SocketServer.Class
{
    #region Enum

    public enum LOGTYPE
    {
        Message = 1,
        SYSTEM = 2,
        SYSTEMERROR = 3,
    }

    #endregion
    
    public static class clsLOG
    {
        public delegate void LOG_UIVIEW_Event(LOGTYPE TYPE, string DATA);
        static public event LOG_UIVIEW_Event OnLOG_VIEW = null;

        static private int keepDay;
        static private string LOG_PATH = @"C:\BPNS\LOG\";
        static private bool bLOG_Thread = true;
        static private Queue Queue_LOG = new Queue();
        static private Thread threadWrite;
        static private clsLOG_PATH PATH;


        /// <summary>
        /// 각 LOG TYPE 에 맞게 LOG 경로 를 지정해주는 함수
        /// </summary>
        /// <param name="_Path">LOG Base Path경로</param>
        /// <param name="TYPE">사용할 LOG를 Type별로 배열로 받아서 처리</param>
        /// <returns>LOG Path Setting 성공 여부</returns>
        static private bool PATH_SETTING(string _Path, string[] TYPE)
        {
            try
            {
                PATH.LOG = _Path;
                PATH.LOG_DATE = PATH.LOG + String.Format("{0:yyyy-MM-dd}", DateTime.Now).Trim();

                if (TYPE == null)
                {
                    TYPE = PATH.LOG_TYPE_PATH.Select(C => C.Key).ToArray(); //List to Array [기존Data 활용]
                    PATH.LOG_TYPE_PATH.Clear();
                }

                if (TYPE != null)
                {
                    foreach (string sTemp in TYPE)
                    {
                        PATH.LOG_TYPE_PATH.Add(sTemp, PATH.LOG_DATE + @"\" + sTemp + @"\");
                    }
                }

                //해당 경로의 폴더를 생성 한다.
                MakeFolder();

                return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 각 LOG TYPE 에 맞게 LOG 경로 를 지정해주는 함수(NotMakeFile_LOG 처리용)
        /// </summary>
        /// <param name="_Path">LOG Base Path경로</param>
        /// <param name="TYPE">사용할 LOG를 Type별로 배열로 받아서 처리</param>
        /// <param name="NotMakeFile_LOG">File 처리를 원하지 않는 LOGTYPE</param>
        /// <returns></returns>
        static private bool PATH_SETTING(string _Path, string[] TYPE, LOGTYPE[] NotMakeFile_LOG)
        {
            try
            {
                PATH.LOG = _Path;
                PATH.LOG_DATE = PATH.LOG + String.Format("{0:yyyy-MM-dd}", DateTime.Now).Trim();
                string Temp;

                if (TYPE != null)
                {
                    foreach (string sTemp in TYPE)
                    {
                        Temp = NotMakeFile_LOG.Where(c => c.ToString().CompareTo(sTemp) == 0).FirstOrDefault().ToString();

                        if (Temp == "0")
                        {
                            PATH.LOG_TYPE_PATH.Add(sTemp, PATH.LOG_DATE + @"\" + sTemp + @"\");
                        }
                        else
                        {

                            PATH.LOG_TYPE_PATH.Add(sTemp, null);    //Value 자리를 Null하여 File 생성을 하지 않는다.                        
                        }

                    }
                }

                //해당 경로의 폴더를 생성 한다.
                MakeFolder();

                return true;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// PATH_SETTING 에서 Setting 된 경로 대로 실제로 폴더를 생성 하는 함수
        /// </summary>
        static private void MakeFolder()
        {
            //현재 날짜 폴더 명
            string _Temp = PATH.LOG + String.Format("{0:yyyy-MM-dd}", DateTime.Now).Trim();

            try
            {
                if (Directory.Exists(PATH.LOG) == false)
                {
                    Directory.CreateDirectory(PATH.LOG);
                }

                if (string.Equals(_Temp, PATH.LOG_DATE) == false)
                {
                    PATH_SETTING(PATH.LOG, null);
                }

                if (Directory.Exists(PATH.LOG_DATE) == false)
                {
                    Directory.CreateDirectory(PATH.LOG_DATE);
                    Folder_Delete();
                }

                if (Directory.Exists(PATH.LOG_DATE) == false)
                {
                    Directory.CreateDirectory(PATH.LOG_DATE);
                    Folder_Delete();
                }

                foreach (KeyValuePair<string, string> kvp in PATH.LOG_TYPE_PATH)
                {
                    if (kvp.Value != null)
                    {
                        if (Directory.Exists(kvp.Value) == false)
                        {
                            Directory.CreateDirectory(kvp.Value);
                        }
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 'KeepDay' 에 맞게 LOG 폴더를 삭제 하는 함수
        /// </summary>
        static private void Folder_Delete()
        {
            string[] Folder;
            double Distance = 0;
            string strDate;
            try
            {
                Folder = Directory.GetDirectories(PATH.LOG);

                foreach (string dir in Folder)
                {
                    strDate = dir.Substring(dir.Length - 10, 10);
                    Distance = DateTime.Now.Subtract(Convert.ToDateTime(strDate)).TotalDays;
                    if (Distance > keepDay)
                    {
                        Directory.Delete(dir, true);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 실제로 LOG를 File Write 를 처리하는 Thread 함수
        /// </summary>
        static private void threadLOGFileWrite()
        {
            string _DirPath = string.Empty;
            string _FilePath = string.Empty;
            string Trace_FilePath = string.Empty;

            clsLOGMsg clsMSG = null;

            while (bLOG_Thread == true)
            {
                try
                {

                    clsMSG = null;

                    lock (Queue_LOG.SyncRoot)
                    {
                        if (Queue_LOG.Count > 0)
                        {
                            clsMSG = (clsLOGMsg)Queue_LOG.Dequeue();
                        }
                    }


                    if (clsMSG != null)
                    {
                        _FilePath = String.Format("{0:HH}", DateTime.Now) + ".log";
                        MakeFolder();//경로 유무 확인

                        _FilePath = clsMSG.LogType + "_" + _FilePath;
                        _DirPath = PATH.LOG_TYPE_PATH.Where(C => C.Key == clsMSG.LogType).FirstOrDefault().Value;

                        if (_DirPath != null)
                        {
                            //해당 경로에 파일 존재 유무 확인
                            if (File.Exists(_DirPath + _FilePath) == true)
                            {
                                using (StreamWriter sw = new StreamWriter(_DirPath + _FilePath, true))
                                {
                                    sw.WriteLine(clsMSG.EventTime + "\t" + clsMSG.LOGDATA);
                                    sw.Close();
                                }
                            }
                            else
                            {
                                using (StreamWriter sw = File.CreateText(_DirPath + _FilePath))
                                {
                                    sw.WriteLine(clsMSG.EventTime + "\t" + clsMSG.LOGDATA);
                                    sw.Close();
                                }
                            }
                        }

                    }

                    //Application.DoEvents();
                    Thread.Sleep(10);

                }
                catch (ThreadAbortException)
                {

                }
                catch { }
            }
        }

        /// <summary>
        /// LOG를 기록하기 위한 함수
        /// </summary>
        /// <param name="_LOGTYPE">LOGTYPE[ENUM]</param>
        /// <param name="_DATA">LOG VALUE</param>
        /// <param name="_ONLY_UI">Log Display 처리 유무</param>
        /// <param name="_FILE_WRITE">File Write 유무</param>
        static public void SetLOG(LOGTYPE _LOGTYPE, string _DATA, bool _ONLY_UI = true, bool _FILE_WRITE = true)
        {
            clsLOGMsg clsLOGMsg = new clsLOGMsg
            {
                LogType = _LOGTYPE.ToString(),
                EventTime = "[" + String.Format("{0:HH:mm:ss.fff}" + "]\t", DateTime.Now).Trim(),
                LOGDATA = _DATA.Trim()
            };

            try
            {
                if (_FILE_WRITE)
                {
                    //File Write Queue 에 적재
                    lock (Queue_LOG.SyncRoot)
                    {
                        Queue_LOG.Enqueue(clsLOGMsg);    //LOG File Save 용
                    }
                }

                if (_ONLY_UI)
                {
                    if (OnLOG_VIEW != null)
                    {
                        OnLOG_VIEW(_LOGTYPE, _DATA);
                    }
                }

                clsLOGMsg = null;
            }
            catch
            {
            }
        }

        /// <summary>
        /// LOG BASE PATH 를 재설정 할때 사용
        /// </summary>
        /// <param name="_PATH">BASE PATH</param>
        static public void Set_LOGPATH(string _PATH)
        {
            PATH_SETTING(_PATH, null);
        }

        /// <summary>
        /// LOG Keep Day를 수정하는 함수
        /// </summary>
        /// <param name="_Day"></param>
        static public void Set_KeepDay(int _Day)
        {
            try
            {
                keepDay = _Day;

                Folder_Delete();
            }
            catch { }
        }

        /// <summary>
        /// LOG 기능을 초기화 하는 함수
        /// </summary>
        /// <returns>초기화 성공 여부</returns>
        static public bool Initial()
        {
            try
            {
                string[] TYPE = System.Enum.GetNames(typeof(LOGTYPE));
                bool bRet = false;
                PATH = new clsLOG_PATH();
                keepDay = Convert.ToInt32(clsConfig.ReadAppConfig("KEEP_DAY"));
                LOG_PATH = clsConfig.ReadAppConfig("MGT_LOG_FILE_PATH");

                bRet = PATH_SETTING(LOG_PATH, TYPE);

                if (bRet == true)
                {
                    threadWrite = new Thread(new ThreadStart(threadLOGFileWrite));
                    threadWrite.SetApartmentState(ApartmentState.STA);
                    threadWrite.Name = "ThreadLOGFileWrite";
                    threadWrite.Start();
                }

                return bRet;
            }
            catch
            { return false; }
        }


        /// <summary>
        /// File 처리를 원하지 않는 LOTTYPE 인자를 받아서 처리 하는 Initial 함수
        /// </summary>
        /// <param name="NotMakeFile_LOG"></param>
        /// <returns>초기화 성공 여부</returns>
        static public bool Initial(params LOGTYPE[] NotMakeFile_LOG)
        {
            try
            {
                string[] TYPE = System.Enum.GetNames(typeof(LOGTYPE));
                bool bRet = false;
                PATH = new clsLOG_PATH();
                keepDay = Convert.ToInt32(clsConfig.ReadAppConfig("KEEP_DAY"));
                LOG_PATH = clsConfig.ReadAppConfig("MGT_LOG_FILE_PATH");

                bRet = PATH_SETTING(LOG_PATH, TYPE, NotMakeFile_LOG);

                if (bRet == true)
                {
                    threadWrite = new Thread(new ThreadStart(threadLOGFileWrite));
                    threadWrite.SetApartmentState(ApartmentState.STA);
                    threadWrite.Name = "ThreadLOGFileWrite";
                    threadWrite.Start();
                }

                return bRet;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// LOG 처리 기능을 중지 하는함수
        /// </summary>
        /// <returns>LOG 기능 Close 성공 여부</returns>
        static public bool Close()
        {
            try
            {
                bLOG_Thread = false;
                while (true)
                {
                    if (threadWrite != null && !threadWrite.IsAlive)
                    {
                        threadWrite.Abort();
                        threadWrite = null;
                    }

                    if (threadWrite == null) break;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    class clsLOGMsg
    {
        private string _LogType = string.Empty;

        private string _EventTime = string.Empty;

        private string _LOGDATA = string.Empty;

        #region Property
        public string LogType
        {
            get { return _LogType; }
            set { _LogType = value; }
        }
        public string EventTime
        {
            get { return _EventTime; }
            set { _EventTime = value; }
        }
        public string LOGDATA
        {
            get { return _LOGDATA; }
            set { _LOGDATA = value; }
        }
        #endregion
    }

    class clsLOG_PATH
    {
        private string _LOG = string.Empty;

        private string _LOG_DATE = string.Empty;

        private SortedList<string, string> _LOG_TYPE_PATH = new SortedList<string, string>();

        #region Property

        public string LOG
        {
            get { return _LOG; }
            set { _LOG = value; }
        }

        public string LOG_DATE
        {
            get { return _LOG_DATE; }
            set { _LOG_DATE = value; }
        }

        public SortedList<string, string> LOG_TYPE_PATH
        {
            get { return _LOG_TYPE_PATH; }
            set { _LOG_TYPE_PATH = value; }
        }

        #endregion

    }
}
