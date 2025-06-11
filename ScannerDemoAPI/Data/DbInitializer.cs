using OpenIddict.Abstractions;

namespace ScannerDemoAPI.Data
{
    public class DbInitializer
    {
        public static async Task SeedOpenIddictClientAsync(IServiceProvider services)
        {
            var manager = services.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("scannerdemomaui_client") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "scannerdemomaui_client",
                    DisplayName = "ScannerDemoMaui",
                    Permissions =
              {
                  OpenIddictConstants.Permissions.Endpoints.Token,
                  OpenIddictConstants.Permissions.GrantTypes.Password,
                  OpenIddictConstants.Permissions.GrantTypes.RefreshToken
              }
                });
            }
        }
    }
}
