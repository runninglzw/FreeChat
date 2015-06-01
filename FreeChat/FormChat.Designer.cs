namespace FreeChat
{
    partial class FormChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChat));
            this.btnSentMsg = new System.Windows.Forms.Button();
            this.linkLableAccept = new System.Windows.Forms.LinkLabel();
            this.linkLabelRefuse = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFont = new System.Windows.Forms.ToolStripButton();
            this.btnImage = new System.Windows.Forms.ToolStripButton();
            this.btnSendFile = new System.Windows.Forms.ToolStripButton();
            this.labFileInfo = new System.Windows.Forms.Label();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.txtSMsg = new FreeChat.RtfRichTextBox();
            this.txtRMsg = new FreeChat.RtfRichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSentMsg
            // 
            this.btnSentMsg.Location = new System.Drawing.Point(345, 321);
            this.btnSentMsg.Name = "btnSentMsg";
            this.btnSentMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSentMsg.TabIndex = 1;
            this.btnSentMsg.Text = "发送(&S)";
            this.btnSentMsg.UseVisualStyleBackColor = true;
            this.btnSentMsg.Click += new System.EventHandler(this.btnSentMsg_Click);
            // 
            // linkLableAccept
            // 
            this.linkLableAccept.AutoSize = true;
            this.linkLableAccept.BackColor = System.Drawing.SystemColors.Control;
            this.linkLableAccept.Font = new System.Drawing.Font("SimSun", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLableAccept.LinkColor = System.Drawing.Color.Green;
            this.linkLableAccept.Location = new System.Drawing.Point(331, 199);
            this.linkLableAccept.Name = "linkLableAccept";
            this.linkLableAccept.Size = new System.Drawing.Size(33, 13);
            this.linkLableAccept.TabIndex = 5;
            this.linkLableAccept.TabStop = true;
            this.linkLableAccept.Text = "接受";
            this.linkLableAccept.Visible = false;
            this.linkLableAccept.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLableAccept_LinkClicked);
            // 
            // linkLabelRefuse
            // 
            this.linkLabelRefuse.AutoSize = true;
            this.linkLabelRefuse.BackColor = System.Drawing.SystemColors.Control;
            this.linkLabelRefuse.Font = new System.Drawing.Font("SimSun", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabelRefuse.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.linkLabelRefuse.Location = new System.Drawing.Point(372, 199);
            this.linkLabelRefuse.Name = "linkLabelRefuse";
            this.linkLabelRefuse.Size = new System.Drawing.Size(33, 13);
            this.linkLabelRefuse.TabIndex = 6;
            this.linkLabelRefuse.TabStop = true;
            this.linkLabelRefuse.Text = "拒绝";
            this.linkLabelRefuse.Visible = false;
            this.linkLabelRefuse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRefuse_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(262, 321);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImage,
            this.btnFont,
            this.btnSendFile});
            this.toolStrip1.Location = new System.Drawing.Point(9, 195);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(72, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnFont
            // 
            this.btnFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFont.Image = ((System.Drawing.Image)(resources.GetObject("btnFont.Image")));
            this.btnFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(23, 22);
            this.btnFont.Text = "设置字体";
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // btnImage
            // 
            this.btnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImage.Image = ((System.Drawing.Image)(resources.GetObject("btnImage.Image")));
            this.btnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(23, 22);
            this.btnImage.Text = "发送表情";
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // btnSendFile
            // 
            this.btnSendFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSendFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSendFile.Image")));
            this.btnSendFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(23, 22);
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // labFileInfo
            // 
            this.labFileInfo.AutoSize = true;
            this.labFileInfo.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labFileInfo.ForeColor = System.Drawing.Color.Blue;
            this.labFileInfo.Location = new System.Drawing.Point(192, 200);
            this.labFileInfo.Name = "labFileInfo";
            this.labFileInfo.Size = new System.Drawing.Size(113, 12);
            this.labFileInfo.TabIndex = 10;
            this.labFileInfo.Text = "admin 向你发送文件";
            this.labFileInfo.Visible = false;
            // 
            // fontDialog1
            // 
            this.fontDialog1.ShowColor = true;
            // 
            // txtSMsg
            // 
            this.txtSMsg.HiglightColor = FreeChat.RtfRichTextBox.RtfColor.White;
            this.txtSMsg.Location = new System.Drawing.Point(3, 223);
            this.txtSMsg.Name = "txtSMsg";
            this.txtSMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtSMsg.Size = new System.Drawing.Size(422, 92);
            this.txtSMsg.TabIndex = 0;
            this.txtSMsg.Text = "";
            this.txtSMsg.TextColor = FreeChat.RtfRichTextBox.RtfColor.Black;
            this.txtSMsg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtSMsg_MouseClick);
            // 
            // txtRMsg
            // 
            this.txtRMsg.BackColor = System.Drawing.SystemColors.Window;
            this.txtRMsg.HiglightColor = FreeChat.RtfRichTextBox.RtfColor.White;
            this.txtRMsg.Location = new System.Drawing.Point(3, 12);
            this.txtRMsg.Name = "txtRMsg";
            this.txtRMsg.ReadOnly = true;
            this.txtRMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtRMsg.Size = new System.Drawing.Size(422, 181);
            this.txtRMsg.TabIndex = 4;
            this.txtRMsg.Text = "";
            this.txtRMsg.TextColor = FreeChat.RtfRichTextBox.RtfColor.Black;
            // 
            // FormChat
            // 
            this.AcceptButton = this.btnSentMsg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(432, 347);
            this.Controls.Add(this.labFileInfo);
            this.Controls.Add(this.txtSMsg);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.linkLabelRefuse);
            this.Controls.Add(this.linkLableAccept);
            this.Controls.Add(this.txtRMsg);
            this.Controls.Add(this.btnSentMsg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormChat";
            this.Load += new System.EventHandler(this.FormChat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormChat_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSentMsg;
        //private System.Windows.Forms.RichTextBox txtRMsg;
        private System.Windows.Forms.LinkLabel linkLableAccept;
        private System.Windows.Forms.LinkLabel linkLabelRefuse;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnImage;
        private System.Windows.Forms.ToolStripButton btnSendFile;
        //private System.Windows.Forms.RichTextBox txtSMsg;
        private RtfRichTextBox txtSMsg;
        private RtfRichTextBox txtRMsg;
        private System.Windows.Forms.Label labFileInfo;
        private System.Windows.Forms.ToolStripButton btnFont;
        private System.Windows.Forms.FontDialog fontDialog1;
    }
}