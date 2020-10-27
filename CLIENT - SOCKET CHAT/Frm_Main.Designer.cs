namespace CLIENT_SOCKET_CHAT
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
            this.btn_thoat = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btn_get_list_user_online = new System.Windows.Forms.Button();
            this.list_user_online = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.list_user_online)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_thoat
            // 
            this.btn_thoat.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thoat.Location = new System.Drawing.Point(389, 220);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(109, 35);
            this.btn_thoat.TabIndex = 3;
            this.btn_thoat.Text = "THOÁT";
            this.btn_thoat.UseVisualStyleBackColor = true;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // btn_get_list_user_online
            // 
            this.btn_get_list_user_online.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_get_list_user_online.Location = new System.Drawing.Point(12, 220);
            this.btn_get_list_user_online.Name = "btn_get_list_user_online";
            this.btn_get_list_user_online.Size = new System.Drawing.Size(237, 35);
            this.btn_get_list_user_online.TabIndex = 4;
            this.btn_get_list_user_online.Text = "LẤY DANH SÁCH USER ONLINE";
            this.btn_get_list_user_online.UseVisualStyleBackColor = true;
            this.btn_get_list_user_online.Click += new System.EventHandler(this.btn_get_list_user_online_Click);
            // 
            // list_user_online
            // 
            this.list_user_online.AllowUserToAddRows = false;
            this.list_user_online.AllowUserToDeleteRows = false;
            this.list_user_online.AllowUserToResizeRows = false;
            this.list_user_online.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.list_user_online.BackgroundColor = System.Drawing.Color.White;
            this.list_user_online.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.list_user_online.Location = new System.Drawing.Point(12, 12);
            this.list_user_online.MultiSelect = false;
            this.list_user_online.Name = "list_user_online";
            this.list_user_online.ReadOnly = true;
            this.list_user_online.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.list_user_online.Size = new System.Drawing.Size(486, 202);
            this.list_user_online.TabIndex = 8;
            this.list_user_online.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.list_user_online_CellDoubleClick);
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 265);
            this.Controls.Add(this.list_user_online);
            this.Controls.Add(this.btn_get_list_user_online);
            this.Controls.Add(this.btn_thoat);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIENT - SOCKET CHAT";
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.list_user_online)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_thoat;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_get_list_user_online;
        private System.Windows.Forms.DataGridView list_user_online;
    }
}

