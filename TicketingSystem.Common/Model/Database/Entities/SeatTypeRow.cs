using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace TicketingSystem.Common.Model.Database.Entities
{
    [Table("SeatTypes")]
    public class SeatTypeRow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required int SeatTypeId { get; set; }
        public required string SeatType { get; set; }
    }
}
