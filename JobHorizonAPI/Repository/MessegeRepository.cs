using JobHorizonAPI.Interface;
using JobHorizonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHorizonAPI.Repository
{
    public class MessegeRepository : Repository<Messege>, IMessegeRepository
    {
        JobHorizonContext context;

        public MessegeRepository()
        {
            context = new JobHorizonContext();
        }

        public IEnumerable<Messege> GEtConnectedPeople(int id)
        {
            return context.Set<Messege>()
                   .Where(e => id == e.ReceiverId || id==e.SenderId)
                   .GroupBy(e => new { e.ReceiverId, e.SenderId})
                   .Select(g => g.FirstOrDefault())
                   .Distinct()
                   .ToList(); ;
        }

        public IEnumerable<Messege> GetMyMessege(int SenderId, int ReceiverId)
        {
            return context.Set<Messege>().Where(x => x.SenderId == SenderId && x.ReceiverId == ReceiverId || x.SenderId == ReceiverId && x.ReceiverId == SenderId).ToList();
        }
    }
}