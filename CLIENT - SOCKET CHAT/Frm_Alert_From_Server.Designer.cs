namespace CLIENT_SOCKET_CHAT
{
    partial class Frm_Alert_From_Server
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
            this.txt_msg_from_server = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_msg_from_server
            // 
            this.txt_msg_from_server.Location = new System.Drawing.Point(12, 12);
            this.txt_msg_from_server.Multiline = true;
            this.txt_msg_from_server.Name = "txt_msg_from_server";
            this.txt_msg_from_server.Size = new System.Drawing.Size(555, 135);
            this.txt_msg_from_server.TabIndex = 0;
            // 
            // Frm_Alert_From_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 158);
            this.Controls.Add(this.txt_msg_from_server);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Frm_Alert_From_Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message From Server";
            this.Load += new System.EventHandler(this.Frm_Alert_From_Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_msg_from_server;
    }
}