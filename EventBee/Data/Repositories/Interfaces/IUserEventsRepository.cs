using EventBee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Data.Repositories.Interfaces
{
    public interface IUserEventsRepository
    {
        List<Event> GetUserEvents(int id);
        bool DeleteUserEvent(int id, int eventId);

        UserEvents AddUserEvent(int id, int eventId);
        List<User> GetEventUsers(int id);
    }
}
