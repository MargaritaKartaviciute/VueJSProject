using EventBee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Repositories.Interfaces
{
    public interface IPlaceRepository
    {
        List<Place> GetAll();
        Place GetById(int id);
        Place Add(Place place);
        bool DeleteById(int id);
        Place Update(int id, Place updatePlace);
    }
}
