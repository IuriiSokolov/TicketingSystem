using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Common.Model.Database;

namespace TicketingSystem.Common.Context
{
    public interface ITicketingDbContext
    {
        DbSet<Venue> Venues { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<Seat> Seats { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Ticket> Tickets { get; set; }
    }
}
