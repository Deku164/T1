namespace SocketServer
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_ServerIP_Port = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.disConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.mbtn_logView = new MetroFramework.Controls.MetroButton();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.flp_WIFI_RSSI = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.flp_BLE_RSSI = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.flp_WIFI_SSID = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.flp_BLE_MAC = new System.Windows.Forms.FlowLayoutPanel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroTile6 = new MetroFramework.Controls.MetroTile();
            this.metroTile5 = new MetroFramework.Controls.MetroTile();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.mlbl_GPS_EW = new MetroFramework.Controls.MetroLabel();
            this.mlbl_GPS_NS = new MetroFramework.Controls.MetroLabel();
            this.metroTile4 = new MetroFramework.Controls.MetroTile();
            this.metroTile3 = new MetroFramework.Controls.MetroTile();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_message_Send = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.List_LOG = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.flp_WIFI_RSSI.SuspendLayout();
            this.flp_BLE_RSSI.SuspendLayout();
            this.flp_WIFI_SSID.SuspendLayout();
            this.flp_BLE_MAC.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem,
            this.serverMenuToolStripMenuItem,
            this.clientMenuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1884, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.applicationToolStripMenuItem.Text = "&Application";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // serverMenuToolStripMenuItem
            // 
            this.serverMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.serverMenuToolStripMenuItem.Name = "serverMenuToolStripMenuItem";
            this.serverMenuToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.serverMenuToolStripMenuItem.Text = "&Server Menu";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.startToolStripMenuItem.Text = "&Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem.Text = "&Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // clientMenuToolStripMenuItem
            // 
            this.clientMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.lbl_ServerIP_Port,
            this.toolStripSeparator1,
            this.startToolStripMenuItem1,
            this.disConnectToolStripMenuItem,
            this.toolStripSeparator2});
            this.clientMenuToolStripMenuItem.Name = "clientMenuToolStripMenuItem";
            this.clientMenuToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.clientMenuToolStripMenuItem.Text = "&Client Menu";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(240, 22);
            this.toolStripMenuItem1.Text = "IP:Port Setting";
            // 
            // lbl_ServerIP_Port
            // 
            this.lbl_ServerIP_Port.Name = "lbl_ServerIP_Port";
            this.lbl_ServerIP_Port.Size = new System.Drawing.Size(180, 23);
            this.lbl_ServerIP_Port.Text = "127.0.0.1:25007";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
            // 
            // startToolStripMenuItem1
            // 
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            this.startToolStripMenuItem1.Size = new System.Drawing.Size(240, 22);
            this.startToolStripMenuItem1.Text = "&Connect";
            this.startToolStripMenuItem1.Click += new System.EventHandler(this.btnClientConn_Click);
            // 
            // disConnectToolStripMenuItem
            // 
            this.disConnectToolStripMenuItem.Name = "disConnectToolStripMenuItem";
            this.disConnectToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.disConnectToolStripMenuItem.Text = "&DisConnect";
            this.disConnectToolStripMenuItem.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(237, 6);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.mbtn_logView);
            this.metroPanel1.Controls.Add(this.metroProgressSpinner1);
            this.metroPanel1.Controls.Add(this.flp_WIFI_RSSI);
            this.metroPanel1.Controls.Add(this.flp_BLE_RSSI);
            this.metroPanel1.Controls.Add(this.flp_WIFI_SSID);
            this.metroPanel1.Controls.Add(this.flp_BLE_MAC);
            this.metroPanel1.Controls.Add(this.metroTile6);
            this.metroPanel1.Controls.Add(this.metroTile5);
            this.metroPanel1.Controls.Add(this.metroTile1);
            this.metroPanel1.Controls.Add(this.metroTile2);
            this.metroPanel1.Controls.Add(this.mlbl_GPS_EW);
            this.metroPanel1.Controls.Add(this.mlbl_GPS_NS);
            this.metroPanel1.Controls.Add(this.metroTile4);
            this.metroPanel1.Controls.Add(this.metroTile3);
            this.metroPanel1.Controls.Add(this.groupBox2);
            this.metroPanel1.Controls.Add(this.List_LOG);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 24);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1884, 837);
            this.metroPanel1.TabIndex = 14;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // mbtn_logView
            // 
            this.mbtn_logView.Location = new System.Drawing.Point(1170, 0);
            this.mbtn_logView.Name = "mbtn_logView";
            this.mbtn_logView.Size = new System.Drawing.Size(23, 834);
            this.mbtn_logView.TabIndex = 34;
            this.mbtn_logView.Text = "<>";
            this.mbtn_logView.Click += new System.EventHandler(this.mbtn_logView_Click);
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(697, 14);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(50, 50);
            this.metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroProgressSpinner1.TabIndex = 33;
            this.metroProgressSpinner1.Value = 100;
            // 
            // flp_WIFI_RSSI
            // 
            this.flp_WIFI_RSSI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flp_WIFI_RSSI.BackColor = System.Drawing.SystemColors.Window;
            this.flp_WIFI_RSSI.Controls.Add(this.metroLabel6);
            this.flp_WIFI_RSSI.Location = new System.Drawing.Point(579, 208);
            this.flp_WIFI_RSSI.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.flp_WIFI_RSSI.Name = "flp_WIFI_RSSI";
            this.flp_WIFI_RSSI.Size = new System.Drawing.Size(189, 617);
            this.flp_WIFI_RSSI.TabIndex = 31;
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel6.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel6.Location = new System.Drawing.Point(3, 0);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(186, 49);
            this.metroLabel6.Style = MetroFramework.MetroColorStyle.Black;
            this.metroLabel6.TabIndex = 4;
            this.metroLabel6.Text = "value";
            this.metroLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel6.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel6.UseStyleColors = true;
            // 
            // flp_BLE_RSSI
            // 
            this.flp_BLE_RSSI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flp_BLE_RSSI.BackColor = System.Drawing.SystemColors.Window;
            this.flp_BLE_RSSI.Controls.Add(this.metroLabel2);
            this.flp_BLE_RSSI.Location = new System.Drawing.Point(195, 208);
            this.flp_BLE_RSSI.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.flp_BLE_RSSI.Name = "flp_BLE_RSSI";
            this.flp_BLE_RSSI.Size = new System.Drawing.Size(189, 617);
            this.flp_BLE_RSSI.TabIndex = 31;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(3, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(186, 49);
            this.metroLabel2.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "value";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel2.UseStyleColors = true;
            // 
            // flp_WIFI_SSID
            // 
            this.flp_WIFI_SSID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flp_WIFI_SSID.BackColor = System.Drawing.SystemColors.Window;
            this.flp_WIFI_SSID.Controls.Add(this.metroLabel5);
            this.flp_WIFI_SSID.Location = new System.Drawing.Point(390, 208);
            this.flp_WIFI_SSID.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.flp_WIFI_SSID.Name = "flp_WIFI_SSID";
            this.flp_WIFI_SSID.Size = new System.Drawing.Size(189, 617);
            this.flp_WIFI_SSID.TabIndex = 32;
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel5.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel5.Location = new System.Drawing.Point(3, 0);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(186, 49);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Black;
            this.metroLabel5.TabIndex = 4;
            this.metroLabel5.Text = "value";
            this.metroLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel5.UseStyleColors = true;
            // 
            // flp_BLE_MAC
            // 
            this.flp_BLE_MAC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flp_BLE_MAC.BackColor = System.Drawing.SystemColors.Window;
            this.flp_BLE_MAC.Controls.Add(this.metroLabel1);
            this.flp_BLE_MAC.Location = new System.Drawing.Point(6, 208);
            this.flp_BLE_MAC.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.flp_BLE_MAC.Name = "flp_BLE_MAC";
            this.flp_BLE_MAC.Size = new System.Drawing.Size(189, 617);
            this.flp_BLE_MAC.TabIndex = 32;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(186, 49);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Orange;
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "value";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroLabel1.UseStyleColors = true;
            // 
            // metroTile6
            // 
            this.metroTile6.Location = new System.Drawing.Point(390, 70);
            this.metroTile6.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.metroTile6.Name = "metroTile6";
            this.metroTile6.Size = new System.Drawing.Size(189, 132);
            this.metroTile6.Style = MetroFramework.MetroColorStyle.Brown;
            this.metroTile6.TabIndex = 25;
            this.metroTile6.Text = "< Wi-Fi >  \r\nSSID";
            this.metroTile6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTile6.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile6.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // metroTile5
            // 
            this.metroTile5.Location = new System.Drawing.Point(579, 70);
            this.metroTile5.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.metroTile5.Name = "metroTile5";
            this.metroTile5.Size = new System.Drawing.Size(189, 132);
            this.metroTile5.Style = MetroFramework.MetroColorStyle.Brown;
            this.metroTile5.TabIndex = 26;
            this.metroTile5.Text = "< Wi-Fi > \r\nRSSI";
            this.metroTile5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTile5.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile5.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // metroTile1
            // 
            this.metroTile1.Location = new System.Drawing.Point(6, 70);
            this.metroTile1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(189, 132);
            this.metroTile1.TabIndex = 25;
            this.metroTile1.Text = "< BLE > \r\nMAC Addr";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTile1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // metroTile2
            // 
            this.metroTile2.Location = new System.Drawing.Point(195, 70);
            this.metroTile2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(189, 132);
            this.metroTile2.TabIndex = 26;
            this.metroTile2.Text = "< BLE > \r\nRSSI";
            this.metroTile2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTile2.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile2.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // mlbl_GPS_EW
            // 
            this.mlbl_GPS_EW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlbl_GPS_EW.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlbl_GPS_EW.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlbl_GPS_EW.Location = new System.Drawing.Point(963, 208);
            this.mlbl_GPS_EW.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.mlbl_GPS_EW.Name = "mlbl_GPS_EW";
            this.mlbl_GPS_EW.Size = new System.Drawing.Size(189, 132);
            this.mlbl_GPS_EW.Style = MetroFramework.MetroColorStyle.Magenta;
            this.mlbl_GPS_EW.TabIndex = 29;
            this.mlbl_GPS_EW.Text = "value";
            this.mlbl_GPS_EW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mlbl_GPS_EW.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mlbl_GPS_EW.UseStyleColors = true;
            // 
            // mlbl_GPS_NS
            // 
            this.mlbl_GPS_NS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mlbl_GPS_NS.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlbl_GPS_NS.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlbl_GPS_NS.Location = new System.Drawing.Point(774, 208);
            this.mlbl_GPS_NS.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.mlbl_GPS_NS.Name = "mlbl_GPS_NS";
            this.mlbl_GPS_NS.Size = new System.Drawing.Size(189, 132);
            this.mlbl_GPS_NS.Style = MetroFramework.MetroColorStyle.Magenta;
            this.mlbl_GPS_NS.TabIndex = 30;
            this.mlbl_GPS_NS.Text = "value";
            this.mlbl_GPS_NS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mlbl_GPS_NS.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mlbl_GPS_NS.UseStyleColors = true;
            // 
            // metroTile4
            // 
            this.metroTile4.Location = new System.Drawing.Point(963, 70);
            this.metroTile4.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.metroTile4.Name = "metroTile4";
            this.metroTile4.Size = new System.Drawing.Size(189, 132);
            this.metroTile4.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroTile4.TabIndex = 27;
            this.metroTile4.Text = "< GPS >\r\nE / W";
            this.metroTile4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTile4.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile4.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // metroTile3
            // 
            this.metroTile3.Location = new System.Drawing.Point(774, 70);
            this.metroTile3.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(189, 132);
            this.metroTile3.Style = MetroFramework.MetroColorStyle.Teal;
            this.metroTile3.TabIndex = 28;
            this.metroTile3.Text = "< GPS >\r\nN / S";
            this.metroTile3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTile3.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
            this.metroTile3.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.btn_message_Send);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(6, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(685, 61);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client Mode";
            // 
            // btn_message_Send
            // 
            this.btn_message_Send.Location = new System.Drawing.Point(238, 20);
            this.btn_message_Send.Name = "btn_message_Send";
            this.btn_message_Send.Size = new System.Drawing.Size(75, 32);
            this.btn_message_Send.TabIndex = 8;
            this.btn_message_Send.Text = "SendData";
            this.btn_message_Send.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(319, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "SendData_Auto";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 21);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "024643303343303030303552303530303003";
            // 
            // List_LOG
            // 
            this.List_LOG.Location = new System.Drawing.Point(1199, 0);
            this.List_LOG.Name = "List_LOG";
            this.List_LOG.Size = new System.Drawing.Size(682, 837);
            this.List_LOG.TabIndex = 21;
            this.List_LOG.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1884, 861);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1900, 900);
            this.MinimumSize = new System.Drawing.Size(1210, 900);
            this.Name = "Form1";
            this.Text = "Socket Server / Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.flp_WIFI_RSSI.ResumeLayout(false);
            this.flp_BLE_RSSI.ResumeLayout(false);
            this.flp_WIFI_SSID.ResumeLayout(false);
            this.flp_BLE_MAC.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox lbl_ServerIP_Port;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disConnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.FlowLayoutPanel flp_BLE_RSSI;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.FlowLayoutPanel flp_BLE_MAC;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroLabel mlbl_GPS_EW;
        private MetroFramework.Controls.MetroLabel mlbl_GPS_NS;
        private MetroFramework.Controls.MetroTile metroTile4;
        private MetroFramework.Controls.MetroTile metroTile3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_message_Send;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox List_LOG;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        private System.Windows.Forms.FlowLayoutPanel flp_WIFI_RSSI;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.FlowLayoutPanel flp_WIFI_SSID;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroTile metroTile6;
        private MetroFramework.Controls.MetroTile metroTile5;
        private MetroFramework.Controls.MetroButton mbtn_logView;
    }
}

