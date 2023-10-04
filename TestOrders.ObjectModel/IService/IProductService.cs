using TestOrders.ObjectModel.DTOs;

namespace TestOrders.ObjectModel.IService
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetTopProducts(string sortBy, int count);
    }
}
