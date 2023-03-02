using FrontToBack.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private static int Skip { get; set; } = 2;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var query = _appDbContext.Products.
                Include(products => products.ProductImages).
                Include(products => products.Category);

            ViewBag.ProductCount = query.Count();
            var products = query.
                Take(2).
                ToList();
            return View(products);
        }

        public IActionResult LoadMore(int skip)
        {
            var products = _appDbContext.Products.
                Skip(skip).
                Include(products=>products.ProductImages).
                Include(products => products.Category).
                Take(2).
                ToList();

            return PartialView("_ProductLoadMorePartial",products);
        }
    }
}
