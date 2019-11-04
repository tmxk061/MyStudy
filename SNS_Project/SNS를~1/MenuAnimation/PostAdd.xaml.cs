using Microsoft.Win32;
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
using System.Windows.Shapes;
using MenuAnimado1;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MenuAnimation.ServiceReference1;

namespace MenuAnimation
{
    /// <summary>
    /// PostAdd.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PostAdd : Window
    {
        public Image Post_Imgae = new Image();
        MainWindow MW = null;

        public PostAdd(MainWindow mainwindow)
        {
            InitializeComponent();
            MW = mainwindow;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //업로드 버튼
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                Post_Imgae.Source = new BitmapImage(new Uri(ofd.FileName, UriKind.Relative));

                ImageBrush IB = new ImageBrush(Post_Imgae.Source);
                uploadbtn.Background = IB;
            }
        }

        //등록 버튼
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*
              회원번호
              Post_Imgae
              posttext    
              등록날짜
             */
            string date = DateTime.Now.ToString();
            insertPost_packet ip = new insertPost_packet(Post_Imgae, posttext.Text, MW.Nowaccount.Acc_num,date);

            DataBaseCtrClient dbctr = new DataBaseCtrClient();
            dbctr.AddPost("대충여기어딘가", posttext.Text, MW.Nowaccount.Acc_num);

            //서버에 전송 -> 성공시
            post_poto post = new post_poto(1, Post_Imgae, "와하하", 1, 0, 0, date);
            MW.Gallery.Items.Add(new photoBtn(MW,post));
            this.Close();
        }

        public static byte[] ObjectByteArrayConverter(object p_obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {

                formatter.Serialize(ms, p_obj);

                return ms.ToArray();

            }
            catch
            {

                return null;

            }
            finally
            {

                ms.Close();

            }

        }

    }
}
