using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SERVER_SOCKET_CHAT
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Main());
        }
    }
}
