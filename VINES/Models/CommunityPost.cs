using System;

namespace VINES.Models
{
    public class CommunityPost
    {
        public int communityPostID { get; set; }

        public string communityPostTitle { get; set; }

        public int communityPostCategory { get; set; }

        public string communityPostContent { get; set; }

        public DateTime dateAdded { get; set; }

        public DateTime lastModified { get; set; }



    }
}
