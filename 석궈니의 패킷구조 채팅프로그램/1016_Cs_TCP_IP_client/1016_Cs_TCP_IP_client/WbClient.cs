using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016_Cs_TCP_IP_client
{
    public class WbClient
    {
        #region 맴버 필드 및 프로퍼티
        public IPEndPoint ipep;
        public Socket server;
        public bool isConnect = false;

        private Form1 form;


        #endregion

        #region 연결(쓰레드 생성) / 해제
        public bool Connect(string ip, int port)
        {
            try
            {
                ipep = new IPEndPoint(IPAddress.Parse(ip), port);

                server = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream, ProtocolType.Tcp);
                server.Connect(ipep);

                isConnect = true;

                Thread th = new Thread(new ParameterizedThreadStart(ClientThread));
                th.Start(this);
                th.IsBackground = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public void DisConnect()
        {
            isConnect = false;
            server.Close();
        }
        #endregion

        #region 폼1 접근
        public void ParentInfo(Form1 f)
        {
            form = f;
        }
        #endregion

        #region 기능

       

        public void SendPacket(string msg)
        {
            try
            {
                if (server.Connected)
                {
                    byte[] data = Encoding.Default.GetBytes(msg);
                    this.SendData(data);
                }
                else
                {
                    MessageBox.Show("서버 미연결");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } //패킷전달용

        private void SendData(byte[] data)
        {
            try
            {
                int total = 0;
                int size = data.Length;
                int left_data = size;
                int send_data = 0;

                // 전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                data_size = BitConverter.GetBytes(size);
                send_data = this.server.Send(data_size);

                // 실제 데이터 전송
                while (total < size)
                {
                    send_data = this.server.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 

        public void ReceiveData(ref byte[] rcvdata)
        {
            try
            {
                int total = 0;
                int size = 0;
                int left_data = 0;
                int recv_data = 0;
                // 수신할 데이터 크기 알아내기   
                byte[] data_size = new byte[4];
                recv_data = this.server.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                rcvdata = new byte[size];
                // 서버에서 전송한 실제 데이터 수신
                while (total < size)
                {
                    recv_data = this.server.Receive(rcvdata, total, left_data, 0);
                    if (recv_data == 0) break;
                    total += recv_data;
                    left_data -= recv_data;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        #region Tread
        public void ClientThread(object data)
        {
          while (true)
          {
             byte[] rcvdata = null;

             ReceiveData(ref rcvdata);
             form.PaserByteData(rcvdata);
             //form.msgPrint(Encoding.Default.GetString(rcvdata));
          }
        }
        #endregion

    }
}
