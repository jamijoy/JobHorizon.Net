using JobHorizonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHorizonAPI.Interface
{
    interface IBid : IRepository<BidList>
    {
        IEnumerable<BidList> GetBidList(int id);
    }
}
