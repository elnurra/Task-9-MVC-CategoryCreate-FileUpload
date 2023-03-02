
using FrontToBack.Services.Basket;
using FrontToBack.Services.Product;

namespace FrontToBack
{
    public static class ServiceRegistration
    {

        public static void FrontToBackServiceRegistration(this IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.AddHttpContextAccessor();
            //scope,transient and singleton 
            services.AddScoped<IBasketProductCount,BasketProductCount>();
            services.AddScoped<IProductService,ProductService>();
       
           

        }
    }
}
