using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Models.Order
{
    public class OrderDetails : BaseModel
    {
        [Required]
        public required string PurchaseMethod { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime DateOfPurchase { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime DateOfDelivery { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime DateOfShipping { get; set; }
    }
}
