using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016_Cs_TCP_IP_client
{
    public partial class NewMemberForm : Form
    {

        #region 속성 / 프로퍼티 / 생성자

        public string ID { get; private set;}
        public string PW { get; private set;}
        public string NAME { get; private set; }
        public int AGE { get; private set; }

        public NewMemberForm()
        {
            InitializeComponent();
        }
        #endregion


        //버튼_회원가입
        private void button1_Click(object sender, EventArgs e)
        {
            ID = textBox1.Text;
            PW = textBox2.Text;
            NAME = textBox3.Text;
            AGE = int.Parse(textBox4.Text);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

       
    }
}
