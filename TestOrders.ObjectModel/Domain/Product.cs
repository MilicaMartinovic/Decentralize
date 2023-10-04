using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOrders.ObjectModel.Base;

namespace TestOrders.ObjectModel.Domain
{

    public class Product : BaseEnitty
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
