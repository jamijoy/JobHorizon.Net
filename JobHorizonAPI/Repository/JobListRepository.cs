using JobHorizonAPI.Interface;
using JobHorizonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHorizonAPI.Repository
{
    public class JobListRepository : Repository<JobList>, IJobList
    {
        JobHorizonContext context;

        public JobListRepository()
        {
            context = new JobHorizonContext();
        }

        public IEnumerable<JobList> JobForWorker(string type)
        {
            return context.Set<JobList>().Where(x => x.JobType == type && x.Status=="Active").ToList();
        }

        public IEnumerable<JobList> JobPostedByCustomer(int id)
        {
            return context.Set<JobList>().Where(x => x.PostedBy == id && x.Status == "Active").ToList();
        }
    }
}