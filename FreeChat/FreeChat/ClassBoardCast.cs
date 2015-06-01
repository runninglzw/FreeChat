using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace FreeChat
{
    class ClassBoardCast
    {
        UdpClient bcUdpClient = new UdpClient();
        IPEndPoint bcIPEndPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 2425);

        public string localIP = string.Empty;

        //获取本机IP，如果是vista或windows7，取InterNetwork对应的地址
        public void GetLocalIP()
        {
            try
            {
                foreach (IPAddress _ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    if (_ipAddress.AddressFamily.ToString() == "InterNetwork")
                    {
                        localIP = _ipAddress.ToString();
                        break;
                    }
                    else
                    {
                        localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            
        }

        //发送自己的信息到广播地址
        public void BoardCast()
        {
            GetLocalIP();
            FormSetupUserInfo setUserInfo = new FormSetupUserInfo();
            string computerInfo = ":USER:" + setUserInfo.txtSetName.Text.Trim() + ":" + System.Environment.UserName +
                ":" + localIP+ ":" + setUserInfo.txtSetGroup.Text.Trim();

            byte[] buff = Encoding.Default.GetBytes(computerInfo);
           // while (true)
            //{
                bcUdpClient.Send(buff, buff.Length, bcIPEndPoint);
           //     Thread.Sleep(2000);
            //}
        }

        //用户退出时，发送消息至广播地址
        public void UserQuit()
        {
            GetLocalIP();
            string quitInfo = ":QUIT:" + localIP;
            byte[] bufQuit = Encoding.Default.GetBytes(quitInfo);

            bcUdpClient.Send(bufQuit, bufQuit.Length, bcIPEndPoint);
        }

        //收到别人上线的通知时，回复对方，以便对方将自己加入在线用户列表
        public void BCReply(string ipReply)
        {
            GetLocalIP();
            IPEndPoint EPReply = new IPEndPoint (IPAddress.Parse(ipReply),2425);
            FormSetupUserInfo setUserInfo = new FormSetupUserInfo();
            string computerInfo = ":REPY:" + setUserInfo.txtSetName.Text.Trim() + ":" + System.Environment.UserName +
                ":" + localIP + ":" + setUserInfo.txtSetGroup.Text.Trim();

            byte[] buff = Encoding.Default.GetBytes(computerInfo);
            bcUdpClient.Send(buff, buff.Length, EPReply);
        }
    }
}
