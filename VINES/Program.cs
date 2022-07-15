using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINES.JobFactory;
using VINES.Models;
using VINES.Scheduler;
using VINES.Processes;

namespace VINES
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IJobFactory, MyJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton<NewsJob>();
                    services.AddSingleton(new JobModel(Guid.NewGuid(), typeof(NewsJob), "Get News", "0/5 * * * * ?"));
                    services.AddHostedService<MyScheduler>();
                });
    }
}
