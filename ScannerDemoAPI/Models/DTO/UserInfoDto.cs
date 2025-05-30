using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ScannerDemoAPI.Models.DTO
{
    public class UserInfoDto
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }
}
