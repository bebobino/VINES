using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using VINES.Models;

namespace VINES.Processes
{
    public class Help
    {


        static string constring = "Data Source=DESKTOP-6731HIA\\SQLEXPRESS;Initial Catalog=Vines;Integrated Security=True";

        public static void test(int sourcesID, string uploadDate, string pageTitle, string webURL, string summary)
        {
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







        public void sendEmail(string to, string subject, string body)
        {
            var from = "vinessystems@outlook.com"; //VINES email
            var password = "v4Cc!n3$"; //VINES email password 

            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp-mail.outlook.com",
                Port = 587,
                Credentials = new NetworkCredential(from, password)
            };

            try
            {
                Debug.WriteLine("Sending email");
                email.Send(from, to, subject, body);
                Debug.WriteLine("Email sent");
            }
            catch (SmtpException e)
            {
                Debug.WriteLine("Email not sent");
                Debug.WriteLine(e);
            }

        }









        public string logIP()
        {
            string hostName = Dns.GetHostName();
            string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            return IP;
        }
        public bool checkIP()
        {

            var IP = logIP();
            Debug.WriteLine(IP);
            SqlConnection con = new SqlConnection(constring);
            SqlDataReader SR = null;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * From IPAddresses WHERE IPAddress = @IPAddress AND isBlocked = 1";
            cmd.Parameters.Add("@IPAddress", SqlDbType.NVarChar).Value = IP;
            SR = cmd.ExecuteReader();
            if (SR.Read())
            {
                Debug.WriteLine("IP Blocked");
                con.Close();
                con.Dispose();
                return false;
            }
            else
            {
                
                con.Close();
                con.Open();
                cmd.CommandText = "Select * From IPAddresses WHERE IPAddress = @IPAddress";
                SR = cmd.ExecuteReader();
                if (SR.Read())
                {
                    Debug.WriteLine("Existing IP in database");
                }
                else
                {
                    con.Close();
                    con.Open();
                    Debug.WriteLine("New IP address");
                    cmd.CommandText = "INSERT INTO IPAddresses (IPAddress, violations, isBlocked, dateAdded) VALUES (@IPAddress, @violations, @isBlocked, @dateAdded)";
                    cmd.Parameters.Add("@violations", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@isBlocked", SqlDbType.Bit).Value = false; 
                    cmd.Parameters.Add("@dateAdded", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    cmd.ExecuteNonQuery();

                }
                con.Close();
                con.Dispose();
            }
            return true;
        }
        public string Hash(string phrase)
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