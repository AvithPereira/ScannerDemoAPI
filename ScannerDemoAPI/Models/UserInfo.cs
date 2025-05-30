using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScannerDemoAPI.Models
{
    public class UserInfo
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DisplayName("Full Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
