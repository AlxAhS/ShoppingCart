using Microsoft.EntityFrameworkCore;
using ShoppingCartMvcUI.Models;
using ShoppingCartMvcUI.Repositories.Interfaces;

namespace ShoppingCartMvcUI.Repositories
{
    public class StoreSettingRepository : IStoreSettingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StoreSettingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> BooksList()
        {
            var books = await _dbContext.Books
                            .Include(x => x.Id)
                            .Include(x => x.BookName)
                            .Include(x => x.AuthorName)
                            .Include(x => x.Price)
                            .ToListAsync();
            if(books == null) { throw new Exception("No books on list"); }
            return books;
        }


        //public async Task<IActionResult> CreateBook(Book book)
        //{
        //    if (book == null)
        //    {
        //        throw new Exception("Book can not be created");
        //    }

        //    return book;

        //}
    }
}
