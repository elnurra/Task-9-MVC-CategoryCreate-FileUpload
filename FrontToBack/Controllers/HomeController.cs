using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.Services.Basket;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;


        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            
        }

        public IActionResult Index()
        {
          
            HomeVM homeVM = new HomeVM();
            homeVM.Sliders=_appDbContext.Sliders.ToList();
            homeVM.SliderDetails = _appDbContext.SliderDetails.FirstOrDefault();
            homeVM.Categories = _appDbContext.Category.ToList();
            homeVM.Products = _appDbContext.Products.Include(product=>product.ProductImages).ToList();
            
            return View(homeVM);
        }

       
    }
}