using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartMvcUI
{
    public interface ICartRepository
    {
        Task<bool> AddItem(int bookId, int qty);
        Task<bool> RemoveItem(int bookId);
        Task<IEnumerable<ShoppingCart>> GetUserCart(); 
    }
}