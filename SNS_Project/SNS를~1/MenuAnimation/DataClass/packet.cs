using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MenuAnimation
{ 

    class packet
    {

    }

    class insertPost_packet
    {
        public string Msg { get; private set; }
        public Image Photo  { get; private set; }
        public string Text  { get; private set; }
        public int Accnum  { get; private set; }
        public string Date { get; private set; }

        public insertPost_packet(Image photo, string text, int accnum, string date)
        {
            Msg = "InsertPost";
            Photo = photo;
            Text = text;
            Accnum = accnum;
            Date = date;
        }
    }

    class insertComment_packet
    {
        public string Msg { get; private set; }
        public string Text { get; private set; }
        public int Post_num { get; private set; }

        public insertComment_packet(string text, int post_num)
        {
            Msg = "InsertComment";
            Text = text;
            Post_num = post_num;
        }
    }
}
