using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1002_shop_program
{
    public partial class shopingItem : UserControl
    {
        public TextBox num;
        public Form1 form;

        public shopingItem(Image img, string name, string price, string url, Form1 f)
        {
            InitializeComponent();
            pictureBox1.Image = img;
            textBox1.Text = name;
            textBox2.Text = price;
            textBox4.Text = url;

            num = this.textBox3;
            form = f;
        }

        //장바구니
        private void button2_Click(object sender, EventArgs e)
        {
            form.AddList(int.Parse(textBox3.Text));
        }

        //상세검색
        private void button1_Click(object sender, EventArgs e)
        {
            form.CallSearchItemModal(textBox1.Text);
        }
    }
}
