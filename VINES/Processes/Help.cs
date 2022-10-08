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
using System.Text;
using System.IO;

namespace VINES.Processes
{
    public class Help
    {


        static string constring = AppSettings.ConnectionStrings.DefaultConnection;

        public void saveNews(int sourcesID, string uploadDate, string pageTitle, string webURL, string summary)
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

        public void dailyCheck()
        {
            SqlConnection con = new SqlConnection(constring);
            SqlDataReader SR = null;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "update patients set isSubscribed = 0, showAds = 1 where subEnd < GETDATE()";
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();

        }






        public void sendEmail(string to, string subject, string body)
        {
            var from = AppSettings.Smtp.FromAddress;
            var password = AppSettings.Smtp.Password;   

            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = AppSettings.Smtp.Server,
                Port = AppSettings.Smtp.Port,
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

        readonly static string key = AppSettings.Cryp.Key;

        public string Encrypt(string text)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cs))
                        {
                            streamWriter.Write(text);
                        }
                        array = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public string Decrypt(string text)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(text);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cs))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
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