namespace FreeChat
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lvFriend = new System.Windows.Forms.ListView();
            this.chUname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbUserCount = new System.Windows.Forms.Label();
            this.NotifyFreeChat = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AboutStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.labDate = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.labSearch = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFriend
            // 
            this.lvFriend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFriend.BackColor = System.Drawing.Color.Snow;
            this.lvFriend.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUname,
            this.chMname,
            this.chIP,
            this.chGroup});
            this.lvFriend.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvFriend.Location = new System.Drawing.Point(0, 24);
            this.lvFriend.Name = "lvFriend";
            this.lvFriend.Size = new System.Drawing.Size(286, 563);
            this.lvFriend.TabIndex = 1;
            this.lvFriend.UseCompatibleStateImageBehavior = false;
            this.lvFriend.View = System.Windows.Forms.View.Details;
            this.lvFriend.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvFriend_ColumnClick);
            this.lvFriend.ItemActivate += new System.EventHandler(this.lvFriend_ItemActivate);
            // 
            // chUname
            // 
            this.chUname.Text = "用户名";
            this.chUname.Width = 72;
            // 
            // chMname
            // 
            this.chMname.Text = "ID";
            this.chMname.Width = 85;
            // 
            // chIP
            // 
            this.chIP.Text = "IP";
            this.chIP.Width = 41;
            // 
            // chGroup
            // 
            this.chGroup.Text = "项目组";
            // 
            // lbUserCount
            // 
            this.lbUserCount.AutoSize = true;
            this.lbUserCount.Location = new System.Drawing.Point(8, 594);
            this.lbUserCount.Name = "lbUserCount";
            this.lbUserCount.Size = new System.Drawing.Size(65, 12);
            this.lbUserCount.TabIndex = 2;
            this.lbUserCount.Text = "在线人数：";
            // 
            // NotifyFreeChat
            // 
            this.NotifyFreeChat.ContextMenuStrip = this.contextMenuStrip1;
            this.NotifyFreeChat.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyFreeChat.Icon")));
            this.NotifyFreeChat.Text = "FreeChat";
            this.NotifyFreeChat.Visible = true;
            this.NotifyFreeChat.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutStripMenuItem1,
            this.设置ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // AboutStripMenuItem1
            // 
            this.AboutStripMenuItem1.Name = "AboutStripMenuItem1";
            this.AboutStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.AboutStripMenuItem1.Text = "关于";
            this.AboutStripMenuItem1.Click += new System.EventHandler(this.AboutStripMenuItem1_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.设置ToolStripMenuItem.Text = "设置";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.SetToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(203, 594);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(49, 36);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // labDate
            // 
            this.labDate.AutoSize = true;
            this.labDate.ForeColor = System.Drawing.Color.Navy;
            this.labDate.Location = new System.Drawing.Point(7, 9);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(53, 12);
            this.labDate.TabIndex = 4;
            this.labDate.Text = "2015-4-1";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txtSearch.Location = new System.Drawing.Point(79, 609);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(104, 21);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // labSearch
            // 
            this.labSearch.AutoSize = true;
            this.labSearch.Location = new System.Drawing.Point(8, 613);
            this.labSearch.Name = "labSearch";
            this.labSearch.Size = new System.Drawing.Size(65, 12);
            this.labSearch.TabIndex = 5;
            this.labSearch.Text = "查找用户：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 629);
            this.Controls.Add(this.labSearch);
            this.Controls.Add(this.lbUserCount);
            this.Controls.Add(this.lvFriend);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.labDate);
            this.Controls.Add(this.btnRefresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 668);
            this.MinimumSize = new System.Drawing.Size(272, 668);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvFriend;
        private System.Windows.Forms.ColumnHeader chUname;
        private System.Windows.Forms.ColumnHeader chMname;
        private System.Windows.Forms.ColumnHeader chIP;
        private System.Windows.Forms.Label lbUserCount;
        private System.Windows.Forms.NotifyIcon NotifyFreeChat;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chGroup;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolStripMenuItem AboutStripMenuItem1;
        private System.Windows.Forms.Label labDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label labSearch;
    }
}

