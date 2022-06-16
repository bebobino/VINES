﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.Models;
using System.Data.SqlClient;
using System.Data.Sql;
using VINES.Pages.CommunityPosts;

namespace VINES.Pages
{
    public class IndexModel : PageModel
    {

        public IEnumerable<CommunityPost> getRecords { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            getRecords = DisplayRecords();
        }

        public static List<CommunityPost> DisplayRecords()
        {
            List<CommunityPost> ListObj = new List<CommunityPost>();
            string connection = "Data Source=DESKTOP-6731HIA\\SQLEXPRESS;Initial Catalog=Vines;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand com = new SqlCommand("SELECT * from dbo.CommunityPost", con))
                {
                    con.Open();
                    using (SqlDataReader sdr = com.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CommunityPost cp = new CommunityPost();
                            cp.communityPostID = Convert.ToInt32(sdr["communityPostID"]);
                            cp.communityPostTitle = Convert.ToString(sdr["communityPostTitle"]);
                            cp.communityPostCategory = Convert.ToInt32(sdr["communityPostCategory"]);
                            cp.communityPostContent = Convert.ToString(sdr["communityPostContent"]);
                            cp.dateAdded = Convert.ToDateTime(sdr["dateAdded"]);
                            cp.lastModified = Convert.ToDateTime(sdr["lastModified"]);
                            ListObj.Add(cp);

                        }
                    }
                    return ListObj;
                }
            }
        }

    }
}
