using TestOrders.ObjectModel.IService;
using TestOrders.ObjectModel.Domain;
using TestOrders.ObjectModel.IRepository;
using TestOrders.ObjectModel.DTOs;
using AutoMapper;

namespace TestOrders.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductDTO> GetTopProducts(string sortBy, int count)
        {
            var products = _productRepository.GetTopProducts(sortBy, count) ?? new List<Product>();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
        }
    }
}