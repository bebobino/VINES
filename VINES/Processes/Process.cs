using System;
using System.Collections.Generic;
using System.Linq;
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
