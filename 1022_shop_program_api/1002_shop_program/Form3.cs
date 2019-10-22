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
    public partial class Form3 : Form
    {
        public Form3(string searchword, int what)
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            if (what == 0)
                webBrowser1.Navigate("https://search.naver.com/search.naver?where=nexearch&sm=tab_jum&query=" + searchword);    //통합검색
            else if (what == 1)
                webBrowser1.Navigate("https://search.naver.com/search.naver?where=post&sm=tab_jum&query=" + searchword);        //블로그
            else if (what == 2)
                webBrowser1.Navigate("https://search.naver.com/search.naver?where=video&sm=tab_jum&query=" + searchword);       //동영상
        }
    }
}
