using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketingSystem.Common.Model.Database
{
    [Table("SeatStatuses")]
    public class SeatStatusRow
    {
        [Key]
        public required int SeatStatusId { get; set; }
        public required string Status { get; set; }
    }
}
