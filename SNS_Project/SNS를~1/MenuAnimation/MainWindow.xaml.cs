using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MenuAnimation;
using MenuAnimation.ServiceReference1;
using Microsoft.Win32;

namespace MenuAnimado1
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public List<post_poto> postlist = new List<post_poto>(); //현재 표시중인 계정 리스트
        public account LoginAccount = null; //로그인 되어 있는 계정
        public account Nowaccount = null; //현재 조회중인 계정

        public MainWindow()
        {
            InitializeComponent();

            //임시로그인
            LoginAccount = new account(1, "서석권", null, "tmxki061", "1234", "안녕하세요", "12/12/12");
            Nowaccount = LoginAccount;
            GetPostList(); //조회중 계정 게시물 불러오기
            UpdateProfil();
        }


        #region 레이아웃
        //버튼_종료
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //버튼_메뉴클릭(open)
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        //버튼_메뉴클릭(close)
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        //메뉴 드래그 이동
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion


        #region 계정관리
        //계정 검색
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string str = SearchText.Text;
            //str 서버로 전송, 계정 있는지 없는지 확인

            //계정이 있을 경우
            //계정 객체 생성
            account friend = new account(2, "한지수", null, "qwe", "1234", "교대만세", "12/12/12");
            Nowaccount = friend;
            Post_Update();

            BeginStoryboard((Storyboard)FindResource("CloseMenu"));
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        //버튼_홈
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Nowaccount = LoginAccount;
            Post_Update();

            BeginStoryboard((Storyboard)FindResource("CloseMenu"));
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        #endregion


        #region 데이터 관리
        //게시물추가 버튼
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PostAdd PA = new PostAdd(this);
            PA.ShowDialog();
        }

        #endregion

        #region 갱신
        //게시물 갱신
        private void Post_Update()
        {
            GetPostList();
            UpdateProfil();
        }

        public void Update_Post()
        {
            while (true)
            {
                Gallery.Items.Remove(Gallery.Items.Count);
                if (Gallery.Items.Count == 1)
                    break;
            }
            
            for (int i = 0; i < postlist.Count; i++)
            {
                Gallery.Items.Add(new photoBtn(this, postlist[i]));
            }
        }

        //프로필 갱신
        public void UpdateProfil()
        {
            myname.Text = Nowaccount.Acc_name;
            myprofile.Text = Nowaccount.Acc_profile;
        }

        //현재 조회중 계정의 게시물들 불러오는 메서드
        private void GetPostList()
        {
            postlist.Clear();
            DataBaseCtrClient dbctr = new DataBaseCtrClient();
            Post[] list = dbctr.FindPost(Nowaccount.Acc_num);

            for (int i = 0; i < list.Length; i++)
            {
                post_poto p =
                    new post_poto(list[i].POST_NUM, null, list[i].POST_TEXT,
                    list[i].ACC_NUM, list[i].POST_COUNT, list[i].POST_LOVE, list[i].POST_DATE);

                postlist.Add(p);

            }

            //데이터 불러오고
            Update_Post();//게시물 갱신
                          //try
                          //{

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        #endregion

        #region 필터링
        //버튼_이미지 필터링
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainName.Text = "PHOTO";
        }

        //버튼_동영상 필터링
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainName.Text = "MEDIA";

        }

        //버튼_전체 콘텐츠
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainName.Text = "GALLERY";

        }
        #endregion


    }
}
