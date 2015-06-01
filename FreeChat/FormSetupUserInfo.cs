using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FreeChat
{
    //保存和获取用户名和工作组的类
    public partial class FormSetupUserInfo : Form
    {
        public FormSetupUserInfo()
        {
            InitializeComponent();
            try
            {
                //定义一个文件流来存放用户信息
                FileStream fsRead = new FileStream(@"UserInformation.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fsRead);
                string userInfo = sr.ReadLine();
                string[] info = userInfo.Split(':');
                this.txtSetName.Text = info[0];
                this.txtSetGroup.Text = info[1];
                sr.Close();
                fsRead.Close();
            }
            catch
            {
                FileStream fsWrite = new FileStream(@"UserInformation.txt",FileMode.Create,FileAccess.Write);
                //获取登录计算机的用户名                              与当前用户关联的网络域名
                string date = System.Environment.UserName + ":" +System.Environment.UserDomainName;
                StreamWriter sw = new StreamWriter(fsWrite);
                sw.Write(date);
                sw.Close();
                fsWrite.Close();
                this.txtSetName.Text = System.Environment.UserName;
                this.txtSetGroup.Text = System.Environment.UserDomainName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtSetName.Text == "")
            {
                //显示错误errorProvider
                errorProvider1.SetError(txtSetName, "请输入用户名");
            }
            else if (this.txtSetGroup.Text == "")
            {
                errorProvider1.SetError(txtSetGroup, "请输入项目组名");
            }
            else
            {
                errorProvider1.Dispose();
                FileStream write = new FileStream(@"UserInformation.txt", FileMode.Create, FileAccess.Write);
                string data = this.txtSetName.Text + ":" + this.txtSetGroup.Text;
                StreamWriter sw = new StreamWriter(write);
                sw.Write(data);
                sw.Close();
                write.Close();
                this.Update();
                this.Close();
                ClassBoardCast CUpdate = new ClassBoardCast();
                CUpdate.BoardCast();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
