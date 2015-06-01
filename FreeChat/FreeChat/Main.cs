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
        //引入User32.dll（用户界面相关的应用程序接口），入口点为FindWindow方法
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //声明排序器
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
            //当项在控件中显示小图标时使用
            this.lvFriend.SmallImageList = imageList;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {   
            //设置当前时间
            this.labDate.Text = DateTime.Now.ToString("yyyy年MM月dd日") + "    "+ DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn"));//zh-cn表示显示中文
            this.NotifyFreeChat.Text = System.Environment.UserName;

            //监听消息（广播和聊天）将lvFriend, lbUserCount作为参数传递，起到一个同步ListView和label的作用
            ClassStartUdpThread startUdpThread = new ClassStartUdpThread(lvFriend, lbUserCount);
            Thread tStartUdpThread = new Thread(new ThreadStart(startUdpThread.StartUdpThread));
            //设置为后台线程
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
            //在任务栏显示
            this.ShowInTaskbar = true;
            //正常显示
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
                //获得选中的项
                ListViewItem lvItem = lvFriend.SelectedItems[0];
               
                string windowsName = "与 " + lvItem.SubItems[1].Text + " 对话中";
                IntPtr handle = FindWindow(null, windowsName);
                //如果找到窗口则该窗口获得焦点
                if (handle != IntPtr.Zero)
                {
                    Form frm = (Form)Form.FromHandle(handle);
                    frm.WindowState = FormWindowState.Normal;
                    //设置窗口可见
                    frm.Activate();
                }
                    //没找到窗口则新创建一个聊天窗口
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
            //发送user:消息，自己发送如果其他用户没添加自己则在添加自己之后发送repy消息，这样两个用户都互相添加，相当于刷新
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
