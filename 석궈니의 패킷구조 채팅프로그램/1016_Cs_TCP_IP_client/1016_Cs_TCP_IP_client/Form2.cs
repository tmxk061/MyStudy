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
    public partial class Form2 : Form
    {

        #region 속성 / 프로퍼티 / 생성자
        public string IP { get; private set; }
        public int PORT { get; private set; }

        public Form2()
        {
            InitializeComponent();
        }
        #endregion

        #region 기능
        //확인
        private void button1_Click(object sender, EventArgs e)
        {
            IP = textBox1.Text;
            PORT = int.Parse(textBox2.Text);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        //취소
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion

    }
}
