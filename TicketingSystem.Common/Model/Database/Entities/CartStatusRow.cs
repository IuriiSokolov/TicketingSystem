using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Common.Model.Database.Entities
{
    [Table("CartStatuses")]
    public class CartStatusRow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required int CartStatusId { get; set; }
        public required string Status { get; set; }
    }
}
