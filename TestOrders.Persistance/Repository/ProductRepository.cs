using TestOrders.ObjectModel.Domain;
using TestOrders.ObjectModel.IRepository;

namespace TestOrders.Persistance.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetTopProducts(string sortBy, int count)
        {
            using var context = new AppDbContext();

            if (sortBy == "amount")
            {
                return context.Products
                              .OrderByDescending(p => p.OrderLines.Sum(ol => ol.PriceAtOrder * ol.Quantity))
                              .Take(count)
                              .ToList();
            }

            return context.Products
                          .OrderByDescending(p => p.OrderLines.Sum(ol => ol.Quantity))
                          .Take(count)
                          .ToList();
        }
    }
}
