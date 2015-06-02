using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace FreeChat
{
    class ClassSendMsg
    {
        byte[] bufSendMsg = null;

        IPEndPoint sendMsgEP = null;
        UdpClient sendMsgUdpClient = new UdpClient();

        public ClassSendMsg(string r_desIP,byte[] bufMsg)
        {
            //发松udp消息的基本步骤，UdpClient绑定一个ip地址和端口号（connect（IPEndPoint））,直接发送即可。或者使用一个UdpClient的send方法向指定的IPEndPoint（ip终结点）发送也行（本例使用第二种）
            //接收udp消息的基本步骤，绑定一个端口号给UdpClient，使用UdpClient的receive方法（参数为IPEndPoint）接收即可
            this.sendMsgEP = new IPEndPoint(IPAddress.Parse(r_desIP), 2425);
            this.bufSendMsg = bufMsg;
        }
        //使用UDP协议发送文件，端口号为2425
        public void SendMessage()
        {
            //
            sendMsgUdpClient.Send(bufSendMsg, bufSendMsg.Length, sendMsgEP);
        }
    }
}
