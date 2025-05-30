using System.ComponentModel.DataAnnotations;

namespace ScannerDemoAPI.Models.DTO
{
    public class CartItemDto
    {
        public string Barcode { get; set; }

        public int Quantity { get; set; }
    }
}
