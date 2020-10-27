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
using System.Data.SqlClient;

namespace SERVER_SOCKET_CHAT
{
    public partial class Frm_Main : Form
    {
        string Sql_Connetion_String = @"Data Source=.\SQLEXPRESS;Initial Catalog=CHAT_SOCKET;Integrated Security=True;";
        SqlConnection Sql_Conn;
        SqlCommand Sql_Cmd;
        String Sql_Query;
        SqlDataReader Sql_Data_Reader;
        DataTable DT;

        string IP_Server = "127.0.0.1";
        Int32 Port_Server = 10000;

        Socket Listener;

        public Frm_Main()
        {
            InitializeComponent();
        }

        private void btn_start_server_Click(object sender, EventArgs e)
        {
            // KIỂM TRA TÌNH TRẠNG SỬ DỤNG CỦA PORT

            if (Check_Port_Is_Available(IP_Server, Port_Server) == true)
            {
                MessageBox.Show("PORT này đã được sử dụng. Xin chọn PORT khác");
                return;
            }

            // TẠO SERVER

            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
                btn_start_server.Enabled = false;
                btn_stop_server.Enabled = true;
            }
        }

        private void btn_stop_server_Click(object sender, EventArgs e)
        {
            Listener.Close();
            btn_start_server.Enabled = true;
            btn_stop_server.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Create_Server(IP_Server, Port_Server);
        }

        string Xu_Ly_Du_Lieu(string base64_data, string ip_client)
        {
            string data_return;
            string[] data1, data2;
            string Username, Password;

            data1 = (string[])Deserialize_From_String(base64_data);
            if (data1 == null) { return "DỮ LIỆU NHẬN ĐƯỢC BỊ LỖI"; }

            string command = data1[0];
            switch (command)
            {
                case "login":
                    {
                        data2 = (string[])Deserialize_From_String(data1[1]);
                        if (data2 == null) { return "DỮ LIỆU NHẬN ĐƯỢC BỊ LỖI"; }
                        Username = data2[0].Trim();
                        Password = data2[1].Trim();
                        string port_client = data2[2].Trim();

                        if (Username == "" || Password == "") { return "USERNAME VÀ PASSWORD KHÔNG ĐƯỢC RỖNG"; }

                        // KIỂM TRA THÔNG TIN ĐĂNG NHẬP CÓ ĐÚNG TRONG CSDL KHÔNG

                        Sql_Query = "SELECT PASSWORD FROM ACCOUNTS WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD";

                        DT = new DataTable();
                        Sql_Conn = new SqlConnection(Sql_Connetion_String);
                        try
                        {
                            Sql_Conn.Open();
                            Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                            Sql_Cmd.Parameters.AddWithValue("USERNAME", Username);
                            Sql_Cmd.Parameters.AddWithValue("PASSWORD", Password);
                            Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                            DT.Load(Sql_Data_Reader);
                            Sql_Cmd.Dispose();
                            Sql_Conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            return ex.ToString();
                        }

                        if (DT.Rows.Count == 0)
                        {
                            return "USERNAME HOẶC PASSWORD KHÔNG ĐÚNG";
                        }

                        // NẾU ĐÚNG THÌ TIẾN HÀNH THÊM VÀO DANH SÁCH ONLINE
                        // NẾU CHƯA CÓ TRONG DANH SÁCH ONLINE THÌ THÊM VÀO DANH SÁCH ONLINE

                        bool user_online_exists = false;

                        foreach (DataGridViewRow row in list_user_online.Rows)
                        {
                            if (row.Cells[0].Value.ToString().ToLower() == Username.ToLower())
                            {
                                user_online_exists = true;
                                break;
                            }
                        }

                        if (user_online_exists == false)
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                string[] dgvr = new string[] { Username, ip_client, port_client, DateTime.Now.ToString() };
                                list_user_online.Rows.Add(dgvr);
                            });
                        }

                        // TRẢ KẾT QUẢ ĐĂNG NHẬP THÀNH CÔNG VỀ CHO CLIENT

                        data_return = "OK";
                        break;
                    }
                case "register":
                    {
                        data2 = (string[])Deserialize_From_String(data1[1]);
                        if (data2 == null) { return "DỮ LIỆU NHẬN ĐƯỢC BỊ LỖI"; }
                        Username = data2[0].Trim();
                        Password = data2[1].Trim();

                        if (Username == "" || Password == "") { return "USERNAME VÀ PASSWORD KHÔNG ĐƯỢC RỖNG"; }

                        // KIỂM TRA USER ĐÃ CÓ ĐĂNG KÝ CHƯA

                        Sql_Query = "SELECT USERNAME FROM ACCOUNTS WHERE USERNAME = @USERNAME";

                        DT = new DataTable();
                        Sql_Conn = new SqlConnection(Sql_Connetion_String);
                        try
                        {
                            Sql_Conn.Open();
                            Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                            Sql_Cmd.Parameters.AddWithValue("USERNAME", Username);
                            Sql_Data_Reader = Sql_Cmd.ExecuteReader();
                            DT.Load(Sql_Data_Reader);
                            Sql_Cmd.Dispose();
                            Sql_Conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            return ex.ToString();
                        }

                        if (DT.Rows.Count != 0)
                        {
                            return "USERNAME NÀY ĐÃ ĐĂNG KÝ RỒI";
                        }

                        // NẾU CHƯA THÌ TIẾN HÀNH THÊM USER VÀO CSDL

                        Sql_Query = "INSERT INTO ACCOUNTS (USERNAME, PASSWORD) VALUES (@USERNAME, @PASSWORD)";

                        Sql_Conn = new SqlConnection(Sql_Connetion_String);
                        try
                        {
                            Sql_Conn.Open();
                            Sql_Cmd = new SqlCommand(Sql_Query, Sql_Conn);
                            Sql_Cmd.Parameters.AddWithValue("USERNAME", Username);
                            Sql_Cmd.Parameters.AddWithValue("PASSWORD", Password);
                            Sql_Cmd.ExecuteNonQuery();
                            Sql_Cmd.Dispose();
                            Sql_Conn.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            return ex.ToString();
                        }

                        // TRẢ KẾT QUẢ ĐÃ ĐĂNG KÝ THÀNH CÔNG VỀ CHO CLIENT

                        data_return = "OK";
                        break;
                    }
                case "get_list_users_online":
                    {
                        DataTable dt_list_user_online = new DataTable();
                        dt_list_user_online.Columns.Add("USERNAME", typeof(string));
                        dt_list_user_online.Columns.Add("IP", typeof(string));
                        dt_list_user_online.Columns.Add("PORT", typeof(string));

                        foreach (DataGridViewRow dgvR in list_user_online.Rows)
                        {
                            dt_list_user_online.Rows.Add(dgvR.Cells[0].Value, dgvR.Cells[1].Value, dgvR.Cells[2].Value);
                        }

                        data_return = Serialize_To_String(dt_list_user_online);
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

                    string ip_client = "";
                    IPEndPoint Remote_IpEndPoint = handler.RemoteEndPoint as IPEndPoint;

                    if (Remote_IpEndPoint != null)
                    {
                        ip_client = Remote_IpEndPoint.Address.ToString();
                    }

                    data = Xu_Ly_Du_Lieu(data.Substring(0, data.Length - 5), ip_client);

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

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            list_user_online.ColumnCount = 4;
            list_user_online.Columns[0].Name = "USERNAME";
            list_user_online.Columns[1].Name = "IP";
            list_user_online.Columns[2].Name = "PORT";
            list_user_online.Columns[3].Name = "TIME LOGIN";
        }

        private void btn_send_msg_to_all_Click(object sender, EventArgs e)
        {
            string User_Client, IP_Client;
            Int32 Port_Client = 0;

            string Msg = txt_msg.Text.Trim();

            if (Msg != "" && list_user_online.Rows.Count != 0)
            {

                foreach (DataGridViewRow dgvr in list_user_online.Rows)
                {
                    User_Client = dgvr.Cells[0].Value.ToString().Trim();
                    IP_Client = dgvr.Cells[1].Value.ToString().Trim();
                    Port_Client = Int32.Parse(dgvr.Cells[2].Value.ToString().Trim());

                    if (User_Client == "" || IP_Client == "" || Port_Client == 0)
                    {
                        MessageBox.Show("THÔNG TIN CLIENT KHÔNG ĐẦY ĐỦ", "THÔNG BÁO");
                        return;
                    }

                    string[] data1 = { "alert_from_server", "Message" };
                    data1[1] = Msg;

                    string msg_from_client = Send_Data(IP_Client, Port_Client, data1);

                    if (msg_from_client != "OK")
                    {
                        MessageBox.Show(msg_from_client);
                    }
                }
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
    }
}
