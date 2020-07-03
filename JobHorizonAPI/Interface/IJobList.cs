using JobHorizonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHorizonAPI.Interface
{
    interface IJobList: IRepository<JobList>
    {
        IEnumerable<JobList> JobPostedByCustomer(int id);
        IEnumerable<JobList> JobForWorker(string type);
    }
}
