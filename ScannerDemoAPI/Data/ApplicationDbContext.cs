using Microsoft.EntityFrameworkCore;
using ScannerDemoAPI.Models;
using ScannerDemoAPI.Models.DTO;

namespace ScannerDemoAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; } 
        public DbSet<CartItem> CartItems { get; set; }
    }
}
