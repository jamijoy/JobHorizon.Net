using JobHorizonAPI.Interface;
using JobHorizonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHorizonAPI.Repository
{
    public class JobHistoryRepository : Repository<JobHistory>, IJobHistory
    {
        JobHorizonContext context;
        public JobHistoryRepository()
        {
            context = new JobHorizonContext();
        }

        public IEnumerable<JobHistory> GetApprovedJob(int id)
        {
            return context.Set<JobHistory>().Where(x => x.JobList.PostedBy == id && x.Status=="Pending");
        }

        public IEnumerable<JobHistory> GetApprovedJobByType(string type)
        {
            return context.Set<JobHistory>().Where(x => x.JobList.JobType == type && x.Status == "Pending");
        }
    }
}