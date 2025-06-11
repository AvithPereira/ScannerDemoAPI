using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using ScannerDemoAPI.Models;
using ScannerDemoAPI.Models.DTO;

namespace ScannerDemoAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // these for OpenIddict
        public DbSet<OpenIddictEntityFrameworkCoreApplication> OpenIddictApplications { get; set; }
        public DbSet<OpenIddictEntityFrameworkCoreAuthorization> OpenIddictAuthorizations { get; set; }
        public DbSet<OpenIddictEntityFrameworkCoreScope> OpenIddictScopes { get; set; }
        public DbSet<OpenIddictEntityFrameworkCoreToken> OpenIddictTokens { get; set; }
    }
}
