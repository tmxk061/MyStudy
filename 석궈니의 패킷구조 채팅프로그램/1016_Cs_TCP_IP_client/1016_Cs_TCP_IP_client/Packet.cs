using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1016_Cs_TCP_IP_client
{
    class Packet
    {

        #region singlton
        public static Packet Instance { get; private set; }

        static Packet()
        {
            Instance = new Packet();
        }

        private Packet()
        {

        }
        #endregion

        #region 클라이언트 => 서버 전송
        public string GetNewMember(string id, string pw, string name, int age)
        {
            string msg = null;
            msg += "NEWMEMBER@";         // 회원 가입 요청 메시지
            msg += id + "#";              // 아이디
            msg += pw + "#";              // 암호
            msg += name + "#";            // 이름
            msg += age;                  // 나이     
            return msg;
        }

        public string GetDelMember(string id)
        {
            string msg = null;
            msg += "DELMEMBER@";         // 회원 가입 요청 메시지
            msg += id;
            return msg;
        }

        public string GetLOGIN(string id, string pw)
        {
            string msg = null;
            msg += "LOGIN@";         // 회원 가입 요청 메시지
            msg += id + "#";              // 아이디
            msg += pw;                  // 나이     
            return msg;
        }

        public string GetLOGOUT(string id)
        {
            string msg = null;
            msg += "LOGOUT@";         // 회원 가입 요청 메시지
            msg += id;                  // 나이     
            return msg;
        }

        public string GetMESSAGE(string name, string data)
        {
            string msg = null;
            msg += "MESSAGE@";
            msg += name + "#";
            msg += data;
            return msg;
        }

        #endregion


        #region 서버 => 클라이언트 응답
        public string GetNewMemberAck(string id, string name, bool b)
        {
            string msg = null;

            msg += "NEWMEMBERACK@";
            msg += b + "#";
            msg += id + "#";
            msg += name;
            return msg;
        }
        public string GetDelMemberAck(string id, bool b)
        {
            string msg = null;

            msg += "DELMEMBERACK@";
            msg += b + "#";
            msg += id;

            return msg;
        }

        public string GetLOGINAck(string id, bool b, string name)
        {
            string msg = null;
            msg += "LOGINACK@";
            msg += b + "#";
            msg += id + "#";
            msg += name;
            return msg;
        }

        public string GetLOGOUTAck(string id, bool b)
        {
            string msg = null;
            msg += "LOGOUTACK@";
            msg += b + "#";
            msg += id;
            return msg;
        }

        public string GetMESSAGEAck(string name, string data)
        {
            string msg = null;
            msg += "MESSAGEACK@";
            msg += name + "#";
            msg += data;
            return msg;
        }
        #endregion 
    }
}
