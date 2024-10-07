using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Common.Model.Database
{
    [Table("CartStatuses")]
    public class CartStatusRow
    {
        [Key]
        public required int CartStatusId { get; set; }
        public required string Status { get; set; }
    }
}
