using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1
{
    public class Post_Com
    {
        public int Com_Num { get; set; }
        public string Com_Text { get; set; }
        public int Post_Num { get; set; }
        public string Acc_Name { get; set; }
        public DateTime Date { get; set; }

        public Post_Com()
        {

        }
        public Post_Com(int cnum, string text, int pnum, string aname, DateTime date)
        {
            Com_Num = cnum;
            Com_Text = text;
            Post_Num = pnum;
            Acc_Name = aname;
            Date = date;
        }
    }
}
