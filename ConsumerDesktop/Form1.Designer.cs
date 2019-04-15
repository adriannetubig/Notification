namespace ConsumerDesktop
{
    partial class Form1
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
            this.tcConsumer = new System.Windows.Forms.TabControl();
            this.tpUnauthenticated = new System.Windows.Forms.TabPage();
            this.dgvUnauthenticated = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAuthenticated = new System.Windows.Forms.DataGridView();
            this.tcConsumer.SuspendLayout();
            this.tpUnauthenticated.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnauthenticated)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthenticated)).BeginInit();
            this.SuspendLayout();
            // 
            // tcConsumer
            // 
            this.tcConsumer.Controls.Add(this.tpUnauthenticated);
            this.tcConsumer.Controls.Add(this.tabPage2);
            this.tcConsumer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcConsumer.Location = new System.Drawing.Point(0, 0);
            this.tcConsumer.Name = "tcConsumer";
            this.tcConsumer.SelectedIndex = 0;
            this.tcConsumer.Size = new System.Drawing.Size(800, 350);
            this.tcConsumer.TabIndex = 0;
            // 
            // tpUnauthenticated
            // 
            this.tpUnauthenticated.Controls.Add(this.dgvUnauthenticated);
            this.tpUnauthenticated.Location = new System.Drawing.Point(4, 22);
            this.tpUnauthenticated.Name = "tpUnauthenticated";
            this.tpUnauthenticated.Padding = new System.Windows.Forms.Padding(3);
            this.tpUnauthenticated.Size = new System.Drawing.Size(792, 324);
            this.tpUnauthenticated.TabIndex = 0;
            this.tpUnauthenticated.Text = "Unauthenticated";
            this.tpUnauthenticated.UseVisualStyleBackColor = true;
            // 
            // dgvUnauthenticated
            // 
            this.dgvUnauthenticated.AllowUserToAddRows = false;
            this.dgvUnauthenticated.AllowUserToDeleteRows = false;
            this.dgvUnauthenticated.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUnauthenticated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnauthenticated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnauthenticated.Location = new System.Drawing.Point(3, 3);
            this.dgvUnauthenticated.Name = "dgvUnauthenticated";
            this.dgvUnauthenticated.ReadOnly = true;
            this.dgvUnauthenticated.Size = new System.Drawing.Size(786, 318);
            this.dgvUnauthenticated.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvAuthenticated);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Authenticated";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtSender);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 350);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 100);
            this.panel1.TabIndex = 0;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(343, 39);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "Log In";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(237, 41);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.Text = "password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(73, 41);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 20);
            this.txtUserName.TabIndex = 8;
            this.txtUserName.Text = "username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "UserName";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(343, 13);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(237, 15);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(100, 20);
            this.txtMessage.TabIndex = 5;
            this.txtMessage.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Message";
            // 
            // txtSender
            // 
            this.txtSender.Location = new System.Drawing.Point(73, 15);
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(100, 20);
            this.txtSender.TabIndex = 3;
            this.txtSender.Text = "Sender";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sender";
            // 
            // dgvAuthenticated
            // 
            this.dgvAuthenticated.AllowUserToAddRows = false;
            this.dgvAuthenticated.AllowUserToDeleteRows = false;
            this.dgvAuthenticated.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuthenticated.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuthenticated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuthenticated.Location = new System.Drawing.Point(3, 3);
            this.dgvAuthenticated.Name = "dgvAuthenticated";
            this.dgvAuthenticated.ReadOnly = true;
            this.dgvAuthenticated.Size = new System.Drawing.Size(786, 318);
            this.dgvAuthenticated.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tcConsumer);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tcConsumer.ResumeLayout(false);
            this.tpUnauthenticated.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnauthenticated)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthenticated)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcConsumer;
        private System.Windows.Forms.TabPage tpUnauthenticated;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvUnauthenticated;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvAuthenticated;
    }
}

