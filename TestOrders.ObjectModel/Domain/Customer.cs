using TestOrders.ObjectModel.Base;

namespace TestOrders.ObjectModel.Domain
{
    public class Customer : BaseEnitty
    {
        public string Name { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }

}