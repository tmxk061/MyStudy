using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016_Cs_TCP_IP_client
{
    public partial class Form1 : Form
    {

        #region 속성 / 프로퍼티 / 생성자
        WbClient client = new WbClient();
        Form2 form2 = new Form2();
        NewMemberForm newMem = new NewMemberForm();
        Packet pak = Packet.Instance;

        public string User_Id { get; set; }
        public string User_Name { get; set; }

        public bool IsLogin = false;

        public Form1()
        {
            InitializeComponent();
            client.ParentInfo(this);

        }
        #endregion

        #region 다이얼로그 LOAD / CLOSED
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
            LogOutUI();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client.isConnect == true)
            {
                client.DisConnect();
            }
        }
        #endregion

        #region 서버연결 / 해제
        //메뉴_서버연결
        private void 프로그램종료ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            string temp = string.Empty;

            if (form2.ShowDialog() == DialogResult.OK)
            {
                if (client.Connect(form2.IP, form2.PORT))
                {
                    UI.FillDrawing(panel1, Color.Green);
                    UI.LabelState(label1, "서버연결");

                    temp = String.Format("[연결] {0}:{1} 성공",
                        form2.IP, form2.PORT);
                }
                else
                {
                    temp = String.Format("[연결] {0}:{1} 실패",
                            form2.IP, form2.PORT);
                }
            }

            UI.LogPrint(listBox1, temp);

        }

        //메뉴_서버연결해제
        private void 서버연결해제ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.DisConnect();
            UI.FillDrawing(panel1, Color.Red);
            UI.LabelState(label1, "서버연결 해제");

            string temp = String.Format("[해제] {0}:{1} 성공",
                       form2.IP, form2.PORT);

            UI.LogPrint(listBox1, temp);

        }
        #endregion

        #region 능동 기능
        
       
        //메뉴_회원가입
        private void 회원가입ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (newMem.ShowDialog() == DialogResult.OK)
            {
                string memberData = pak.GetNewMember(newMem.ID, newMem.PW, newMem.NAME, newMem.AGE);
                client.SendPacket(memberData);
            }
        }

        //버튼_로그인
        private void button7_Click(object sender, EventArgs e)
        {
            string LoginData = pak.GetLOGIN(textBox7.Text, textBox8.Text);
            client.SendPacket(LoginData);
        }

        //버튼_로그아웃
        private void button6_Click(object sender, EventArgs e)
        {
            string LogoutData = pak.GetLOGOUT(textBox7.Text);
            client.SendPacket(LogoutData);
        }

        //버튼_계정삭제
        private void button5_Click(object sender, EventArgs e)
        {
            string DelData = pak.GetDelMember(textBox7.Text);
            client.SendPacket(DelData);
        }

        //버튼_채팅 메시지 전송
        private void button1_Click(object sender, EventArgs e)
        {
            if (client.isConnect == false)
                return;

            if (IsLogin == false)
                return;

            string MsgData = pak.GetMESSAGE(textBox1.Text,textBox2.Text);
            client.SendPacket(MsgData);
            //client.Send(textBox1.Text, textBox2.Text);
        }
        #endregion

        #region 수동 기능

        //채팅 메시지 업데이트
        public void msgPrint(string msg)
        {
            UI.LogPrint(listBox2, msg);
        }


        #endregion



        #region 수신 패킷 분석 및 처리
        public void PaserByteData(byte[] data)
        {
            string msg = Encoding.Default.GetString(data);
            string[] token = msg.Split('@');
            switch (token[0].Trim())
            {
                case "NEWMEMBERACK": NewMemberAck(token[1]); break;
                case "DELMEMBERACK": DelMemberAck(token[1]); break;
                case "LOGINACK": LogInAck(token[1]); break;
                case "LOGOUTACK": LogOutAck(token[1]); break;
                case "MESSAGEACK": MessageAck(token[1]); break;
            }
        }
       
        void NewMemberAck(string msg)
        {
            string[] sp = msg.Split('#');
            if (sp[0] == "True")
            {
                MessageBox.Show("성공");
            }
            else 
                MessageBox.Show("실패");
        }

        void DelMemberAck(string msg)
        {
            string[] sp = msg.Split('#');
            if (sp[0] == "True")
            {
                MessageBox.Show("삭제성공");
                this.Text = sp[2] + "가 " +  "삭제되었습니다.";
            }
            else
                MessageBox.Show("삭제실패");
        }

        void LogInAck(string msg)
        {
            string[] sp = msg.Split('#');
            if (sp[0] == "True")
            {
                IsLogin = true;
                MessageBox.Show("로그인 성공");

                User_Id = sp[1];
                User_Name = sp[2];

                this.Text = User_Name + "님 이 로그인 하셨습니다.";
                textBox1.Text = User_Name;
                LoginUI();
            }
            else
                MessageBox.Show("로그인 실패");
        }

        void LogOutAck(string msg)
        {
           
            string[] sp = msg.Split('#');
            if (sp[0] == "True")
            {
                IsLogin = false;
                MessageBox.Show("로그아웃 성공");
                LogOutUI();
            }
            else
                MessageBox.Show("로그아웃 실패");
        }

        void MessageAck(string msg)
        {
            if (IsLogin == false)
                return;

            string[] sp = msg.Split('#');
            UI.LogPrint(listBox2, string.Format("[{0}]{1}", sp[0], sp[1]));
        }
        #endregion



        #region UI컨트롤
        void LoginUI()
        {
            UI.ControllEnabled(button7, false);
            UI.ControllEnabled(button6, true);
            UI.ControllEnabled(button1, true);
            UI.ControllEnabled(textBox1, true);
            UI.ControllEnabled(textBox2, true);
            UI.ControllEnabled(listBox2, true);
        }

        void LogOutUI()
        {
            UI.ControllEnabled(button7, true);
            UI.ControllEnabled(button6, false);
            UI.ControllEnabled(button1, false);
            UI.ControllEnabled(textBox1, false);
            UI.ControllEnabled(textBox2, false);
            UI.ControllEnabled(listBox2, false);
        }
        #endregion


    }
}
