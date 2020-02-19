using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;           //匯入網路通訊協定相關函數
using System.Net.Sockets;   //匯入網路插座功能函數

namespace ClientAP
{
    public partial class Form1 : Form
    {
        //公用變數
        Socket T;           //通訊物件
        string User;        //使用者

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
    
        //傳送訊息給 Server (Send Message to the Server)
        private void Send(string Str )
        {
            try
            {
                byte[] B = Encoding.Default.GetBytes(Str);     //翻譯字串Str為Byte陣列B
                int ret = T.Send( B, 0, B.Length, SocketFlags.None);      //使用連線物件傳送資料
            }
            catch { }
            //MessageBox.Show(ret.ToString());
        }
    
        private void Button1_Click(object sender, EventArgs e)
        {
            string IP = TextBox1.Text;                                  //伺服器IP
            int Port = int.Parse(TextBox2.Text);                        //伺服器Port
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);  //伺服器的連線端點資訊
            //建立可以雙向通訊的TCP連線
            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            User = TextBox3.Text;                                       //使用者名稱
            timer1.Enabled = true;
            
            try
            {
                T.Connect(EP);                  //連上伺服器的端點EP(類似撥號給電話總機)
                Send("0" + User);               //連線後隨即傳送自己的名稱給伺服器
            }
            catch (Exception)
            {
                MessageBox.Show("無法連上伺服器！"); //連線失敗時顯示訊息
                return;
            }

            Button1.Enabled = false; //讓連線按鍵失效，避免重複連線
            Button3.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Button1.Enabled == false)
            {
                Send("9" + User); //傳送自己的離線訊息給伺服器
                T.Close(); //關閉網路通訊器T
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string IP = TextBox1.Text;                                  //伺服器IP
                int Port = int.Parse(TextBox2.Text);                        //伺服器Port
                IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);  //伺服器的連線端點資訊
                //建立可以雙向通訊的TCP連線
                T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                T.Connect(EP);
                if(Button2.BackColor != Color.Green)
                Button2.BackColor = Color.Green;
            }
            catch
            {
                if (Button2.BackColor != Color.Red)
                Button2.BackColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Button1.Enabled == false)
            {
                Send("9" + User); //傳送自己的離線訊息給伺服器
                T.Close();        //關閉網路通訊器T
                Button1.Enabled = true;
                Button3.Enabled = false;
            }
        }
    }
}
