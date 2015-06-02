using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace FreeChat
{
    class ClassStartUdpThread
    { 
       
        private ListView lvDisplayUser;//listview显示在线列表
        private Label lbUserCount;//label显示在线人数

        public ClassStartUdpThread(ListView lDisplayUser, Label lUserCount)
        {
            this.lvDisplayUser = lDisplayUser;
            this.lbUserCount = lUserCount;
        }

        //在程序运行后保持监听2425端口，负责处理各种类型消息
        //1.用户第一次登陆发送":USER:"消息，收到此类消息会将对方加入到自己的在线好友列表中
        //2.接收到":USER:"消息后，向对方发送":REPY:"消息，让对方将自己加入好友列表中
        //3.发送文字时候也是发送的":USER:"类型的消息
        //4.发送文件时发送的是":DATA:"类型的消息，当处理方点击了确认接收文件的按钮时，在向发送方发送":ACEP:"消息，接收方同时在绑定Socket之后进行监听Socket连接，同时发送方在接收到":ACEP:"消息之后开始连入接收方绑定好的Socket，这样双方的文件传输通道就建立了，发送方循环发送，接收方循环接收即可
        public void StartUdpThread()
        {
            UdpClient udpClient = new UdpClient(2425);
            //绑定任意ip地址和一个端口号用来接收别人的信息
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] buff = udpClient.Receive(ref ipEndPoint);
                string userInfo = Encoding.Default.GetString(buff);
                string msgHead = userInfo.Substring(0, 6);//消息前6位为消息类型标识符
                string msgBody = userInfo.Substring(6);//第7位开始为消息实体内容

                switch(msgHead)
                {
                    /*用户第一次登录时发送USER消息到广播地址，收到此类消息会将对方
                     * 加入到自己的在线好友列表中，并回复对方消息，告诉对方自己在线 */
                    case ":USER:" :
                        try
                        {
                            string[] sBody = msgBody.Split(':');
                            //创建一个新的ListViewItem用来插入ListView
                            ListViewItem lviUser = new ListViewItem();
                            //ListViewItem:整个表格的一行。ListViewSubItem：表格中某个单元格。
                            ListViewItem.ListViewSubItem lviComputerName = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem lviIP = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem lvGroup = new ListViewItem.ListViewSubItem();

                            lviUser.Text = sBody[0];
                            lviComputerName.Text = sBody[1];
                            lviIP.Text = sBody[2];
                            lvGroup.Text = sBody[3];

                            lviUser.SubItems.Add(lviComputerName);
                            lviUser.SubItems.Add(lviIP);
                            lviUser.SubItems.Add(lvGroup);

                            bool flag = true;
                            //遍历ListView
                            for (int i = 0; i < this.lvDisplayUser.Items.Count; i++)
                            {

                                if (lviIP.Text == this.lvDisplayUser.Items[i].SubItems[2].Text)
                                {
                                    //如果ip地址相等（同一个计算机）而用户名不等：删除原来的，添加刚创建的
                                    if (lviUser.Text != this.lvDisplayUser.Items[i].SubItems[0].Text)
                                    {
                                        this.lvDisplayUser.Items[i].Remove();
                                        flag = true;
                                    }
                                    //如果ip地址相等（同一个计算机）而用户名也相等，不必添加到列表中
                                    else
                                    {
                                        flag = false;
                                    }
                                }
                            }
                            if (flag)
                            {
                                this.lvDisplayUser.Items.Add(lviUser);
                            }                
                            //更新在线人数
                            lbUserCount.Text = "在线人数：  " + this.lvDisplayUser.Items.Count;

                            //回复消息
                            ClassBoardCast CReply = new ClassBoardCast();                            
                            CReply.BCReply(lviIP.Text);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;

                    //聊天消息MESG
                    case ":MESG:":
                        try
                        {
                            string[] mBody = msgBody.Split('|');
                            string msgName = mBody[0];
                            string msgID = mBody[1];
                            string msgIP = mBody[2];
                            string msgDetail = mBody[3];
                            
                            //创建一条新线程接收消息
                            ClassReceiveMsg cRecMsg = new ClassReceiveMsg(msgIP, msgName,msgID, msgDetail);
                            Thread tRecMsg = new Thread(new ThreadStart(cRecMsg.StartRecMsg));
                            tRecMsg.Start();
                        }
                        catch
                        {
                        }
                        break; 

                    //用户退出时发送QUIT开头的消息
                    case ":QUIT:":
                        try
                        {
                            for (int i = 0; i < this.lvDisplayUser.Items.Count; i++)
                            {
                                if (msgBody == this.lvDisplayUser.Items[i].SubItems[2].Text)
                                {
                                    //从当前在线用户列表中删除
                                    this.lvDisplayUser.Items[i].Remove();                                    
                                }
                            }
                            lbUserCount.Text = "在线人数：  " + this.lvDisplayUser.Items.Count;
                        }
                        catch
                        { 
                        }
                        break;

                    /*自己上线时会向广播发送消息，在接到别人以REPY开头的回复消息时，
                    将对方加入自己的在线好友列表中*/        
                    case ":REPY:":
                        try
                        {
                            string[] sBody = msgBody.Split(':');
                            ListViewItem lviUser = new ListViewItem();
                            ListViewItem.ListViewSubItem lviComputerName = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem lviIP = new ListViewItem.ListViewSubItem();
                            ListViewItem.ListViewSubItem lvGroup = new ListViewItem.ListViewSubItem();

                            lviUser.Text = sBody[0];
                            lviComputerName.Text = sBody[1];
                            lviIP.Text = sBody[2];
                            lvGroup.Text = sBody[3];

                            lviUser.SubItems.Add(lviComputerName);
                            lviUser.SubItems.Add(lviIP);
                            lviUser.SubItems.Add(lvGroup);

                            bool flag = true;
                            for (int i = 0; i < this.lvDisplayUser.Items.Count; i++)
                            {
                                //如果已经有对方ip则不必添加
                                if (lviIP.Text == this.lvDisplayUser.Items[i].SubItems[2].Text)
                                {
                                    //if (lviUser.Text != this.lvDisplayUser.Items[i].SubItems[0].Text)
                                    //{
                                    //    //MessageBox.Show(this.lvDisplayUser.Items[i].SubItems[0].Text);
                                    //    MessageBox.Show(this.lvDisplayUser.Items[i].Text);
                                    //    this.lvDisplayUser.Items[i].Remove();
                                    //}                                                                
                                    flag = false;                                
                                }
                            }
                            if (flag)
                            {
                                this.lvDisplayUser.Items.Add(lviUser);
                            }                

                            lbUserCount.Text = "在线人数：  " + this.lvDisplayUser.Items.Count;  
                           
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;

                    //以DATA开头的消息，表示有人发送文件
                    case ":DATA:":
                        try
                        {
                            string[] mBody = msgBody.Split('|');
                            string msgName = mBody[0];
                            string msgID = mBody[1];
                            string msgIP = mBody[2];
                            string msgFileName = mBody[3];
                            string msgFileLen = mBody[4];

                            string msgDetail = "【发送文件】" + msgFileName;
                            //创建一条新线程接收消息
                            ClassReceiveMsg cRecMsg = new ClassReceiveMsg(msgIP, msgName, msgID, msgDetail);
                            Thread tRecMsg = new Thread(new ThreadStart(cRecMsg.StartRecMsg));
                            tRecMsg.Start();
                        }
                        catch
                        { 
                        }
                        break;

                    //接到以ACEP开头的消息，表示对方同意接收文件
                    case ":ACEP:":
                        try
                        {
                            string[] mFileBody = msgBody.Split('|');
                            string mFilePath = mFileBody[3];
                            string mIP = mFileBody[2];

                            ClassSendFile cSendFile = new ClassSendFile(mFilePath, mIP);
                            Thread tSendFile = new Thread(new ThreadStart(cSendFile.SendFile));
                            tSendFile.IsBackground = true;
                            tSendFile.Start();
                        }
                        catch
                        { 
                        }
                        break;
                }
            }
        }       
    }
}
