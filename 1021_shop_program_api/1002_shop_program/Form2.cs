using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1002_shop_program
{
    public partial class Form2 : Form
    {
        string searchword = string.Empty;

        public Form2(string title)
        {
            InitializeComponent();

            textBox1.Text = title;
            comboBox1.SelectedIndex = 0;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        //검색
        private void button1_Click(object sender, EventArgs e)
        {
            searchword = string.Empty;
            searchword += textBox1.Text;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                searchword += listBox1.Items[i];
            }
            int what = comboBox1.SelectedIndex;
            Form3 form3 = new Form3(searchword, what);
            form3.Show();
        }

        //포함
        private void button2_Click(object sender, EventArgs e)
        {
            //★☆★ 네이버는 +는 공백 %2B가 "+"
            listBox1.Items.Add(" %2B" + textBox2.Text);
            textBox2.Clear();
        }

        //제외
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(" -" + textBox3.Text);
            textBox3.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
