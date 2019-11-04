using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.ServiceModel;

namespace Dashboard1
{
    [ServiceContract]
    public interface IDataBaseCtr
    {
        [OperationContract]
        List<Post> FindPost(int ACC_NUM);

        [OperationContract]
        void AddPost(string POST_LINK, string POST_TEXT, int ACC_NUM);

        [OperationContract]
        void ConnectDB();

        [OperationContract]
        void DisConnectDB();
       

    }


    class DataBaseCtr : IDataBaseCtr
    {

        Allinfo allinfo = new Allinfo();
        CommentList clist = new CommentList();
        //PostList plist = new PostList();
        AccList acclist = new AccList();

        private SqlConnection conn;

        private string constr1;
        private string constr2;

        public DataBaseCtr()
        {
 
            conn = new SqlConnection();
            constr1 = @"Server=192.168.0.32;database = instatest;uid=pch;pwd=1234;";
            constr2 = @"Data Source=[192.168.0.32];Initial Catalog=[instatest]; User ID=[pch]";
            conn.ConnectionString = constr1;

        }

       
        public void ConnectDB()
        {
            try
            {
                conn.Open();    //  데이터베이스 연결
               
               
            }
            catch (Exception)
            {
                MessageBox.Show("연결 실패");
            }
        }

        public void DisConnectDB()
        {
            try
            {
                conn.Close();    //  데이터베이스 연결

            }
            catch (Exception)
            {
                MessageBox.Show("연결해제 실패");
            }
        }

        private void AddPost_Click(object sender, RoutedEventArgs e)
        {

            //AddPost("image1/maxican.jpg", "나오늘 집안가", 1);
            //DeletePost(8);
            //FindComment(3);
            //AllUser();
            //PrintPost(2);
            //MakeComment("안녕", 1, 2);
            PrintOtherPost(2);
        }
        //유저수 읽기 프로시저
        private void AllUser()
        {
            try
            {
                string comtext = "AllUser";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter param_alluser = new SqlParameter();
                param_alluser.Direction = System.Data.ParameterDirection.Output;
                param_alluser.ParameterName = "@alluser";
                param_alluser.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_alluser);
                command.ExecuteNonQuery();
                //allinfo.getuser((int)param_alluser.Value);
                allinfo.Result = param_alluser.Value.ToString();
                MessageBox.Show("저장 성공");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //게시물수 읽기 프로시저
        private void AllCount_Post()
        {
            try
            {
                string comtext = "AllCount_Post";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter param_alluser = new SqlParameter();
                param_alluser.Direction = System.Data.ParameterDirection.Output;
                param_alluser.ParameterName = "@ALLCOUNT_POST";
                param_alluser.SqlDbType = System.Data.SqlDbType.Int;
                command.Parameters.Add(param_alluser);
                command.ExecuteNonQuery();
                //allinfo.getuser((int)param_alluser.Value);
                allinfo.Result = param_alluser.Value.ToString();
                MessageBox.Show("저장 성공");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        //게시물 올리기 프로시저(이미지저장과정필요)에코로 성공실패
        public void AddPost(string POST_LINK, string POST_TEXT, int ACC_NUM)
        {
            try
            {
                ConnectDB();
                string comtext = "AddPost";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                SqlParameter param_POST_link = new SqlParameter("@POST_LINK", POST_LINK);
                command.Parameters.Add(param_POST_link);

                SqlParameter param_POST_text = new SqlParameter("@POST_TEXT", POST_TEXT);
                command.Parameters.Add(param_POST_text);

                SqlParameter param_ACC_NUM = new SqlParameter();
                param_ACC_NUM.ParameterName = "@ACC_NUM";
                param_ACC_NUM.SqlDbType = System.Data.SqlDbType.Int;
                param_ACC_NUM.Value = ACC_NUM;
                command.Parameters.Add(param_ACC_NUM);

                command.ExecuteNonQuery();

                MessageBox.Show("저장 성공");
                DisConnectDB();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //게시물 삭제 프로시저
        private void DeletePost(int POST_NUM)
        {
            try
            {
                string comtext = "DeletePost";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };



                SqlParameter param_POST_NUM = new SqlParameter();
                param_POST_NUM.ParameterName = "@POST_NUM";
                param_POST_NUM.SqlDbType = System.Data.SqlDbType.Int;
                param_POST_NUM.Value = POST_NUM;
                command.Parameters.Add(param_POST_NUM);

                command.ExecuteNonQuery();

                MessageBox.Show("저장 성공");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //댓글 불러오기
        public void FindComment(int POST_NUM)
        {
            try
            {

                string comtext = "FindComment";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                clist.Clear();
                while (reader.Read())
                {
                    int cmtnum = int.Parse(reader["COM_NUM"].ToString());
                    string cmttext = reader["COM_TEXT"].ToString();
                    int postnum = int.Parse(reader["POST_NUM"].ToString());
                    string accname = reader["ACC_NAME"].ToString();             // 서브쿼리이용. COMDATA테이블의 ACC_ID로 가져온 ACC_NAME
                    DateTime date = DateTime.Parse(reader["COM_DATE"].ToString());

                    clist.Add(new Post_Com(cmtnum, cmttext, postnum, accname, date));
                }
            }
            catch
            {

            }
        }
        //게시물 불러오기
        public List<Post> FindPost(int ACC_NUM)
        {
            try
            {
                ConnectDB();
                string comtext = "PrintPost";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                SqlParameter param_num = new SqlParameter("@param1", ACC_NUM);
                command.Parameters.Add(param_num);

                SqlDataReader reader = command.ExecuteReader();
                List<Post> plist = new List<Post>();
                while (reader.Read())
                {
                    int post_num = int.Parse(reader["POST_NUM"].ToString());
                    string post_photo = reader["POST_PHOTO"].ToString();
                    string post_text = reader["POST_TEXT"].ToString();
                    int acc_num = int.Parse(reader["ACC_NUM"].ToString());
                    string post_date = reader["POST_DATE"].ToString();
                    int post_count = int.Parse(reader["POST_COUNT"].ToString());
                    int post_love = int.Parse(reader["POST_LOVE"].ToString());
                    string post_link = reader["POST_LINK"].ToString();
                    //string post_media = reader["POST_MEDIA"].ToString();             // 미디어

                    plist.Add(new Post(post_num, post_photo, post_text, acc_num,
                        post_date, post_count, post_love, post_link));
                }
                //전송
                DisConnectDB();
                return plist;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //검색한 이름으로 게시물 불러오기
        public void PrintOtherPost(int ACC_NUM)
        {
            try
            {

                string comtext = "PrintOtherPost";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                SqlDataReader reader = command.ExecuteReader();
                //plist.Clear();
                while (reader.Read())
                {
                    int acc_num = int.Parse(reader["ACC_NUM"].ToString());
                    string acc_name = reader["ACC_NAME"].ToString();
                    string acc_image = reader["ACC_IMAGE"].ToString();
                    string acc_id = reader["ACC_ID"].ToString();
                    string acc_pw = reader["ACC_PW"].ToString();
                    string acc_profile = reader["ACC_PROFILE"].ToString();
                    string acc_date = reader["ACC_DATE"].ToString();
                    //string post_media = reader["POST_MEDIA"].ToString();             // 미디어
                    Image photo = new Image();// <- url을통해 이미지 저장
                    acclist.Add(new Account(acc_num, acc_name, photo, acc_id, //
                        acc_pw, acc_profile, acc_date));
                }
                //전송
            }
            catch
            {

            }
        }
        //게시물 출력 프로시저
        private void PrintPost(int ACC_NUM)
        {
            try
            {
                string comtext = "PrintPost";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };



                SqlParameter param_ACC_NUM = new SqlParameter();
                param_ACC_NUM.ParameterName = "@param1";
                param_ACC_NUM.SqlDbType = System.Data.SqlDbType.Int;
                param_ACC_NUM.Value = ACC_NUM;
                command.Parameters.Add(param_ACC_NUM);

                command.ExecuteNonQuery();

                MessageBox.Show("저장 성공");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //댓글 생성 프로시저
        private void MakeComment(string com_text, int post_num, int acc_num)
        {
            try
            {
                string comtext = "MAKECOM";
                SqlCommand command = new SqlCommand(comtext, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                SqlParameter param_com_text = new SqlParameter("@COM_TEXT", com_text);
                command.Parameters.Add(param_com_text);

                SqlParameter param_post_num = new SqlParameter();
                param_post_num.ParameterName = "@POST_NUM";
                param_post_num.SqlDbType = System.Data.SqlDbType.Int;
                param_post_num.Value = post_num;
                command.Parameters.Add(param_post_num);

                SqlParameter param_acc_num = new SqlParameter();
                param_acc_num.ParameterName = "@ACC_NUM";
                param_acc_num.SqlDbType = System.Data.SqlDbType.Int;
                param_acc_num.Value = acc_num;
                command.Parameters.Add(param_acc_num);

                command.ExecuteNonQuery();

                MessageBox.Show("저장 성공");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
