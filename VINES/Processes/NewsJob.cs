using HtmlAgilityPack;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace VINES.Processes
{
    public class NewsJob : IJob
    {

        public void getNews()
        {


            getPNA();
            //getDOH();


        }
        public static async void getPNA()
        {
            try
            {
                HtmlWeb web = new HtmlWeb();

                web.OverrideEncoding = Encoding.UTF8;
                web.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0";

                var url = "https://www.pna.gov.ph/articles/search?q=vaccine";
                var httpClient = new HttpClient();

                var htmlDocument = new HtmlDocument();
                htmlDocument = web.Load(url);

                var articleList = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("articles")).ToList();

                var articles = articleList[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("article media")).ToList();

                foreach (var article in articles)
                {
                    var uploadDate = article.Descendants("p")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("byline")).FirstOrDefault().InnerText;
                    uploadDate = uploadDate.Trim();

                    var pageTitle = article.Descendants("h3")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("media-heading")).FirstOrDefault().InnerText;
                    var webURL = article.Descendants("h3")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("media-heading")).FirstOrDefault().InnerHtml;
                    var reg = new Regex("\".*?\"");
                    var matches = reg.Matches(webURL);
                    foreach (var item in matches)
                        webURL = "https://www.pna.gov.ph" + item.ToString().Trim('"');
                    var summary = article.Descendants("p")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("excerpt")).FirstOrDefault().InnerText;
                    summary = summary.Replace("&ndash;", ":");
                    //createRecord(1, uploadDate, pageTitle, webURL, summary);

                    Help.test(1, uploadDate, pageTitle, webURL, summary);
                    //Debug.WriteLine(pageTitle + "\n" + uploadDate + "\n" + summary + "\n" + webURL);
                }
            }catch(Exception e)
            {

            }

        }

        public static async void getDOH()
        {
            var url = "https://doh.gov.ph/search/node/vaccine";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var articleList = htmlDocument.DocumentNode.Descendants("ol")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("search-results node-results")).ToList();

            var articles = articleList[0].Descendants("li")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("")).ToList();

            foreach (var article in articles)
            {


                var pageTitle = article.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("title")).FirstOrDefault().InnerText;

                var summary = article.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("search-snippet")).FirstOrDefault().InnerText;
                summary = summary.Replace("&ndash;", ":");

               // Debug.WriteLine(pageTitle + "\n" + "\n" + summary + "\n");
            }

        }

        public void timer()
        {
            Debug.WriteLine("5 seconds has passed " + DateTime.Now);
        }

        public Task Execute(IJobExecutionContext context)
        {
            timer();
            getNews();
            return Task.CompletedTask;
        }
    }
}