using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MenuAnimation
{
    public class post_poto
    {
        public int Num { get; private set; }
        public Image Photo = new Image();
        public string Text { get; private set; }
        public int Accnum { get; private set; }
        public int Count { get; private set; }
        public int Love { get; private set; }
        public string Date { get; private set; }

        public post_poto(int num, Image photo, string text, int accnum, int count, int love, string date)
        {
            Num = num;
            Photo = photo;
            Text = text;
            Accnum = accnum;
            Count = count;
            Love = love;
            Date = date;
        }
    }


}
