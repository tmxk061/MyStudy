using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1016_Cs_TCP_IP_client
{
    class UI
    {
        public static void FillDrawing(Control c, Color color)
        {
            c.BackColor = color;
        }

        public static void LabelState(Control c, string text)
        {
            c.Text = text;
        }

        public static void LogPrint(ListBox c, string msg)
        {

            //MSG = [연결]....성공
            msg +="(" + DateTime.Now.ToString() + ")";
            c.Items.Add(msg);
        }

        public static void ControllEnabled(Control c, bool stat)
        {
            c.Enabled = stat;
        }
    }
}
