using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1
{
    [DataContract]
    public class Post
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int POST_NUM { get; private set; }

        [DataMember(Order = 2, IsRequired = true)]
        public string POST_PHOTO { get; private set; }

        [DataMember(Order = 3, IsRequired = true)]
        public string POST_TEXT { get; private set; }

        [DataMember(Order = 4, IsRequired = true)]
        public int ACC_NUM { get; private set; }

        [DataMember(Order = 5, IsRequired = true)]
        public string POST_DATE { get; private set; }

        [DataMember(Order = 6, IsRequired = true)]
        public int POST_COUNT { get; private set; }

        [DataMember(Order = 7, IsRequired = true)]
        public int POST_LOVE { get; private set; }

        [DataMember(Order = 8, IsRequired = true)]
        public string POST_LINK { get; private set; }


        public Post(int post_num, string post_photo, string post_text, 
            int acc_num, string post_date, 
            int post_count,int post_love,string post_link)
        {
            POST_NUM = acc_num;

            POST_PHOTO = post_photo;

            POST_TEXT = post_text;

            ACC_NUM = acc_num;

            POST_DATE = post_date;

            POST_COUNT = post_count;

            POST_LOVE = post_love;

            POST_LINK = post_link;

        }
    }
}
  