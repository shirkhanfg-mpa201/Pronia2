using Microsoft.EntityFrameworkCore;
using Pronia2.Abstractions;
using Pronia2.Contexts;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pronia2.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly AppDbContext _context;

        public BasketService(IHttpContextAccessor accessor, AppDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            var usedId = _accessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isExistUser = await _context.Users.AnyAsync(u => u.Id == usedId);

            if (!isExistUser)
            {
                return new List<BasketItem>();
            }

            var basketItems = await _context.BasketItems
                .Include(b => b.Product)
                .Where(b => b.AppUserId == usedId)
                .ToListAsync();

            return basketItems;
        }
    }
}
