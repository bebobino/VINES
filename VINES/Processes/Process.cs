using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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

            GetHtmlAsync();
            
        }
        public static async void GetHtmlAsync()
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
                Debug.WriteLine(article.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("media-heading")).FirstOrDefault().InnerText
                    );
                Debug.WriteLine(article.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("byline")).FirstOrDefault().InnerText
                    );
                Debug.WriteLine(article.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("excerpt")).FirstOrDefault().InnerText
                    );
                Debug.WriteLine("\n");
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
    }
}
