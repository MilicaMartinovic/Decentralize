using TestOrders.ObjectModel.Domain;

namespace TestOrders.Persistance
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product{Name="Product 1", Price=10m},
                new Product{Name="Product 2", Price=20m},
                new Product{Name="Product 3", Price=30m},
                new Product{Name="Product 4", Price=40m},
                new Product{Name="Product 5", Price=50m},
                new Product{Name="Product 6", Price=60m},
                new Product{Name="Product 7", Price=70m},
                new Product{Name="Product 8", Price=80m},
                new Product{Name="Product 9", Price=90m},
                new Product{Name="Product 10", Price=100m},
            };

            foreach (var product in products)
            {
                if (!context.Products.Any(p => p.Name == product.Name))
                {
                    context.Products.Add(product);
                }
            }

            var customers = new Customer[]
            {
                new Customer{Name="Customer 1"},
                new Customer{Name="Customer 2"},
            };
            context.Customers.AddRange(customers);

            var orders = new Order[]
            {
                new Order{Customer=customers[0], Date=DateTime.Now},
                new Order{Customer=customers[1], Date=DateTime.Now.AddDays(-1)},
            };
            context.Orders.AddRange(orders);

            context.SaveChanges();

            var random = new Random();

            foreach (var order in orders)
            {
                foreach (var product in products)
                {
                    var orderLine = new OrderLine
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = random.Next(1,9),
                        PriceAtOrder = product.Price 
                    };

                    context.OrderLines.Add(orderLine);
                }
            }

            context.SaveChanges();
        }

    }

}
