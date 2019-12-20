using EventBee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Data.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        List<Comment> GetAll();
        Comment GetById(int id);
        Comment Add(Comment newItem);
        bool DeleteById(int id);
        Comment Update(int id, Comment updateEvent);
    }
}
