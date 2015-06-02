using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.IO;

namespace FreeChat
{
    public partial class FormChat : Form
    {
        private string destinationIP = string.Empty;//目的ip
        private string destinationName = string.Empty;//目的用户名
        private string destinationID = string.Empty;//目的计算机名
        private string receiveMsg = string.Empty;//收到的消息
        public string Cuser = string.Empty;//自己的用户名
        public string CuserIP = string.Empty;//自己的ip

        public string filePath = string.Empty;
        public Socket socketTCPListen;
        public Socket socketReceiveFile;
        public IPEndPoint ipEP;
        byte[] Buff = new byte[1024000];
        
        const int WM_COPYDATA = 0x004A;//文本类型参数

        private bool isTextBoxNotEmpty = true; //判断输入文本框是否为空

        private ToolStripDropDown emotionDropDown = new ToolStripDropDown();

        RtfRichTextBox richtxtChat = new RtfRichTextBox();

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;//用户定义数据
            public int cbData;//数据大小
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;//指向数据的指针
        }//消息中传递的结构体

        public FormChat(string ip,string name,string id,string mesg)
        {
            destinationIP = ip;
            destinationName = name;
            destinationID = id;
            receiveMsg = mesg;
            InitializeComponent();
        }      
        //聊天窗口初始化，如果是有消息传过来，显示消息
        private void FormChat_Load(object sender, EventArgs e)
        {
            FormSetupUserInfo fUI = new FormSetupUserInfo();
            Cuser = fUI.txtSetName.Text.Trim();

            LoadImage();

            ClassBoardCast cBC = new ClassBoardCast();
            cBC.GetLocalIP();
            CuserIP = cBC.localIP;

            this.MaximizeBox = false;
            if (receiveMsg != string.Empty)
            {
                displayMessage(receiveMsg);
            }
        }

        //从Porperties.Resources加载图片
        private void LoadImage()
        {
            lock (richtxtChat.Emotions)
            {
                for (int i = 0; i < 30; i++)
                {
                    string imageName = "Face_" + i.ToString();
                    richtxtChat.Emotions[i.ToString()] = Properties.Resources.ResourceManager.GetObject(imageName) as Bitmap;                                     
                }
            }

            emotionDropDown.ImageScalingSize = new Size(18, 18);//图片大小
            emotionDropDown.LayoutStyle = ToolStripLayoutStyle.Table;//设置布局
            foreach (string str in richtxtChat.Emotions.Keys)
            {
                emotionDropDown.Items.Add(null, richtxtChat.Emotions[str], emotion_Click).ToolTipText = GetToolTipText(str);
            }
            ((TableLayoutSettings)emotionDropDown.LayoutSettings).ColumnCount = 10;//设置每行显示数目
        }

        //显示图片注释
        private string GetToolTipText(string str)
        {
            switch (str)
            {
                case "0":
                    str = "微笑";
                    break;
                case "1":
                    str = "发愁";
                    break;
                case "2":
                    str = "喜欢";
                    break;
                case "3":
                    str = "大笑";
                    break;
                case "4":
                    str = "不开心";
                    break;
                case "5":
                    str = "偷笑";
                    break;
                case "6":
                    str = "撇嘴";
                    break;
                case "7":
                    str = "晕";
                    break;
                case "8":
                    str = "讪笑";
                    break;
                case "9":
                    str = "鄙视";
                    break;
                case "10":
                    str = "委屈";
                    break;
                case "11":
                    str = "撅嘴";
                    break;
                case "12":
                    str = "可怜";
                    break;
                case "13":
                    str = "菜刀";
                    break;
                case "14":
                    str = "米饭";
                    break;
                case "15":
                    str = "猪头";
                    break;
                case "16":
                    str = "玫瑰";
                    break;
                case "17":
                    str = "爱心";
                    break;
                case "18":
                    str = "匕首";
                    break;
                case "19":
                    str = "大便";
                    break;
                case "20":
                    str = "晚安";
                    break;
                case "21":
                    str = "太阳";
                    break;
                case "22":
                    str = "拥抱";
                    break;
                case "23":
                    str = "赞赏";
                    break;
                case "24":
                    str = "不赞赏";
                    break;
                case "25":
                    str = "握手";
                    break;
                case "26":
                    str = "胜利";
                    break;
                case "27":
                    str = "佩服";
                    break;
                case "28":
                    str = "小指";
                    break;
                case "29":
                    str = "OK";
                    break;
                default:
                    break;

            }
            return str;

        }

        //图片点击事件
        private void emotion_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            if (item == null)
            {
                return;
            }
            //string text = item.ToolTipText;
            //if (text.Split(' ').Length > 1)
            //{
            //    text = text.Split(' ')[1];
            //}
            this.txtSMsg.InsertImage(item.Image);
            this.txtSMsg.Focus();               
        }

        //接收传递的消息(处理SendMessage()消息)
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    Type mytype = mystr.GetType();
                    mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                    receiveMsg = mystr.lpData;
                    
                    displayMessage(receiveMsg);//显示传过来的文件消息，并显示是否接受文件的控件
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
        //显示消息
        private void displayMessage(string msg)
        {
            try
            {
                
                if ( msg.Length>6 && msg.Substring(0, 6) == "【发送文件】")
                {
                    //在聊天窗口显示发送的内容
                    this.txtRMsg.AppendTextAsRtf(destinationName + "  " + DateTime.Now.ToLongTimeString() + "\r\n",
                        new Font(this.Font,FontStyle.Regular), RtfRichTextBox.RtfColor.Blue);

                    this.txtRMsg.SelectionColor = Color.Red;
                    this.txtRMsg.AppendText(receiveMsg + "\n");
                    //this.txtRMsg.AppendTextAsRtf("\n");
                    this.txtRMsg.ForeColor = Color.Black;
                    this.txtRMsg.Select(txtRMsg.Text.Length, 0);
                    this.txtRMsg.ScrollToCaret();

                    this.filePath = msg.Substring(6);
                    //显示是否接受文件的控件
                    this.labFileInfo.Text = destinationName + " 向你发送文件";
                    this.labFileInfo.Visible = true;
                    this.linkLableAccept.Visible = true;
                    this.linkLabelRefuse.Visible = true;
                }
                else if (msg.Length>6 && msg.Substring(0,6) == "【发送信息】")
                {
                    //this.txtRMsg.AppendTextAsRtf(destinationName + " " + DateTime.Now.ToLongTimeString() + "\r\n",
                    //   new Font(this.Font,FontStyle.Regular), RtfRichTextBox.RtfColor.Blue);
                    this.txtRMsg.SelectionColor = Color.Red;
                    this.txtRMsg.AppendText(receiveMsg + "\n");
                    //this.txtRMsg.AppendTextAsRtf("\n");
                    this.txtRMsg.ForeColor = Color.Black;
                    this.txtRMsg.Select(txtRMsg.Text.Length, 0);
                    this.txtRMsg.ScrollToCaret();
                }
                else
                {
                    this.txtRMsg.AppendTextAsRtf(destinationName + "  " + DateTime.Now.ToLongTimeString() + "\r\n",
                        new Font(this.Font, FontStyle.Regular), RtfRichTextBox.RtfColor.Blue);
                    this.txtRMsg.AppendTextAsRtf("   ");
                    this.txtRMsg.AppendRtf(receiveMsg);
                    //this.txtRMsg.AppendTextAsRtf("\n");
                    this.txtRMsg.Select(txtRMsg.Text.Length, 0);
                    this.txtRMsg.ScrollToCaret();

                }
            }
            catch
            {
            }
        }
        //发送消息
        private void btnSentMsg_Click(object sender, EventArgs e)
        {
                sentMessage();
        }
        private void sentMessage()
        {
            if (this.txtSMsg.Text == "")
            {
                this.txtSMsg.Text = "输入消息不能为空...";
                this.txtSMsg.BackColor = Color.OldLace;
                this.isTextBoxNotEmpty = false;//设置输入的消息不为空
                this.txtSMsg.ReadOnly = true;//输入消息只读
            }
            if(isTextBoxNotEmpty)
            {
                try
                {
                    string sendMessageInfo = ":MESG:" + Cuser + "|" + System.Environment.UserName + "|" +
                        CuserIP + "|" + this.txtSMsg.Rtf;
                    byte[] buff = Encoding.Default.GetBytes(sendMessageInfo);

                    ClassSendMsg cSendMsg = new ClassSendMsg(destinationIP, buff);
                    cSendMsg.SendMessage();

                    this.txtRMsg.AppendTextAsRtf(Cuser + "  " + DateTime.Now.ToLongTimeString() + "\r\n", 
                        new Font(this.Font, FontStyle.Regular), RtfRichTextBox.RtfColor.Green);

                    this.txtRMsg.AppendTextAsRtf("   ");
                    this.txtRMsg.AppendRtf(this.txtSMsg.Rtf);
                    //this.txtRMsg.AppendTextAsRtf("\n");
                    this.txtRMsg.Select(txtRMsg.Text.Length, 0);
                    this.txtRMsg.ScrollToCaret();
                    this.txtSMsg.Text = string.Empty;
                }
                catch
                {
                    this.txtRMsg.AppendText(DateTime.Now.ToLongTimeString() + " 发送消息失败！" + "\r\n");
                }
            }
        }
        //发送文件按钮
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            try
            {   
                OpenFileDialog Dlg = new OpenFileDialog();
                FileInfo FI;
                Dlg.Filter = "所有文件(*.*)|*.*";
                Dlg.CheckFileExists = true;
                Dlg.InitialDirectory = "C:\\Documents and Settings\\" + System.Environment.UserName + "\\桌面\\";
               //如果点了Ok，发送文件
                if (Dlg.ShowDialog() == DialogResult.OK)
                {
                    FI = new FileInfo(Dlg.FileName);
                    //发送":DATA:"消息
                    string sendMsg = ":DATA:" + Cuser + "|" + System.Environment.UserName + "|" +
                        CuserIP + "|" + Dlg.FileName + "|" + FI.Length + "|";

                    byte[] buff  = Encoding.Default.GetBytes(sendMsg);

                    ClassSendMsg cSendFileInfo = new ClassSendMsg (destinationIP,buff);
                    cSendFileInfo.SendMessage();
                    //在聊天窗口中显示信息
                    this.txtRMsg.AppendTextAsRtf(Cuser + "  " + DateTime.Now.ToLongTimeString() + "\r\n",
                        new Font(this.Font, FontStyle.Regular), RtfRichTextBox.RtfColor.Green);

                    this.txtRMsg.SelectionColor = Color.Red;
                    this.txtRMsg.AppendText("【发送文件】" + Dlg.FileName + "\r\n");
                    this.txtRMsg.ForeColor = Color.Black;
                    this.txtRMsg.Select(txtRMsg.Text.Length, 0);
                    this.txtRMsg.ScrollToCaret();
                    
                }                
            }
            catch
            {
                MessageBox.Show("文件发送失败！" + "\r\n");
            }
        }

        //拒绝接收文件按钮
        private void linkLabelRefuse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.labFileInfo.Visible = false;
            this.linkLableAccept.Visible = false;
            this.linkLabelRefuse.Visible = false;

            string sendMsg = ":MESG:" + Cuser + "|" + System.Environment.UserName + "|" +
                    CuserIP + "|" + "【发送信息】对方拒绝接收";
            byte[] buff = Encoding.Default.GetBytes(sendMsg);

            ClassSendMsg cSendFileInfo = new ClassSendMsg(destinationIP, buff);
            cSendFileInfo.SendMessage();
        }
        //接受文件的按钮
        private void linkLableAccept_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {                
                this.linkLabelRefuse.Enabled = false;
                this.linkLableAccept.Enabled = false;

                string aToReceive = ":ACEP:" + Cuser + "|" + System.Environment.UserName + "|" +
                    CuserIP + "|" + filePath + "|";
                byte[] buff = Encoding.Default.GetBytes(aToReceive);

                string[] realFileName = filePath.Split('\\');
                string filename = realFileName[realFileName.Length - 1].ToString();
                int len;


                //发送接受文件信息
                ClassSendMsg cReadyToReceive = new ClassSendMsg(destinationIP, buff);
                cReadyToReceive.SendMessage();
                //同意接收文件，发送同意请求，并打开TCP监听
                TCPListen();

                socketReceiveFile = socketTCPListen.Accept();
                //选择路径保存
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.OverwritePrompt = true;
                SFD.RestoreDirectory = true;
                SFD.Filter = "所有文件(*.*)|*.*";
                SFD.InitialDirectory = "C:\\Documents and Settings\\" + System.Environment.UserName + "\\桌面\\";
                SFD.FileName = filename;

                if ((len = socketReceiveFile.Receive(Buff)) != 0)
                {
                    if (SFD.ShowDialog() == DialogResult.OK)
                    {
                        //将字节流写入到文件流的基本方法，先声明一个文件流，用写的方式打开，循环将接收到的字节流写入到文件流中
                        FileStream FS = new FileStream(SFD.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                        FS.Write(Buff, 0, len);
                        while ((len = socketReceiveFile.Receive(Buff)) != 0)
                        {
                            FS.Write(Buff, 0, len);
                        }
                        FS.Flush();
                        FS.Close();
                        this.txtRMsg.SelectionColor = Color.Red;
                        this.txtRMsg.AppendText("【接收完成】文件已保存" + "\r\n");
                        this.txtRMsg.ForeColor = Color.Black;
                    }
                }
                //返回保存成功消息
                string sendMessageInfo = ":MESG:" + Cuser + "|" + System.Environment.UserName + "|" +
                    CuserIP + "|" + "【发送信息】文件已发送成功";
                byte[] buffReply = Encoding.Default.GetBytes(sendMessageInfo);
                //发送
                ClassSendMsg cSendMsg = new ClassSendMsg(destinationIP, buffReply);
                cSendMsg.SendMessage();

                socketTCPListen.Close();
                socketReceiveFile.Close();

                this.linkLabelRefuse.Enabled = true;
                this.linkLableAccept.Enabled = true;
                //在将确认接收文件的控件设置为不可见
                this.labFileInfo.Visible = false;
                this.linkLabelRefuse.Visible = false;
                this.linkLableAccept.Visible = false;
            }
            catch
            {
            }
            finally
            {
                socketTCPListen.Close();
                socketReceiveFile.Close();
            }
        }

        //同意接收文件，发送同意请求，并打开TCP监听
        public void TCPListen()
        {
            socketTCPListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipEP = new IPEndPoint(IPAddress.Parse(CuserIP), 8001);
            socketTCPListen.Bind(ipEP);
            socketTCPListen.Listen(1024);
        }
                
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //聊天窗口ESC退出快捷键
        private void FormChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        //当发送消息为空时，显示提示信息，鼠标单击此区域，恢复可输入状态
        private void txtSMsg_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isTextBoxNotEmpty)
            {
                this.txtSMsg.Text = "";
                this.txtSMsg.BackColor = Color.White;
                isTextBoxNotEmpty = true;
                this.txtSMsg.ReadOnly = false;
            }
        }

        //显示表情对话框
        private void btnImage_Click(object sender, EventArgs e)
        {
            //获取当前按钮在屏幕上的位置           
            Point pt = this.PointToScreen(new Point(toolStrip1.Location.X,toolStrip1.Location.Y));
            emotionDropDown.Show(pt.X,pt.Y-80);
        }

        //显示字体对话框
        private void btnFont_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == fontDialog1.ShowDialog())
            {
                this.txtSMsg.Font = fontDialog1.Font;
                this.txtSMsg.ForeColor = fontDialog1.Color;
            }
        }
    }
}
