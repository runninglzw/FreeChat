using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FreeChat
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            labName.Text = "SunJiuru";
            labPhone.Text = " ";
            labVersion.Text = "V1.03";

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
