using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using VINES.Models;

namespace VINES.Processes
{
    public class Help
    {
        private static DatabaseContext db;
        public Help(DatabaseContext _db)
        {
            db = _db;
        }

        static string constring = "Data Source=MSI\\SQLEXPRESS; Initial Catalog=Vines;Integrated Security=True; TrustServerCertificate=True";

        public static void test(int sourcesID, string uploadDate, string pageTitle, string webURL, string summary)
        {
            Boolean testing = false;
            SqlConnection con = new SqlConnection(constring);
            SqlDataReader SR = null;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * From webPages WHERE webURL = @webURL";
            cmd.Parameters.Add("@webURL", SqlDbType.NVarChar).Value = webURL;
            SR = cmd.ExecuteReader();
            if (SR.Read())
            {
                con.Close();
            }
            else{
                con.Close();
                con.Open();
                int x = 0;
                cmd.CommandText = "INSERT INTO posts VALUES (@isVisible)";
                cmd.Parameters.Add("@isVisible", SqlDbType.Bit).Value = true;
                cmd.ExecuteNonQuery();
                /*
                cmd.CommandText = "declare @num int set @num = @@IDENTITY select @num";
                SR = cmd.ExecuteReader();
                if (SR.Read())
                {
                    x = Int32.Parse(SR.GetValue(0).ToString());
                }
                */
                cmd.CommandText = "declare @num int set @num = @@IDENTITY INSERT INTO webPages (sourcesID, webURL, uploadDate, pageTitle, summary, " +
                    "postID, dateAdded)VALUES (@sourcesID, @webURL, @uploadDate, @pageTitle, @summary, @num, @dateAdded)";

                cmd.Parameters.Add("@sourcesID", SqlDbType.Int).Value = sourcesID;
                cmd.Parameters.Add("@uploadDate", SqlDbType.DateTime).Value = Convert.ToDateTime(uploadDate);
                cmd.Parameters.Add("@pageTitle", SqlDbType.NVarChar).Value = pageTitle;
                cmd.Parameters.Add("@summary", SqlDbType.NVarChar).Value = summary;
                cmd.Parameters.Add("@dateAdded", SqlDbType.DateTime).Value = DateTime.UtcNow;
                cmd.ExecuteNonQuery();
                

            }
            con.Close();
            con.Dispose();



        }
        public void logIP()
        {
            string hostName = Dns.GetHostName();
            string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            Debug.WriteLine("IP Address is : " + IPAddress.Parse(IP));
        }

        public static void AddNewsRecord(int sourcesID, string uploadDate, string pageTitle, string webURL, string summary)
        {
            var postModel = new Posts
            {
                isVisible = true
            };
            db.Post.Add(postModel);
            db.SaveChanges();

            var webPage = new WebPages
            {
                sourcesID = sourcesID,
                dateAdded = DateTime.Now,
                uploadDate = Convert.ToDateTime(uploadDate),
                pageTitle = pageTitle,
                webURL = webURL,
                summary = summary,
                postID = postModel.postID
            };
            db.WebPages.Add(webPage);
            db.SaveChanges();
        }
        public static string Hash(string phrase)
        {
            SHA512Managed HashTool = new SHA512Managed();
            Byte[] PhraseAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(phrase));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PhraseAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }
    }
}


/*
 
private readonly DatabaseContext Db;

        public SuccessModel(DatabaseContext Db)
        {
            this.Db = Db;
        }

public List<Patients> Patients { get; set; }

public async OnGet(){



}
 */