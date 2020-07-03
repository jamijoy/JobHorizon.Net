using JobHorizonAPI.Interface;
using JobHorizonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHorizonAPI.Repository
{
    public class BidRepository : Repository<BidList>, IBid
    {
        JobHorizonContext context;
        public BidRepository()
        {
            context = new JobHorizonContext();
        }

        public IEnumerable<BidList> GetBidList(int id)
        {
            return context.Set<BidList>().Where(x=> x.JobId == id).ToList();
        }
    }
}