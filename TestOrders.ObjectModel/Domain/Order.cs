using TestOrders.ObjectModel.Base;

namespace TestOrders.ObjectModel.Domain
{
    public class Order : BaseEnitty
    {
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
