using TestOrders.ObjectModel.Base;

namespace TestOrders.ObjectModel.Domain
{
    public class OrderLine : BaseEnitty
    {
        public Guid OrderId { get; set; }
        public virtual  Order Order { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtOrder { get; set; }
    }
}
