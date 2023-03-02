using FrontToBack.DAL;
using FrontToBack.Extensions;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FrontToBack.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext appDbContext, IWebHostEnvironment env = null)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SliderCreateVM sliderCreateVM)
        {
            if (sliderCreateVM.Photo==null)
            {
                ModelState.AddModelError("Photo", "Upploaded is empty");
                return View();

            }
            if (!sliderCreateVM.Photo.isImage())
            {
                ModelState.AddModelError("Photo", "Should be only photo extensions f.e: png,jpg");
                return View();
            }
            if (sliderCreateVM.Photo.CheckImageSize(500))
            {
                ModelState.AddModelError("Photo", "Photo not be bigger than 500kb");
                return View();
            }

            Slider newSlider = new Slider();
            newSlider.ImageUrl = sliderCreateVM.Photo.SaveImage(_env,"img",sliderCreateVM.Photo.FileName);
            _appDbContext.Sliders.Add(newSlider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
