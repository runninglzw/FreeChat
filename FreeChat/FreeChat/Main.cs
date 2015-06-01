using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace FreeChat
{
    public partial class MainForm : Form
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private ClassSortListView lvwColumnSorter;

        private int isSearchNum = -1;

        public MainForm()
        {
            MainForm.CheckForIllegalCrossThreadCalls = false;//在多线程程序中，新创建的线程不能访问UI线程创建的窗口控件，如果需要访问窗口中的控件，可以在窗口构造函数中将CheckForIllegalCrossThreadCalls设置为 false
            InitializeComponent();

            lvwColumnSorter = new ClassSortListView();
            this.lvFriend.ListViewItemSorter = lvwColumnSorter;     //为lvFriend列表指定排序器

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 15);

            this.lvFriend.SmallImageList = imageList;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {   
            //设置当前时间
            this.labDate.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "    "+ DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn"));
            this.NotifyFreeChat.Text = System.Environment.UserName;

            //监听消息（广播和聊天）
            ClassStartUdpThread startUdpThread = new ClassStartUdpThread(lvFriend, lbUserCount);
            Thread tStartUdpThread = new Thread(new ThreadStart(startUdpThread.StartUdpThread));
            tStartUdpThread.IsBackground = true;
            tStartUdpThread.Start();

            //第一次登录时发送广播消息，查看在线用户
            ClassBoardCast boardCast = new ClassBoardCast();
            boardCast.BoardCast();
        }

        //主菜单列表最小化时，不在状态栏显示，通知图标显示
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                NotifyFreeChat.Visible = true;
            }
        }
        //通知图标双击
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
        }
        //用户退出时，向广播地址发送消息
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassBoardCast cUserQuit = new ClassBoardCast();
            cUserQuit.UserQuit();
            this.Dispose();
        }
        //设置用户信息
        private void SetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSetupUserInfo setUInfo = new FormSetupUserInfo();
            setUInfo.MaximizeBox = false;
            setUInfo.Show();
        }
        //关于窗口
        private void AboutStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAbout FA = new FormAbout();
            FA.ShowDialog();
        }

        //生成聊天窗口
        private void lvFriend_ItemActivate(object sender, EventArgs e)
        {
            //bool isFormexist;
            if (lvFriend.SelectedItems[0].Index != -1)
            {                
                ListViewItem lvItem = lvFriend.SelectedItems[0];
               
                string windowsName = "与 " + lvItem.SubItems[1].Text + " 对话中";
                IntPtr handle = FindWindow(null, windowsName);
                if (handle != IntPtr.Zero)
                {
                    Form frm = (Form)Form.FromHandle(handle);
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                }
                else
                {
                    //ipSend为从列表中取出，要发送的对象的IP
                    string ipSend = lvItem.SubItems[2].Text;
                    string nameSend = lvItem.SubItems[0].Text;
                    string idSend = lvItem.SubItems[1].Text;
                    string mesSend = string.Empty;
                    FormChat fChat = new FormChat(ipSend, nameSend,idSend, mesSend);
                    //fChat.Name = lvItem.SubItems[0].Text;
                    fChat.Text = "与 " + lvItem.SubItems[1].Text + " 对话中";
                    fChat.Show();
                }

            }
        }

        //对所点击的列进行排序
        private void lvFriend_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //检查点击的列是不是默认排序列
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                //设置排序列
                lvwColumnSorter.SortColumn = e.Column;
               // lvwColumnSorter.Order = SortOrder.Ascending;
            }
            this.lvFriend.Sort();
        } 

        //刷新在线列表
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.lvFriend.Items.Clear();
            
            ClassBoardCast CUpdate = new ClassBoardCast();
            CUpdate.BoardCast();
        }
        //主菜单点击关闭按钮时，窗口最小化，而不是退出
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            e.Cancel = true;
        }

        //当txtSearch发生变化时与listview项进行匹配
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                for (int i = 0; i < this.lvFriend.Items.Count; i++)
                {
                    if (txtSearch.Text == this.lvFriend.Items[i].SubItems[1].Text)
                    {
                        //this.lvFriend.Items[i].ForeColor = Color.Blue;
                        this.lvFriend.Items[i].BackColor = Color.Salmon;
                        isSearchNum = i;
                    }
                }
            }
            else
            {
                if (isSearchNum > -1)
                {
                    //this.lvFriend.Items[isSearchNum].ForeColor = Color.Black;
                    this.lvFriend.Items[isSearchNum].BackColor = Color.Snow;
                }
            }
        }


              
    }
}
