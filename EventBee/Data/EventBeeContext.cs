using EventBee.Models;
using EventBee.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Data
{
    public class EventBeeContext : DbContext
    {
        public EventBeeContext(DbContextOptions<EventBeeContext> options) : base(options)
        {

        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<UserEvents> UserEvents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
