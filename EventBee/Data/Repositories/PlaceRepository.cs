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
    public class PlaceRepository : IPlaceRepository
    {
        private readonly EventBeeContext _context;

        public PlaceRepository(EventBeeContext context)
        {
            _context = context;
        }
        public List<Place> GetAll()
        {
            var values = _context.Places.ToList();
            return values;
        }

        public Place GetById(int id)
        {
            return _context.Places.SingleOrDefault(x => x.Id == id);
        }

        public Place Add(Place place)
        {
            if (place != null)
            {
                _context.Add(place);
                _context.SaveChanges();
                return place;
            }
            return null;
           
        }

        public bool DeleteById(int id)
        {
            var place =  _context.Places.SingleOrDefault(x => x.Id == id);
            if (place != null)
            {
                _context.Places.Remove(place);
                _context.SaveChanges();
                return true;
            }
            return false;   
        }

        public Place Update(int id, Place updatePlace)
        {
            var findPlace = _context.Places.SingleOrDefault(x => x.Id == id);
            if (findPlace != null)
            {
                findPlace.Number = updatePlace.Number;
                findPlace.Street = updatePlace.Street;
                findPlace.City = updatePlace.City;

                _context.Update(findPlace);
                _context.SaveChanges();

                return findPlace;
            }
            return null;
        }
    }
}
