namespace SERVER_SOCKET_CHAT
{
    partial class Frm_Main
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
            this.btn_start_server = new System.Windows.Forms.Button();
            this.btn_stop_server = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_send_msg_to_all = new System.Windows.Forms.Button();
            this.btn_thoat = new System.Windows.Forms.Button();
            this.list_user_online = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.list_user_online)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_start_server
            // 
            this.btn_start_server.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start_server.Location = new System.Drawing.Point(12, 12);
            this.btn_start_server.Name = "btn_start_server";
            this.btn_start_server.Size = new System.Drawing.Size(138, 37);
            this.btn_start_server.TabIndex = 2;
            this.btn_start_server.Text = "START SERVER";
            this.btn_start_server.UseVisualStyleBackColor = true;
            this.btn_start_server.Click += new System.EventHandler(this.btn_start_server_Click);
            // 
            // btn_stop_server
            // 
            this.btn_stop_server.Enabled = false;
            this.btn_stop_server.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_stop_server.Location = new System.Drawing.Point(156, 12);
            this.btn_stop_server.Name = "btn_stop_server";
            this.btn_stop_server.Size = new System.Drawing.Size(135, 37);
            this.btn_stop_server.TabIndex = 3;
            this.btn_stop_server.Text = "STOP SERVER";
            this.btn_stop_server.UseVisualStyleBackColor = true;
            this.btn_stop_server.Click += new System.EventHandler(this.btn_stop_server_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "DANH SÁCH USER ĐANG ONLINE";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(12, 292);
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(402, 23);
            this.txt_msg.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(292, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "GỬI TIN NHẮN ĐẾN NHỮNG USER ĐANG ONLINE";
            // 
            // btn_send_msg_to_all
            // 
            this.btn_send_msg_to_all.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send_msg_to_all.Location = new System.Drawing.Point(422, 273);
            this.btn_send_msg_to_all.Name = "btn_send_msg_to_all";
            this.btn_send_msg_to_all.Size = new System.Drawing.Size(76, 42);
            this.btn_send_msg_to_all.TabIndex = 6;
            this.btn_send_msg_to_all.Text = "GỬI ĐI";
            this.btn_send_msg_to_all.UseVisualStyleBackColor = true;
            this.btn_send_msg_to_all.Click += new System.EventHandler(this.btn_send_msg_to_all_Click);
            // 
            // btn_thoat
            // 
            this.btn_thoat.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thoat.Location = new System.Drawing.Point(362, 12);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(136, 37);
            this.btn_thoat.TabIndex = 3;
            this.btn_thoat.Text = "THOÁT";
            this.btn_thoat.UseVisualStyleBackColor = true;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // list_user_online
            // 
            this.list_user_online.AllowUserToAddRows = false;
            this.list_user_online.AllowUserToDeleteRows = false;
            this.list_user_online.AllowUserToResizeRows = false;
            this.list_user_online.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.list_user_online.BackgroundColor = System.Drawing.Color.White;
            this.list_user_online.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.list_user_online.Location = new System.Drawing.Point(12, 80);
            this.list_user_online.MultiSelect = false;
            this.list_user_online.Name = "list_user_online";
            this.list_user_online.ReadOnly = true;
            this.list_user_online.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.list_user_online.Size = new System.Drawing.Size(486, 189);
            this.list_user_online.TabIndex = 7;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 326);
            this.Controls.Add(this.list_user_online);
            this.Controls.Add(this.btn_send_msg_to_all);
            this.Controls.Add(this.txt_msg);
            this.Controls.Add(this.btn_thoat);
            this.Controls.Add(this.btn_stop_server);
            this.Controls.Add(this.btn_start_server);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SERVER - SOCKET CHAT";
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list_user_online)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start_server;
        private System.Windows.Forms.Button btn_stop_server;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txt_msg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_send_msg_to_all;
        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.DataGridView list_user_online;
    }
}

