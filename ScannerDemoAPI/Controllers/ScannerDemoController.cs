using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScannerDemoAPI.Data;
using ScannerDemoAPI.Models;
using ScannerDemoAPI.Models.DTO;
using System.Threading.Tasks;

namespace ScannerDemoAPI.Controllers
{
    [Route("api/scannerdemo")]
    [ApiController]
    public class ScannerDemoController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ScannerDemoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from ScannerDemo API!");
        }

        [Authorize]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserInfoDto userInfo)
        {
            if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.Name) || string.IsNullOrWhiteSpace(userInfo.Password))
                return BadRequest("Username and password are required.");

            var user = await _db.UserInfos
                .FirstOrDefaultAsync(u => u.Name == userInfo.Name && u.Password == userInfo.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok("Login successful.");
        }

        [Authorize]
        [HttpGet("getcartitems")]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _db.CartItems
                .Select(c => new CartItemDto
                {
                    Barcode = c.Barcode,
                    Quantity = c.Quantity
                })
                .ToListAsync();
            return Ok(cartItems);
        }

        [Authorize]
        [HttpPost("upsertcartitem")]
        public async Task<IActionResult> UpsertCartItem([FromBody] CartItemDto cartItemDto)
        {
            if (cartItemDto == null || string.IsNullOrWhiteSpace(cartItemDto.Barcode))
                return BadRequest("Barcode is required.");

            var existingItem = await _db.CartItems.FirstOrDefaultAsync(c => c.Barcode == cartItemDto.Barcode);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                _db.CartItems.Update(existingItem);
            }
            else
            {
                var newItem = new CartItem { Barcode = cartItemDto.Barcode, Quantity = 1 };
                await _db.CartItems.AddAsync(newItem);
            }
            await _db.SaveChangesAsync();
            return Ok("Cart item upserted successfully.");
        }

        [Authorize]
        [HttpPost("removecartitem")]
        public async Task<IActionResult> RemoveCartItem([FromBody] CartItemDto cartItemDto)
        {
            if (cartItemDto == null || string.IsNullOrWhiteSpace(cartItemDto.Barcode))
                return BadRequest("Barcode is required.");

            var existingItem = await _db.CartItems.FirstOrDefaultAsync(c => c.Barcode == cartItemDto.Barcode);
            if (existingItem == null)
                return NotFound("Cart item not found.");

            if (existingItem.Quantity > 1)
            {
                existingItem.Quantity--;
                _db.CartItems.Update(existingItem);
            }
            else
            {
                _db.CartItems.Remove(existingItem);
            }
            await _db.SaveChangesAsync();
            return Ok("Cart item removed successfully.");
        }
    }
}
