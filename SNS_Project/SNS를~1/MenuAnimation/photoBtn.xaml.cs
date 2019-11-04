using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MenuAnimado1;
using MenuAnimation;

namespace MenuAnimation
{
    /// <summary>
    /// photoBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class photoBtn : UserControl
    {
        post_poto mypost = null;
        MainWindow MW = null;

        public photoBtn(MainWindow mw, post_poto pt)
        {
            InitializeComponent();
            MW = mw;
            mypost = pt;

            if (mypost.Photo != null)
            {
                ImageBrush IB = new ImageBrush(mypost.Photo.Source);
                mypicture.Background = IB;
            }
            
        }

        //버튼_클릭
        private void Mypicture_Click(object sender, RoutedEventArgs e)
        {
            PostUI PU = new PostUI();
            PU.ShowDialog();
        }

        //버튼_삭제
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MW.postlist.Remove(mypost);
            MW.Gallery.Items.Remove(this);
        }
    }
}
