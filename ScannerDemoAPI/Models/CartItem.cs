using System.ComponentModel.DataAnnotations;

namespace ScannerDemoAPI.Models.DTO
{
    public class CartItem
    {
        [Key]
        [Required]
        public string Barcode { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
