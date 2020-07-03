﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobHorizonAPI.Models;

namespace JobHorizonAPI.Interface
{
    interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> DeleteCommentByForumId(int forum_id);
    }
}
