using EventBee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Repositories.Interfaces
{
    public interface IEventRepository
    {
        List<Event> GetAll();
        Event GetById(int id);
        Event Add(Event newItem);
        bool DeleteById(int id);
        Event Update(int id, Event updateEvent);
    }
}
