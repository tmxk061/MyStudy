using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1016Server
{
    class Member
    {
        public string Id { get; set; }
        public string Pw { get; set; }
        public string Name { get; private set; }
        public int Age { get; set; }

        public Member(string id, string pw, string name, int age)
        {
            Id = id;
            Pw = pw;
            Name = name;
            Age = age;
        }

    }

    class MemberControl
    {
        private List<Member> memlist = new List<Member>();

        public string LastSearchName { get; private set; }

        #region 싱글톤 패턴
        public static MemberControl Instance { get; private set; }

        static MemberControl()
        {
            Instance = new MemberControl();
        }

        private MemberControl()
        {

        }
        #endregion 

        public bool Insert(Member mem)
        {
            try
            {
                for (int i = 0; i < memlist.Count; i++)
                {
                    if (memlist[i].Id.Equals(mem.Id))
                    {
                        return false;
                    }
                }

                memlist.Add(mem);
                return true;
            }
            catch(Exception )
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            foreach (Member mem in memlist)
            {
                if(mem.Id.Equals(id))
                {
                    memlist.Remove(mem);
                    return true;
                }
            }
            return false;
        }

        public bool LogIn(string id, string pw)
        {
            for (int i = 0; i < memlist.Count; i++)
            {
                if (memlist[i].Id.Equals(id))
                {
                    if (memlist[i].Pw.Equals(pw))
                    {
                        LastSearchName = memlist[i].Name;
                        return true;
                    }
                       
                }
            }

            return false;
        }

        public bool LogOut(string id)
        {
            for (int i = 0; i < memlist.Count; i++)
            {
                if (memlist[i].Id.Equals(id))
                {
                   return true;
                }
            }
            return false;
        }

        public string GetName(string id)
        {
            string name = string.Empty;
            for (int i = 0; i < memlist.Count; i++)
            {
                if (memlist[i].Id.Equals(id))
                {
                    name = memlist[i].Name;
                    return name;
                }
            }

            return null;
        }
    }
}
