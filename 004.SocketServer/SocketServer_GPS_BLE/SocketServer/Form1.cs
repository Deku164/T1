using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketServer.Class;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;

namespace SocketServer
{
    public partial class Form1 : Form
    {
        ITransmissionn clsTrans;

        //UI 이벤트 처리용 Timer
        private System.Windows.Forms.Timer _tmrUI;

        //UI DATA SEND 용 Timer
        private System.Windows.Forms.Timer _tmrSendData;

        //UI LOG Queue
        private Queue<string> _queLog = new Queue<string>();

        public Form1()
        {
            InitializeComponent();

            clsLOG.OnLOG_VIEW += LOG_EVENT;
            clsLOG.Initial();

            InitialTransmissionn();

            Init_Timer();

        }

        #region Trans

        public Boolean InitialTransmissionn()
        {
            try
            {
                clsTrans = new clsTransmissionn();
                clsTrans.Open();

                return true;
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
                return false;
            }
        }

        private void Init_Timer()
        {
            try
            {
                _tmrUI = new Timer
                {
                    Interval = 500
                };
                _tmrUI.Tick += _tmrUI_Tick;
                _tmrUI.Enabled = true;

                _tmrSendData = new Timer
                {
                    Interval = 2000
                };
                _tmrSendData.Tick += _tmrSendData_Tick;                
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }

        private void _tmrUI_Tick(object sender, EventArgs e)
        {
            _tmrUI.Stop();

            try
            {
                if (clsCommon.Instance.isServerOpen)
                {
                    if (this.startToolStripMenuItem.Enabled == true)
                    {
                        this.startToolStripMenuItem.Enabled = false;
                        this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Green;
                        //this.metroProgressSpinner1.Spinning = false;
                    }
                }
                else
                {
                    if (this.startToolStripMenuItem.Enabled == false)
                    {
                        this.startToolStripMenuItem.Enabled = true;
                        this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Red;
                        this.metroProgressSpinner1.Spinning = true;
                    }
                }

                if (clsCommon.Instance.isClientConn)
                {
                    if (this.startToolStripMenuItem1.Enabled == true)
                    {
                        this.startToolStripMenuItem1.Enabled = false;
                        this.groupBox2.Enabled = true;
                    }
                }
                else
                {
                    if (this.startToolStripMenuItem1.Enabled == false)
                    {
                        this.startToolStripMenuItem1.Enabled = true;
                        this.groupBox2.Enabled = false;
                    }
                }

                UI_LOG_UPDATE();

                UI_DATA_UPDATE();

                if (this.metroProgressSpinner1.Value > 95)
                    this.metroProgressSpinner1.Value = 0;
                else
                    this.metroProgressSpinner1.Value += 1;

            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
            finally
            {
                _tmrUI.Start();
            }
        }

        private void _tmrSendData_Tick(object sender, EventArgs e)
        {
            _tmrSendData.Stop();

            string data = string.Empty;
            Random r = new Random();
            int rand = 0;

            try
            {
                if (clsCommon.Instance.isClientConn)
                {
                    //이산화황 DATA = 0
                    data = "02464630315330303030303052303003";
                    sendDummyData(data);
                    data = "02464530315330303030303052303003";
                    sendDummyData(data);
                    data = "02464430315330303030303052303003";
                    sendDummyData(data);
                    //산소 Data=ppO2
                    data = "02464630324F303230302E3"+ r.Next(0,9).ToString() + "253031392E3"+ r.Next(7, 9).ToString() + "3003";
                    sendDummyData(data);
                    data = "02464530324F303230302E3" + r.Next(0, 9).ToString() + "253031392E3" + r.Next(7, 9).ToString() + "3003";
                    sendDummyData(data);
                    data = "02464430324F303230302E3" + r.Next(0, 9).ToString() + "253031392E3" + r.Next(7, 9).ToString() + "3003";
                    sendDummyData(data);
                    //이산화탄소=5
                    data = "024646303343303030303" + r.Next(3, 5).ToString() + "52303030303003";
                    sendDummyData(data);
                    data = "024645303343303030303" + r.Next(3, 5).ToString() + "52303030303003";
                    sendDummyData(data);
                    data = "024644303343303030303" + r.Next(3, 5).ToString() + "52303030303003";
                    sendDummyData(data);

                    //온도=0
                    data = "02464630355430303030483030303003";
                    sendDummyData(data);
                    data = "02464530355430303030483030303003";
                    sendDummyData(data);
                    data = "02464430355430303030483030303003";
                    sendDummyData(data);
                    //sendDummyData("02464530324F303230302E37253031392E383003");
                }
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
            finally
            {
                _tmrSendData.Start();
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// UI 출력용 LOG Event 처리 함수
        /// </summary>
        /// <param name="TYPE">LOG Type</param>
        /// <param name="DATA">DATA</param>
        private void LOG_EVENT(LOGTYPE TYPE, string DATA)
        {
            string LogMessage = string.Empty;
            string Log_Event = string.Empty;

            try
            {
                //Log_Event = ("TYPE:" + TYPE.ToString() + ", DATA:" + DATA);

                LogMessage = "[" + String.Format("{0:HH:mm:ss.fff}" + "] ", DateTime.Now) + DATA.ToString();

                _queLog.Enqueue(LogMessage);
            }
            catch (Exception ex)
            {
                clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }

        private void UI_LOG_UPDATE()
        {
            while (_queLog.Count > 0)
            {
                //sLog = _queLog.Dequeue();
                TextBoxSetColor(_queLog.Dequeue(), List_LOG);
            }
        }

        private void UI_DATA_UPDATE()
        {
            string data = string.Empty;
            //임시 재정렬 만일 SSID의 Naming 형식이 달라질 경우 수정 필요
            SortedList<int, int> slist_index = new SortedList<int, int>();
            bool isSorted = true;


            while (QueueManager.Instance.MainUISendQueueCount() > 0)
            {
                data = QueueManager.Instance.MainUISendDequeue();
                
                if(data.Length > 10)                    
                {
                    switch (data.Substring(4, 1))
                    {
                        case "B":   //0001B50:8d:6f:b0:02:11,-71

                            #region "case B"

                            #region "Data Set"
                            data = data.Substring(5);

                            string mac = data.Split(',')[0];
                            string rssi = data.Split(',')[1];
                            bool flag = false;

                            foreach (MetroFramework.Controls.MetroLabel temp in flp_BLE_MAC.Controls)
                            {
                                if (temp.Tag == null)
                                {
                                    temp.Tag = mac.ToString();
                                    temp.Text = mac.ToString();
                                }

                                if (temp.Tag != null && temp.Tag.ToString() == mac)
                                {
                                    foreach (MetroFramework.Controls.MetroLabel temp2 in flp_BLE_RSSI.Controls)
                                    {
                                        if (temp2.Tag == null)
                                        {
                                            temp2.Tag = mac.ToString();
                                        }

                                        if (temp2.Tag.ToString() == mac)
                                        {
                                            temp2.Text = rssi;
                                            flag = true;
                                            break;
                                        }
                                    }

                                    if (flag)
                                    {
                                        break;
                                    }
                                }
                            }

                            if (flag == false)
                            {
                                MetroFramework.Controls.MetroLabel mlabel = new MetroFramework.Controls.MetroLabel
                                {
                                    Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
                                            | AnchorStyles.Right),
                                    BorderStyle = BorderStyle.FixedSingle,
                                    FontSize = MetroFramework.MetroLabelSize.Tall,
                                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                                    Location = new Point(3, 0),
                                    Name = "",
                                    Size = new Size(186, 49),
                                    Style = MetroFramework.MetroColorStyle.Orange,
                                    TabIndex = 4,
                                    Text = mac.ToString(),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Theme = MetroFramework.MetroThemeStyle.Light,
                                    UseStyleColors = true,
                                    Tag = mac.ToString()
                                };

                                flp_BLE_MAC.Controls.Add(mlabel);

                                MetroFramework.Controls.MetroLabel mlabel2 = new MetroFramework.Controls.MetroLabel
                                {
                                    Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
                                                 | AnchorStyles.Right),
                                    BorderStyle = BorderStyle.FixedSingle,
                                    FontSize = MetroFramework.MetroLabelSize.Tall,
                                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                                    Location = new Point(3, 0),
                                    Name = "",
                                    Size = new Size(186, 49),
                                    Style = MetroFramework.MetroColorStyle.Orange,
                                    TabIndex = 4,
                                    Text = rssi.ToString(),
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Theme = MetroFramework.MetroThemeStyle.Light,
                                    UseStyleColors = true,
                                    Tag = mac.ToString()
                                };

                                flp_BLE_RSSI.Controls.Add(mlabel2);
                            }

                            #endregion

                            #region "sort"
                            //임시 재정렬 만일 MAC Addr의 주소가 가장 뒷자리만 바뀌는 것이 아닐 경우 수정 필요

                            do
                            {
                                slist_index.Clear();
                                isSorted = true;

                                for (int nloop = 0; nloop < this.flp_BLE_MAC.Controls.Count; ++nloop)
                                {
                                    slist_index.Add(nloop, int.Parse(flp_BLE_MAC.Controls[nloop].Text.Split(':')[5], System.Globalization.NumberStyles.HexNumber));
                                }

                                for (int nloop = 0; nloop < slist_index.Count - 1; ++nloop)
                                {

                                    if (slist_index[nloop] > slist_index[nloop + 1])
                                    {
                                        Control ssidControl = flp_BLE_MAC.Controls[nloop];
                                        Control rssiControl = flp_BLE_RSSI.Controls[nloop];

                                        flp_BLE_MAC.Controls.SetChildIndex(ssidControl, nloop + 1);
                                        flp_BLE_RSSI.Controls.SetChildIndex(rssiControl, nloop + 1);

                                        flp_BLE_MAC.Invalidate();
                                        flp_BLE_RSSI.Invalidate();

                                        isSorted = false;
                                        break;
                                    }
                                    isSorted = true;
                                }

                            } while (!isSorted);
                            #endregion

                            #endregion

                            break;

                        case "G":   //0001GN035.1766E129.1260

                            data = data.Substring(5);
                            this.mlbl_GPS_NS.Text = data.Substring(0, 9);
                            this.mlbl_GPS_EW.Text = data.Substring(9, 9);

                            break;

                        case "W":  //0001WBPN_1,-55/BPN_2,-24

                            #region "case W"

                            #region "Data Set"
                            data = data.Substring(5);
                            string[] arr_data = data.Split('/');

                            foreach (string stemp in arr_data)
                            {
                                string ssid = stemp.Split(',')[0];
                                string rssi_2 = stemp.Split(',')[1];
                                bool flag_2 = false;

                                foreach (MetroFramework.Controls.MetroLabel temp in flp_WIFI_SSID.Controls)
                                {
                                    if (temp.Tag == null)
                                    {
                                        temp.Tag = ssid.ToString();
                                        temp.Text = ssid.ToString();
                                    }

                                    if (temp.Tag != null && temp.Tag.ToString() == ssid)
                                    {
                                        foreach (MetroFramework.Controls.MetroLabel temp2 in flp_WIFI_RSSI.Controls)
                                        {
                                            if (temp2.Tag == null)
                                            {
                                                temp2.Tag = ssid.ToString();
                                            }

                                            if (temp2.Tag.ToString() == ssid)
                                            {
                                                temp2.Text = rssi_2;
                                                flag_2 = true;
                                                break;
                                            }
                                        }

                                        if (flag_2)
                                        {
                                            break;
                                        }
                                    }
                                }

                                if (flag_2 == false)
                                {
                                    MetroFramework.Controls.MetroLabel mlabel = new MetroFramework.Controls.MetroLabel
                                    {
                                        Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
                                                | AnchorStyles.Right),
                                        BorderStyle = BorderStyle.FixedSingle,
                                        FontSize = MetroFramework.MetroLabelSize.Tall,
                                        FontWeight = MetroFramework.MetroLabelWeight.Bold,
                                        Location = new Point(3, 0),
                                        Name = "",
                                        Size = new Size(186, 49),
                                        Style = MetroFramework.MetroColorStyle.Black,
                                        TabIndex = 4,
                                        Text = ssid.ToString(),
                                        TextAlign = ContentAlignment.MiddleCenter,
                                        Theme = MetroFramework.MetroThemeStyle.Light,
                                        UseStyleColors = true,
                                        Tag = ssid.ToString()
                                    };

                                    flp_WIFI_SSID.Controls.Add(mlabel);

                                    MetroFramework.Controls.MetroLabel mlabel2 = new MetroFramework.Controls.MetroLabel
                                    {
                                        Anchor = ((AnchorStyles.Top | AnchorStyles.Left)
                                                     | AnchorStyles.Right),
                                        BorderStyle = BorderStyle.FixedSingle,
                                        FontSize = MetroFramework.MetroLabelSize.Tall,
                                        FontWeight = MetroFramework.MetroLabelWeight.Bold,
                                        Location = new Point(3, 0),
                                        Name = "",
                                        Size = new Size(186, 49),
                                        Style = MetroFramework.MetroColorStyle.Black,
                                        TabIndex = 4,
                                        Text = rssi_2.ToString(),
                                        TextAlign = ContentAlignment.MiddleCenter,
                                        Theme = MetroFramework.MetroThemeStyle.Light,
                                        UseStyleColors = true,
                                        Tag = ssid.ToString()
                                    };

                                    flp_WIFI_RSSI.Controls.Add(mlabel2);
                                }
                            }

                            #endregion

                            #region "sort"                            

                            do
                            {
                                slist_index.Clear();
                                isSorted = true;

                                for (int nloop = 0; nloop < this.flp_WIFI_SSID.Controls.Count; ++nloop)
                                {
                                    slist_index.Add(nloop, int.Parse(flp_WIFI_SSID.Controls[nloop].Text.Split('_')[1]));
                                }

                                for (int nloop = 0; nloop < slist_index.Count - 1; ++nloop)
                                {

                                    if (slist_index[nloop] > slist_index[nloop + 1])
                                    {
                                        Control ssidControl = flp_WIFI_SSID.Controls[nloop];
                                        Control rssiControl = flp_WIFI_RSSI.Controls[nloop];

                                        flp_WIFI_SSID.Controls.SetChildIndex(ssidControl, nloop + 1);
                                        flp_WIFI_RSSI.Controls.SetChildIndex(rssiControl, nloop + 1);

                                        flp_WIFI_SSID.Invalidate();
                                        flp_WIFI_RSSI.Invalidate();

                                        isSorted = false;
                                        break;
                                    }
                                    isSorted = true;
                                }

                            } while (!isSorted);

                            #endregion

                            #endregion
                            break;
                    }

                    if (this.metroProgressSpinner1.Value > 95)
                        this.metroProgressSpinner1.Value = 0;
                    else
                        this.metroProgressSpinner1.Value += 5;

                }
            }
        }

        /// <summary>
        /// 텍스트박스의 라인이상의 단어를 쪼개서 단어로 만들어 WordCheak로 보내어 체크한다.
        /// </summary>
        /// <param name="SelLine"></param>
        private void TextBoxSetColor(string SelLine, RichTextBox Target)
        {
            try
            {
                Target.SelectionStart = Target.Text.Length;//맨 마지막 선택...
                Target.ScrollToCaret();

                if (Target.Lines.Length > 300)
                {
                    Target.Text = string.Empty;
                }

                // for (int Linenum = 0; Linenum < Line.Length; Linenum++)
                //{
                if (SelLine.Contains("SYSTEM") || SelLine.ToUpper().Contains("PORT") || SelLine.ToUpper().Contains("PASSIVE"))
                {
                    Target.SelectionColor = Color.Green;
                    Target.SelectedText = SelLine + Environment.NewLine;
                }
                else
                {
                    // 잘라낸 줄단위를 다시 for문으로 단어별로 쪼갠다
                    string[] Word = SelLine.Split(new char[] { ' ' });
                    for (int Wordnum = 0; Wordnum < Word.Length; Wordnum++)
                    {
                        //만약 해당 단어가 true면 파란색을 적용한다
                        if (WordCheakBlue(Word[Wordnum]) == true)
                        {
                            Target.SelectionColor = Color.Blue;

                            if (Wordnum != 0)
                                Target.SelectedText = " " + Word[Wordnum];
                            else
                                Target.SelectedText = Word[Wordnum];
                        }
                        else if (WordCheakRed(Word[Wordnum]) == true)
                        {
                            Target.SelectionColor = Color.Red;

                            if (Wordnum != 0)
                                Target.SelectedText = " " + Word[Wordnum];
                            else
                                Target.SelectedText = Word[Wordnum];
                        }
                        else
                        {
                            Target.SelectionColor = Color.Black;

                            if (Wordnum != 0)
                                Target.SelectedText = " " + Word[Wordnum];
                            else
                                Target.SelectedText = Word[Wordnum];
                        }
                    }

                    Target.SelectedText = Environment.NewLine;

                }

                Target.SelectionStart = List_LOG.Text.Length;     //맨 마지막 선택...
                Target.ScrollToCaret();
            }
            catch (Exception ex)
            {
                //clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }

        /// <summary>
        /// 텍스트박스의 라인이상의 단어를 쪼개서 단어로 만들어 WordCheak로 보내어 체크한다.
        /// </summary>
        /// <param name="SelLine"></param>
        private void TextBoxSetColor(string SelLine, ListBox Target)
        {
            try
            {
                int num = Target.Text.Length;

                if (Target.Items.Count > 100)
                {
                    Target.Items.RemoveAt(0);
                }

                Target.Items.Add(SelLine);
                int index = Target.Items.Count;
                Target.SelectedIndex = index - 1;

            }
            catch (Exception ex)
            {
                //Kenji 180227 로그 무한으로 남는듯.. 어짜피 디스플레이 오류니까 여기꺼는 안하는걸로
                //clsLOG.SetLOG(LOGTYPE.SYSTEMERROR, ex.ToString());
            }
        }

        // 단어가 파란색으로 변경 되어야할지를 판단한다
        private bool WordCheakBlue(string str)
        {
            string[] tmp = { "[SEND->>]", "SEND" };
            for (int n = 0; n < tmp.Length; n++)
            {
                if (str == tmp[n])
                {
                    return true;
                }
            }
            return false;
        }

        // 단어가 빨간색으로 변경 되어야할지를 판단한다
        private bool WordCheakRed(string str)
        {
            string[] tmp = { "[<<-RECV]", "RECV" };
            for (int n = 0; n < tmp.Length; n++)
            {
                if (str == tmp[n])
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            clsTrans.Server_Open();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _tmrSendData.Enabled = false;
            clsTrans.Close();
        }

        private void btnClientConn_Click(object sender, EventArgs e)
        {
            string Server_IP = string.Empty;
            int Port = 8080;
            string[] arrData;

            if (lbl_ServerIP_Port.Text != string.Empty)
            {
                try
                {
                    arrData = lbl_ServerIP_Port.Text.Split(':');

                    if (int.TryParse(arrData[1], out Port))
                    {
                        clsTrans.Client_Open(arrData[0], Port);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Client 연결 실패\r\n" + ex.Message);
                }
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsCommon.Instance.ProgramRUN = false;

            if (clsCommon.Instance.isServerOpen || clsCommon.Instance.isClientConn)
                clsTrans.Close();

            clsLOG.Close();

            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsCommon.Instance.isClientConn)
            {

                _tmrSendData.Enabled = true;
                //clsMsg tempMsg = new clsMsg();
                //tempMsg.CLIENT_IP = clsCommon.Instance.ServerIP;
                //tempMsg.Port = clsCommon.Instance.SERVER_PORT;
                //tempMsg.S_DATA = this.textBox1.Text;
                //QueueManager.Instance.SetTransmit(tempMsg);
            }
        }

        private void sendDummyData(string data)
        {
            clsMsg tempMsg = new clsMsg
            {
                CLIENT_IP = clsCommon.Instance.ServerIP,
                Port = clsCommon.Instance.SERVER_PORT,
                S_DATA = data
            };
            QueueManager.Instance.SetTransmit(tempMsg);
        }

        private void btn_message_Send_Click(object sender, EventArgs e)
        {
            if (clsCommon.Instance.isClientConn)
            {
                sendDummyData(this.textBox1.Text);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mbtn_logView_Click(object sender, EventArgs e)
        {
            if(this.Size == MaximumSize)
                this.Size = MinimumSize;
            else
                this.Size = MaximumSize;
        }
    }
}
