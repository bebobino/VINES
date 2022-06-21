using System;

namespace VINES.Models
{
    public class JobModel
    {
        public Guid JobID { get; set; }
        public Type JobType { get; set; }
        public string JobName { get; set; }
        public string CronExpression { get; set; }
        public JobModel (Guid JobID, Type JobType, string JobName,
            string CronExpression)
        {
            this.JobID = JobID;
            this.JobType = JobType;
            this.JobName = JobName;
            this.CronExpression = CronExpression;
        }
    }
}
