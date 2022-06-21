﻿using HtmlAgilityPack;
using Quartz;
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
    public class NewsJob: IJob
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

        public void timer()
        {
            Debug.WriteLine("5 seconds has passed " + DateTime.Now);
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            timer();
            return Task.CompletedTask;
        }
    }
}
