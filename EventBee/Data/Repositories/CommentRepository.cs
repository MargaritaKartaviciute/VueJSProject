using EventBee.Data.Repositories.Interfaces;
using EventBee.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Data.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly EventBeeContext _context;

        public CommentRepository(EventBeeContext context)
        {
            _context = context;
        }
        public List<Comment> GetAll()
        {
            var values = _context.Comments
                                  .Include(x => x.User)
                                  .Include(x => x.EventName)
                                  .ToList();
            return values;
        }

        public Comment GetById(int id)
        {
            return _context.Comments
                          .Include(x => x.User)
                          .Include(x => x.EventName)
                          .SingleOrDefault(x => x.Id == id);
        }

        public Comment Add(Comment newItem)
        {
            if (newItem != null)
            {
                _context.Add(newItem);
                _context.SaveChanges();
                return newItem;
            }
            return null;

        }

        public bool DeleteById(int id)
        {
            var findComment = _context.Comments.Include(x => x.User)
                                               .Include(x => x.EventName)
                                               .SingleOrDefault(x => x.Id == id);
            if (findComment != null)
            {
                _context.Comments.Remove(findComment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Comment Update(int id, Comment updateComment)
        {
            var findComment = _context.Comments.Include(x => x.User)
                                                .Include(x => x.EventName)
                                                .SingleOrDefault(x => x.Id == id);
            if (findComment != null)
            {
                findComment.Text = updateComment.Text;

                _context.Update(findComment);
                _context.SaveChanges();

                return findComment;
            }
            return null;


        }
    }
}
