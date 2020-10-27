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
    public partial class Frm_Main : Form
    {
        List<Frm_CHAT> List_Form_Chat = new List<Frm_CHAT>();

        Frm_Alert_From_Server FAlert;

        public bool Status_Login = false;

        public string User_Client;

        public string IP_Main_Server = "127.0.0.1";
        public Int32 Port_Main_Server = 10000;

        string IP_Client = "127.0.0.1";
        Int32 Port_Client = 11000;

        Socket Listener;

        public Frm_Main()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            if (Listener != null)
            {
                Listener.Close();
            }
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Create_Server(IP_Client, Port_Client);
        }

        string Xu_Ly_Du_Lieu(string base64_data)
        {
            string data_return;
            string[] data1, data2;
            string User_Server, User_Client, Msg;
            string IP_Server;
            Int32 Port_Server = 0;

            data1 = (string[])Deserialize_From_String(base64_data);
            if (data1 == null) { return "DỮ LIỆU NHẬN ĐƯỢC BỊ LỖI"; }

            string command = data1[0];
            switch (command)
            {
                case "chat":
                    {
                        data2 = (string[])Deserialize_From_String(data1[1]);
                        if (data2 == null) { return "DỮ LIỆU NHẬN ĐƯỢC BỊ LỖI"; }

                        User_Server = data2[0].Trim();
                        User_Client = data2[1].Trim();
                        IP_Server = data2[2].Trim();
                        Port_Server = Int32.Parse(data2[3].Trim());
                        Msg = data2[4].Trim();

                        // KIỂM TRA THÔNG TIN NHẬN ĐƯỢC

                        if (User_Server == "" || User_Client == "" || IP_Server == "" || Port_Server == 0 || Msg == "")
                        {
                            return "THÔNG TIN KHÔNG ĐẦY ĐỦ";
                        }

                        // THÊM NỘI DUNG CHAT VÀO FORM CHAT
                        
                        string Form_ID = (User_Client + User_Server).ToLower();
                        
                        if (Form_ID != "")
                        {
                            if (Search_Form_Chat(Form_ID) == false)
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    Frm_CHAT FCHAT = new Frm_CHAT();
                                    FCHAT.FrmMain = this;
                                    FCHAT.Form_ID = Form_ID;
                                    FCHAT.User_Client = User_Client;
                                    FCHAT.User_Server = User_Server;
                                    FCHAT.IP_Server = IP_Server;
                                    FCHAT.Port_Server = Port_Server;
                                    FCHAT.IP_Client = IP_Client;
                                    FCHAT.Port_Client = Port_Client;
                                    FCHAT.Msg = Msg;
                                    List_Form_Chat.Add(FCHAT);
                                    FCHAT.Show();
                                });
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    Active_Form_Chat(Form_ID, Msg);
                                });
                            }
                        }

                        data_return = "OK";
                        break;
                    }
                case "alert_from_server":
                    {
                        Msg = data1[1].Trim();
                        this.Invoke((MethodInvoker)delegate()
                        {
                            if (FAlert != null)
                            {
                                FAlert.Close();
                                FAlert = new Frm_Alert_From_Server();
                                FAlert.Msg = Msg;
                                FAlert.Show();
                            }
                            else
                            {
                                FAlert = new Frm_Alert_From_Server();
                                FAlert.Msg = Msg;
                                FAlert.Show();
                            }
                        });
                        data_return = "OK";
                        break;
                    }
                default:
                    {
                        data_return = "LỆNH KHÔNG HỢP LỆ";
                        break;
                    }
            }
            return data_return;
        }

        private void Create_Server(string ip_server, Int32 port)
        {
            string data = null;
            byte[] bytes = new Byte[1024];

            IPAddress ipAddress = IPAddress.Parse(ip_server);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Listener.Bind(localEndPoint);
                Listener.Listen(10);

                while (true)
                {
                    Socket handler = Listener.Accept();
                    data = null;

                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }

                    data = Xu_Ly_Du_Lieu(data.Substring(0, data.Length - 5));

                    byte[] msg = Encoding.UTF8.GetBytes(data + "<EOF>");

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

        bool Check_Port_Is_Available(string ip_address, Int32 port)
        {
            try
            {
                TcpClient TcpClient = new TcpClient();
                IPAddress ipAddress = IPAddress.Parse(ip_address);
                TcpClient.Connect(ipAddress, port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
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

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            // KIỂM TRA ĐĂNG NHẬP

            if (Status_Login == false)
            {
                this.Hide();
                Frm_Login_Register FLogin = new Frm_Login_Register();
                FLogin.IP_Main_Server = IP_Main_Server;
                FLogin.Port_Main_Server = Port_Main_Server;
                FLogin.Port_Client = Port_Client;
                FLogin.FMain = this;
                FLogin.ShowDialog();
            }

            if (Status_Login == false || User_Client == "")
            {
                MessageBox.Show("BẠN CHƯA ĐĂNG NHẬP", "THÔNG BÁO");
                this.Close();
            }

            // KIỂM TRA TÌNH TRẠNG SỬ DỤNG CỦA PORT

            if (Check_Port_Is_Available(IP_Client, Port_Client) == true)
            {
                MessageBox.Show("PORT NÀY ĐÃ ĐƯỢC SỬ DỤNG. XIN CHỌN PORT KHÁC");
                Application.Exit();
                return;
            }

            // TẠO MÁY CHỦ

            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void btn_get_list_user_online_Click(object sender, EventArgs e)
        {
            string[] data_send_to_server = { "COMMAND", "OBJECT" };

            data_send_to_server[0] = "get_list_users_online";
            data_send_to_server[1] = null;

            DataTable DT = new DataTable();

            string msg_from_server = Send_Data(IP_Main_Server, Port_Main_Server, data_send_to_server);
            try
            {
                DT = (DataTable)Deserialize_From_String(msg_from_server);
                if (DT.Rows.Count != 0)
                {
                    list_user_online.DataSource = DT;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("DỮ LIỆU NHẬN ĐƯỢC BỊ LỖI", "THÔNG BÁO");
            }
        }

        private void list_user_online_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string User_Server;

            string IP_Server;
            Int32 Port_Server;

            User_Server = list_user_online.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            IP_Server = list_user_online.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            Port_Server = Int32.Parse(list_user_online.Rows[e.RowIndex].Cells[2].Value.ToString().Trim());

            if (User_Client == User_Server)
            {
                MessageBox.Show("BẠN KHÔNG THỂ CHAT VỚI CHÍNH MÌNH", "THÔNG BÁO");
                return;
            }

            if (User_Client == "" || User_Server == "") { return; }

            string Form_ID = (User_Client + User_Server).ToLower();

            if (Form_ID != "")
            {
                if (Search_Form_Chat(Form_ID) == false)
                {
                    Frm_CHAT FCHAT = new Frm_CHAT();
                    FCHAT.FrmMain = this;
                    FCHAT.Form_ID = Form_ID;
                    FCHAT.User_Client = User_Client;
                    FCHAT.User_Server = User_Server;
                    FCHAT.IP_Server = IP_Server;
                    FCHAT.Port_Server = Port_Server;
                    FCHAT.IP_Client = IP_Client;
                    FCHAT.Port_Client = Port_Client;
                    FCHAT.Msg = "";
                    List_Form_Chat.Add(FCHAT);
                    FCHAT.Show();
                }
                else
                {
                    Active_Form_Chat(Form_ID, "");
                }
            }
        }

        bool Search_Form_Chat(string Form_ID)
        {
            for (int i = 0; i < List_Form_Chat.Count; i++)
            {
                if (List_Form_Chat[i].Form_ID.Trim().ToLower() == Form_ID.Trim().ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public void Remove_Item_From_List_Form_Chat(string Form_ID)
        {
            for (int i = 0; i < List_Form_Chat.Count; i++)
            {
                if (List_Form_Chat[i].Form_ID.Trim().ToLower() == Form_ID.Trim().ToLower())
                {
                    List_Form_Chat.RemoveAt(i);
                }
            }
        }

        public void Active_Form_Chat(string Form_ID, string Msg)
        {
            for (int i = 0; i < List_Form_Chat.Count; i++)
            {
                if (List_Form_Chat[i].Form_ID.Trim().ToLower() == Form_ID.Trim().ToLower())
                {
                    List_Form_Chat[i].Add_Msg_To_List(Msg);
                    List_Form_Chat[i].Activate();
                }
            }
        }
    }
}
