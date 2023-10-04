using Newtonsoft.Json;
using TestOrdersConsole.Models;

class Program
{
    private const string ApiBaseUrl = "http://localhost:5052/products"; 

    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. View top 10 products by volume");
            Console.WriteLine("2. View top 10 products by dollar amount sold");
            Console.WriteLine("3. Exit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await GetTopProducts("volume");
                    break;
                case "2":
                    await GetTopProducts("amount");
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static async Task GetTopProducts(string sortBy)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"{ApiBaseUrl}/top-products?sortBy={sortBy}");

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(jsonResponse) ?? new List<ProductDTO>();

            Console.WriteLine($"Top 10 products by {sortBy}:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.ProductId} - {product.Name} - ${product.Price}");
            }
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }
}
