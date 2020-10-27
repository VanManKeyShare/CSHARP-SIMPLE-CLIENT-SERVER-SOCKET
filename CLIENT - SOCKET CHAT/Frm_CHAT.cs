using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;

namespace CLIENT_SOCKET_CHAT
{
    public partial class Frm_CHAT : Form
    {
        public Frm_Main FrmMain;

        public string Form_ID;
        public string Msg = "";
        public string User_Client, User_Server;

        public string IP_Server;
        public Int32 Port_Server;

        public string IP_Client;
        public Int32 Port_Client;

        public Frm_CHAT()
        {
            InitializeComponent();
        }

        private void Frm_CHAT_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmMain.Remove_Item_From_List_Form_Chat(Form_ID);
        }

        private void Frm_CHAT_Load(object sender, EventArgs e)
        {
            this.Text = this.Text.Replace("<User_Client>", User_Client);
            this.Text = this.Text.Replace("<User_Server>", User_Server);
            
            if (Msg.Trim() != "")
            {
                list_msg.Items.Add(User_Server + ": " + Msg);
                Msg = "";
            }
        }

        public void Add_Msg_To_List(string Msg)
        {
            if (Msg.Trim() != "")
            {
                list_msg.Items.Add(User_Server + ": " + Msg.Trim());
            }
        }

        public string Send_Data(string ip_server, Int32 port_server, string[] data)
        {
            string data_received = "";
            byte[] bytes = new byte[1024];

            string data_send_to_server = Serialize_To_String(data);

            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip_server);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port_server);

                Socket sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    byte[] msg = Encoding.UTF8.GetBytes(data_send_to_server + "<EOF>");

                    int bytesSent = sender.Send(msg);

                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = sender.Receive(bytes);
                        data_received += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        if (data_received.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    data_received = data_received.Substring(0, data_received.Length - 5);

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine(ane.ToString());
                    data_received = "KẾT NỐI THẤT BẠI";
                }
                catch (SocketException se)
                {
                    Console.WriteLine(se.ToString());
                    data_received = "KẾT NỐI THẤT BẠI";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    data_received = "KẾT NỐI THẤT BẠI";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                data_received = "KẾT NỐI THẤT BẠI";
            }

            return data_received;
        }

        public string Serialize_To_String(object DATA)
        {
            if (DATA == null)
            {
                return string.Empty;
            }
            else
            {
                System.IO.MemoryStream MEMORY_STREAM = new System.IO.MemoryStream();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BINARY_FORMATTER = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                BINARY_FORMATTER.Serialize(MEMORY_STREAM, DATA);
                return System.Convert.ToBase64String(MEMORY_STREAM.GetBuffer());
            }
        }

        public object Deserialize_From_String(string BIN_STRING)
        {
            if (BIN_STRING == null)
            {
                return null;
            }
            else
            {
                if (BIN_STRING.Length == 0)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        byte[] BIN_DATA = System.Convert.FromBase64String(BIN_STRING);
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BINARY_FORMATTER = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        System.IO.MemoryStream MEMORY_STREAM = new System.IO.MemoryStream(BIN_DATA);
                        return BINARY_FORMATTER.Deserialize(MEMORY_STREAM);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string Msg = txt_msg.Text.Trim();
            if (Msg != "")
            {
                list_msg.Items.Add(User_Client + ": " + Msg.Trim());
                txt_msg.Text = "";

                string[] data1 = { "COMMAND", "OBJECT" };
                string[] data2 = { "USER_CLIENT", "USER_SERVER", "IP_CLIENT", "PORT_CLIENT", "MSG" };

                data2[0] = User_Client;
                data2[1] = User_Server;
                data2[2] = IP_Client;
                data2[3] = Port_Client.ToString();
                data2[4] = Msg;

                data1[0] = "chat";
                data1[1] = Serialize_To_String(data2);

                string msg_from_server = Send_Data(IP_Server, Port_Server, data1);
                if (msg_from_server != "OK")
                {
                    MessageBox.Show(msg_from_server);
                }
            }
        }

        private void txt_msg_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_msg.Text.Trim() != "")
            {
                btn_send.Enabled = true;
            }
            else
            {
                btn_send.Enabled = false;
            }
        }
    }
}
