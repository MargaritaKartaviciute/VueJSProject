using EventBee.Data;
using EventBee.Models;
using EventBee.Models.Users;
using EventBee.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventBee.Helpers
{
    public class Seed
    {

        public static void SeedData(EventBeeContext _db)
        {
            if (_db != null && !_db.Events.Any())
            {
                var places = new Place[] {
                    new Place{City = "Kaunas", Street = "K.Donelaičio", Number = 62},
                    new Place{City = "Kaunas", Street = "Studentų", Number = 71},
                    new Place{City = "Kaunas", Street = "Sporto", Number = 10},
                    new Place{City = "Kaunas", Street = "Simono Daukanto", Number = 50},
                };
                _db.Places.AddRange(places);
                _db.SaveChanges();
                _db.Events.AddRange(new Event[] {
                    new Event{Name = "You can code",Date = Convert.ToDateTime("2019-12-07 9:00"), Place = places[0], Price = 10, Description = "Programavimo renginys visiems, norintiems išmokti programuoti", Image = "https://www.fintp.org/wp-content/uploads/2014/10/baracktocat.jpg"},
                    new Event{Name = ".NET Core Meetup",Date = Convert.ToDateTime("2019-12-16 12:00"), Place = places[1], Price = 10, Description = "Programavimo renginys besidomintiems .NET Core naujienomis", Image = "https://www.fintp.org/wp-content/uploads/2014/10/baracktocat.jpg"},
                    new Event{Name = "Kalėdinis vakarėlis", Date = Convert.ToDateTime("2019-12-25 9:00"), Place = places[2], Price = 10, Description = "Kauno miesto kalėdinis renginys, suburiantis visą bendruomenę.", Image = "https://www.medinisarkliukas.lt/images/uploader/ka/kaledinis-zaisliukas-eglute-2-1.jpg"},
                    new Event{Name = "KTU amsamblio koncertas",Date = Convert.ToDateTime("2019-12-29 11:00"), Place = places[3], Price = 10, Description = "Kultūrinys renginys, skirtas visai KTU bendruomenei ir ne tik.", Image = "https://i.ytimg.com/vi/J-dKjgpCoP4/maxresdefault.jpg"},
                });
                _db.SaveChanges();

            }
            if (_db != null && !_db.Users.Any())
            {
                var salt = new AppSettings().Salt;
                var user = new User { FirstName = "admin", Surname = "admin", Username = "admin", Password = "admin", Role = Role.Admin };
                user.Password = Password.hashPassword(user.Password + salt);
                user.Role = Role.Admin;
                _db.Users.Add(user);
                _db.SaveChanges();
            }
        }
    }
}
