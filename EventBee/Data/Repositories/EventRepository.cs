using EventBee.Data;
using EventBee.Models;
using EventBee.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Repositories
{
    public class EventRepository: IEventRepository
    {
        private readonly EventBeeContext _context;

        public EventRepository(EventBeeContext context)
        {
            _context = context;
        }
        public List<Event> GetAll()
        {
            var values = _context.Events
                                  .Include(x => x.Place)
                                  .ToList();
            return values;
        }

        public Event GetById(int id)
        {
            return _context.Events
                           .Include(x => x.Place)
                           .SingleOrDefault(x => x.Id == id);
        }

        public Event Add(Event newItem)
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
            var item = _context.Events.Include(x => x.Place).SingleOrDefault(x => x.Id == id);
            if (item != null)
            {
                var eventPlace = _context.Events.Include(x => x.Place)
                                         .Where(x => x.Place.Id == item.Place.Id)
                                         .ToList();
                if (eventPlace.Count == 1)
                {

                    _context.Places.Remove(item.Place);
                    _context.SaveChanges();
                }
                _context.Events.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;   
        }

        public Event Update(int id, Event updateEvent)
        {
            var findEvent = _context.Events.Include(x => x.Place)
                                            .SingleOrDefault(x => x.Id == id);
            if (findEvent != null)
            {
                findEvent.Date = updateEvent.Date;
                findEvent.Name = updateEvent.Name;

                var place = _context.Places.SingleOrDefault(x => x.Id == findEvent.Place.Id);
                if(place != null)
                {
                    place.Number = updateEvent.Place.Number;
                    place.Street = updateEvent.Place.Street;
                    place.City = updateEvent.Place.City;
                    _context.Update(place);
                    _context.SaveChanges();
                }
                findEvent.Place = place;

                _context.Update(findEvent);
                _context.SaveChanges();

                return findEvent;
            }
            return null;

          
        }
    }
}
