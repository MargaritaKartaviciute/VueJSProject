using EventBee.Data.Repositories.Interfaces;
using EventBee.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Data.Repositories
{
    public class UserEventsRepository : IUserEventsRepository
    {
        private readonly EventBeeContext _context;

        public UserEventsRepository(EventBeeContext context)
        {
            _context = context;
        }

        public UserEvents AddUserEvent(int id, int eventId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            var userEvent = _context.Events.FirstOrDefault(x => x.Id == eventId);

            var haveEvent = _context.UserEvents.Include(x => x.User).Include(y => y.Event).Any(x => x.User.Id == id && x.Event.Id == eventId);
            if (user != null && userEvent != null && haveEvent == false)
            {
                var newUserEvent = new UserEvents
                {
                    Id = 0,
                    User = user,
                    Event = userEvent
                };
            
                _context.Add(newUserEvent);
                _context.SaveChanges();
                return newUserEvent;
            }
            return null;


        }

        public bool DeleteUserEvent(int id, int eventId)
        {
            var userEvent = _context.UserEvents.Include(x => x.User)
                                                .Include(x => x.Event)
                                                .Where(x => x.User.Id == id)
                                                .FirstOrDefault(x => x.Event.Id == eventId);
            if (userEvent != null)
            {
                _context.UserEvents.Remove(userEvent);
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public List<User> GetEventUsers(int id)
        {
            var users = _context.UserEvents.Include(x => x.Event)
                                            .Where(x => x.Event.Id == id)
                                            .Select(x => x.User)
                                            .ToList();
            return users;
        }

        public List<Event> GetUserEvents(int id)
        {
            var events = _context.UserEvents.Include(x => x.User)
                                            .Where(x => x.User.Id == id)
                                            .Select(x => x.Event)
                                            .ToList();
            return events;
        }
    }
}
