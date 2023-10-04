using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOrders.ObjectModel.Domain;
using TestOrders.ObjectModel.DTOs;

namespace TestOrders.ObjectModel.IRepository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetTopProducts(string sortBy, int count);
    }
}
