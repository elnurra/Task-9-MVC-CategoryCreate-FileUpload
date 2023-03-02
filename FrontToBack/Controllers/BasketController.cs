using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace FrontToBack.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _appDbContext;


        public BasketController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("name", "Ibrahim");
            return Content("Set olundu");
        }
        public async Task<IActionResult> Add(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = await _appDbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            List<BasketVM> products;
            if (Request.Cookies["basket"] == null)
            {
                products = new();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            BasketVM existproduct = products.FirstOrDefault(p => p.Id == id);
            if (existproduct == null)
            {
                BasketVM basketVM = new();
                basketVM.Id = product.Id;
                basketVM.BasketCount = 1;
                products.Add(basketVM);

            }

            else
            {
                existproduct.BasketCount++;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromDays(30) });
            return RedirectToAction(nameof(ShowBasket));
        }
        public async Task<IActionResult> Delete(int id)
        {
            
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM existproduct = products.FirstOrDefault(p => p.Id == id);
            products.Remove(existproduct);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromDays(30) });
            return RedirectToAction(nameof(ShowBasket));
        }
        public async Task<IActionResult> Remove(int id)
        {

            var products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM existproduct = products.FirstOrDefault(p => p.Id == id);
            if (existproduct.BasketCount==1)
            {
                products.Remove(existproduct);
            }
            else
            {
                existproduct.BasketCount--;
            }
          
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromDays(30) });
            return RedirectToAction(nameof(ShowBasket));
        }






        public IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products;
            if (basket==null)
            {
                products = new();
            }
            else
            {
              products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (var item in products)
                {
                    Product currentProduct = _appDbContext.Products.Include(p=>p.ProductImages).FirstOrDefault(p=>p.Id==item.Id);
                    item.Name = currentProduct.Name;
                    item.Price = currentProduct.Price;
                    item.Id= currentProduct.Id;
                    item.ImageUrl = currentProduct.ProductImages.FirstOrDefault().ImageUrl;

                }
            }
          

            return View(products);
        }
    }
}
//BasketVM existproduct = products.FirstOrDefault(p => p.Id == id);
//if (existproduct != null)
//{
//    BasketVM basketVM = new();
//    basketVM.Id = product.Id;
//    products.Remove(basketVM);

//}