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
    public partial class Frm_Login_Register : Form
    {
        public Frm_Main FMain;

        public string IP_Main_Server;
        public Int32 Port_Main_Server;

        public Int32 Port_Client;

        public Frm_Login_Register()
        {
            InitializeComponent();
        }

        private void btn_dangky_Click(object sender, EventArgs e)
        {
            IP_Main_Server = txt_ip_server.Text.Trim();
            if (IP_Main_Server == "")
            {
                MessageBox.Show("IP SERVER KHÔNG ĐƯỢC RỖNG");
                return;
            }

            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("THÔNG TIN KHÔNG ĐẦY ĐỦ", "THÔNG BÁO");
                return;
            }

            string[] data_send_to_server = { "COMMAND", "OBJECT" };
            string[] thong_tin_user = { "USERNAME", "PASSWORD" };

            thong_tin_user[0] = username;
            thong_tin_user[1] = password;

            data_send_to_server[0] = "register";
            data_send_to_server[1] = Serialize_To_String(thong_tin_user);

            string msg_from_server = Send_Data(IP_Main_Server, Port_Main_Server, data_send_to_server);
            if (msg_from_server == "OK")
            {
                MessageBox.Show("ĐĂNG KÝ THÀNH CÔNG", "THÔNG BÁO");
            }
            else
            {
                MessageBox.Show(msg_from_server, "THÔNG BÁO");
            }
        }

        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            IP_Main_Server = txt_ip_server.Text.Trim();
            if (IP_Main_Server == "")
            {
                MessageBox.Show("IP SERVER KHÔNG ĐƯỢC RỖNG");
                return;
            }

            string username = txt_username.Text.Trim();
            string password = txt_password.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("THÔNG TIN KHÔNG ĐẦY ĐỦ","THÔNG BÁO");
                return;
            }

            string[] data_send_to_server = { "COMMAND", "OBJECT" };
            string[] thong_tin_user = { "USERNAME", "PASSWORD", "PORT" };

            thong_tin_user[0] = username;
            thong_tin_user[1] = password;
            thong_tin_user[2] = Port_Client.ToString();

            data_send_to_server[0] = "login";
            data_send_to_server[1] = Serialize_To_String(thong_tin_user);

            string msg_from_server = Send_Data(IP_Main_Server, Port_Main_Server, data_send_to_server);
            if (msg_from_server == "OK")
            {
                MessageBox.Show("ĐĂNG NHẬP THÀNH CÔNG", "THÔNG BÁO");
                FMain.IP_Main_Server = txt_ip_server.Text.Trim();
                FMain.Port_Main_Server = Int32.Parse(txt_port_server.Text.Trim());
                FMain.User_Client = txt_username.Text.Trim();
                FMain.Status_Login = true;
                FMain.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(msg_from_server, "THÔNG BÁO");
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

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            txt_ip_server.Text = IP_Main_Server;
            txt_port_server.Text = Port_Main_Server.ToString();
        }
    }
}
