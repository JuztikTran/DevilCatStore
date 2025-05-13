using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.Order
{
    public class OrderTracking : BaseModel
    {
        [Required]
        public required string OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        [Required]
        [StringLength(100)]
        public required string Message { get; set; }

        [Required]
        [StringLength(150)]
        public required string Location { get; set; }

        [StringLength(100)]
        public string? CourierInfo { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime TimeTracker { get; set; }
    }
}
