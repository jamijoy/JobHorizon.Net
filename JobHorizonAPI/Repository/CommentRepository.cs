using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobHorizonAPI.Interface;
using JobHorizonAPI.Models;

namespace JobHorizonAPI.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        JobHorizonContext context;
        public CommentRepository()
        {
            context = new JobHorizonContext();
        }
        public IEnumerable<Comment> DeleteCommentByForumId(int forum_id)
        {
            return null;
        }
    }
}