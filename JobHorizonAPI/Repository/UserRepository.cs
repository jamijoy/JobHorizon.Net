using JobHorizonAPI.Interface;
using JobHorizonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHorizonAPI.Repository
{
    
    public class UserRepository : Repository<User>,IUserRepository
    {
        JobHorizonContext context;

        public IEnumerable<User> GetConnectedPeople(int MyId)
        {
            var connection = context.Set<Messege>().FirstOrDefault(x=> x.ReceiverId == MyId);
            return context.Set<User>().Where(x => x.UserId == MyId).ToList();
        }
    }
}