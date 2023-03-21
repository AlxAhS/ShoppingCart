using Microsoft.AspNetCore.Mvc;
using ShoppingCartMvcUI.Models;
using ShoppingCartMvcUI.Models.DTOs;
using ShoppingCartMvcUI.Repositories.Interfaces;
using System.Diagnostics;
using System.Security.Policy;

namespace ShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task <IActionResult> IndexQ(string sterm = "", int genreId = 0)
        {
            IEnumerable<Book> books = await _homeRepository.GetBooks(sterm, genreId);
            IEnumerable<Genre> genres = await _homeRepository.Genres();
            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books = books,
                Genres = genres,
                STerm = sterm,
                GenreId = genreId,
            };
            return View(bookModel);

            //First way to switch views it's modified pattern line on <program.cs> file, I had switch from Index to IndexQ, so I could switch again from IndexQ to SecondView, but in this case I need make the specific method those will work like action. 
            //Other way to switch initial view it's create a new method with the name "SecondView" and change the references on views files like <IndexQ.cshtml> or <_layout.cshtml>. 
            //And finally, a third way to switch a view from a specific class it's just write a parameter on class return action. In this case View("SecondView");
        }

        public IActionResult Privacy()
        {
            return View("SecondView");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SecondView() 
        {
            return View();
        }

    }
}