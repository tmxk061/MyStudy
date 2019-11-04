using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dashboard1
{
    public class Account
    {
        public int Acc_num { get; private set; }
        public string Acc_name { get; private set; }
        public Image Acc_image = new Image();
        public string Acc_id { get; private set; }
        public string Acc_pw { get; private set; }
        public string Acc_profile { get; private set; }
        public string Acc_date { get; private set; }

        public Account(int num, string name, Image image, string id, string pw, string profile, string date)
        {
            Acc_num = num;
            Acc_name = name;
            Acc_image = image;
            Acc_id = id;
            Acc_pw = pw;
            Acc_profile = profile;
            Acc_date = date;
        }

    }
}
