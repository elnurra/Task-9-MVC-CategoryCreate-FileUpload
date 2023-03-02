using FrontToBack.DAL;

namespace FrontToBack.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _appDbContext;

        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Models.Product> GetAllProducts()
        {
            return _appDbContext.Products.ToList();
        }
    }
}
