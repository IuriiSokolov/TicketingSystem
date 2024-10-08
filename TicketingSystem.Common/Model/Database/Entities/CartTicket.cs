using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Common.Model.Database.Entities
{
    [PrimaryKey(nameof(CartId), nameof(TicketId))]
    public class CartTicket
    {
        public int CartId { get; set; }
        public int TicketId { get; set; }
        public Cart Cart { get; set; } = null!;
        public Ticket Ticket { get; set; } = null!;
    }
}
