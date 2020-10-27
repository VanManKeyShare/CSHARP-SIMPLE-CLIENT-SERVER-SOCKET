namespace CLIENT_SOCKET_CHAT
{
    partial class Frm_CHAT
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
            this.list_msg = new System.Windows.Forms.ListBox();
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_msg
            // 
            this.list_msg.FormattingEnabled = true;
            this.list_msg.ItemHeight = 16;
            this.list_msg.Location = new System.Drawing.Point(12, 12);
            this.list_msg.Name = "list_msg";
            this.list_msg.Size = new System.Drawing.Size(533, 244);
            this.list_msg.TabIndex = 0;
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(12, 278);
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(442, 23);
            this.txt_msg.TabIndex = 1;
            this.txt_msg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_msg_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "NỘI DUNG CHAT";
            // 
            // btn_send
            // 
            this.btn_send.Enabled = false;
            this.btn_send.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send.Location = new System.Drawing.Point(460, 259);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(85, 42);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "GỬI";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // Frm_CHAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 311);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_msg);
            this.Controls.Add(this.list_msg);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_CHAT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHAT - <User_Client> - <User_Server>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_CHAT_FormClosing);
            this.Load += new System.EventHandler(this.Frm_CHAT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_msg;
        private System.Windows.Forms.TextBox txt_msg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_send;
    }
}