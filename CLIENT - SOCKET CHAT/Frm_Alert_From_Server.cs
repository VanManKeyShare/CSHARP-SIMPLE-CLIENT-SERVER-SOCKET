using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CLIENT_SOCKET_CHAT
{
    public partial class Frm_Alert_From_Server : Form
    {
        public string Msg = "";

        public Frm_Alert_From_Server()
        {
            InitializeComponent();
        }

        private void Frm_Alert_From_Server_Load(object sender, EventArgs e)
        {
            txt_msg_from_server.Text = Msg;
        }
    }
}
