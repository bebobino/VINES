using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;


namespace VINES.Processes
{
    public class Process
    {

        public void getNews()
        {
            /*
            
            - WEB SCRAPER REQUIRED
            - MUST BE CALLED EVERY 5 MINUTES SA SCHEDULER

            Call system to fetch all sources in DBASE
            
            (for each source na makuha sa DBASE){
                Get first 5 articles
                Save sa DBASE
            }

             */

            GetPNA();
            
        }
        public static async void GetPNA()
        {
            var url = "https://www.pna.gov.ph/articles/search?q=vaccine";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var articleList = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("articles")).ToList();

            var articles = articleList[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("article media")).ToList();
            
            foreach(var article in articles)
            {
                var uploadDate = article.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("byline")).FirstOrDefault().InnerText;
                uploadDate = uploadDate.Trim();

                var pageTitle = article.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("media-heading")).FirstOrDefault().InnerText;
                
                var summary = article.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("excerpt")).FirstOrDefault().InnerText;
                summary = summary.Replace("&ndash;", ":");

                Debug.WriteLine(pageTitle + "\n"+ uploadDate + "\n" + summary + "\n");
            }

        }

        public void checkSub()
        {
            /*
            
            - EVERY START OF DAY DAPAT TINATAWAG (00:00)
            
            Call system to check every user who are subscribed
            
            if(date today is ahead of expiry date){
                Membership expires
            }
             
            */
        }

        public void sendEmail()
        {
            var from = ""; //VINES Gmail
            var password = ""; //VINES Gmail password 
            var to = ""; //User email

            using SmtpClient email = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new NetworkCredential(from, password)
            };

            string subject = ""; //email subject
            string body = ""; //email body
            try
            {
                Debug.WriteLine("sending email lol ***********");
                email.Send(from, to, subject, body);
                Debug.WriteLine("email sent lol ***********");
            }
            catch (SmtpException e)
            {
                Debug.WriteLine("email sending failed lmfao bobo ***********");
            }

        }
    }
}
