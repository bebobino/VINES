using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System.Threading;
using System.Threading.Tasks;
using VINES.Models;

namespace VINES.Scheduler
{
    class MyScheduler : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory jobFactory;
        private readonly JobModel jobMetadata;
        private readonly ISchedulerFactory schedulerFactory;

        public MyScheduler(ISchedulerFactory schedulerFactory, JobModel jobMetadata, IJobFactory jobFactory)
        {
            this.jobFactory = jobFactory;
            this.jobMetadata = jobMetadata;
            this.schedulerFactory = schedulerFactory;
        }
    
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            IJobDetail jobDetail = CreateJob(jobMetadata);
            ITrigger trigger = CreateTrigger(jobMetadata);
            await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }

        private IJobDetail CreateJob(JobModel jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType)
                .WithIdentity(jobMetadata.JobID.ToString())
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        private ITrigger CreateTrigger(JobModel jobMetadata)
        {
            return TriggerBuilder.Create()
                .WithIdentity(jobMetadata.JobID.ToString())
                .WithCronSchedule(jobMetadata.CronExpression)
                .WithDescription(jobMetadata.JobName)
                .Build();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
