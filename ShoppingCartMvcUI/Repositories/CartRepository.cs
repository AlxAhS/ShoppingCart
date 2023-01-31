using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ShoppingCartMvcUI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(ApplicationDbContext dbContext,IHttpContextAccessor httpContextAccesor, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor= httpContextAccesor;
        }
        public async Task<bool> AddItem(int bookId, int qty) 
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                string userId = GetUserId();
                if(string.IsNullOrEmpty(userId)) 
                {
                    return false;            
                }

                var cart = await GetCart(userId);
                if (cart is null) 
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId
                    };
                    _dbContext.ShoppingCarts.Add(cart); 
                }

                _dbContext.SaveChanges();

                var cartItem = _dbContext.CartDetails.FirstOrDefault(a=>a.ShoppingCart_Id== cart.Id && a.BookId == bookId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookId = bookId,
                        ShoppingCart_Id = cart.Id,
                        Quantity = qty
                    };
                    _dbContext.CartDetails.Add(cartItem);
                }
                _dbContext.SaveChanges();
                transaction.Commit();
                return true;    
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveItem(int bookId)
        {
            try
            {
                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }

                var cart = await GetCart(userId);
                if (cart is null)
                {
                    return false;
                }


                var cartItem = _dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCart_Id == cart.Id && a.BookId == bookId);
                if (cartItem is null)
                {
                    return false;
                }

                else if (cartItem.Quantity == 1)
                {
                    _dbContext.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ShoppingCart>>GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
            { throw new Exception("Invalid userId"); }
            var shoppingCart = await _dbContext.ShoppingCarts
                                .Include(a => a.CartDetails)
                                .ThenInclude(a => a.Book)
                                .ThenInclude(a => a.Genre)
                                .Where(a => a.UserId == userId).ToListAsync();
            return shoppingCart;
        }

        private async Task <ShoppingCart> GetCart(string userId) 
        {
            var cart = await _dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }

        Task<ShoppingCart> ICartRepository.GetCart(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
