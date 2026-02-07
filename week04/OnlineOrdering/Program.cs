using System;
using System.Collections.Generic;
using System.Globalization;

namespace OnlineOrderingProgram
{
    class Program
    {
        static void Main()
        {
            // Force consistent currency display in USD
            var currencyCulture = CultureInfo.CreateSpecificCulture("en-US");

            // Create addresses
            var addr1 = new Address("123 Main St", "Boise", "ID", "United States");
            var addr2 = new Address("77 King Road", "Toronto", "ON", "Canada");

            // Create customers
            var custUS = new Customer("Alice Johnson", addr1);
            var custCAN = new Customer("Marco Silva", addr2);

            // Build first order (US customer)
            var order1 = new Order(custUS);
            order1.AddProduct(new Product("Travel Mug", "TM-001", 12.99m, 2));
            order1.AddProduct(new Product("Sticker Pack", "SP-022", 4.50m, 3));

            // Build second order (non-US customer)
            var order2 = new Order(custCAN);
            order2.AddProduct(new Product("Wireless Charger", "WC-100", 29.99m, 1));
            order2.AddProduct(new Product("Phone Case", "PC-303", 15.00m, 2));
            order2.AddProduct(new Product("Tote Bag", "TB-212", 9.50m, 1));

            // Put orders in a list and display details
            var orders = new List<Order> { order1, order2 };

            int orderNo = 1;
            foreach (var ord in orders)
            {
                Console.WriteLine("===================================================");
                Console.WriteLine($"ORDER #{orderNo++}");
                Console.WriteLine();

                Console.WriteLine(ord.GetPackingLabel());
                Console.WriteLine();
                Console.WriteLine(ord.GetShippingLabel());
                Console.WriteLine();

                decimal total = ord.CalculateTotalPrice();
                Console.WriteLine($"Total Price (including shipping): {total.ToString("C", currencyCulture)}");
                Console.WriteLine();

                // Optionally show breakdown
                Console.WriteLine("Itemized:");
                foreach (var p in ord.Products)
                {
                    Console.WriteLine($" - {p.Name} (ID: {p.ProductId}), {p.Quantity} × {p.PricePerUnit.ToString("C", currencyCulture)} = {p.GetTotalCost().ToString("C", currencyCulture)}");
                }
                Console.WriteLine($"Shipping: {(ord.Customer.LivesInUSA() ? 5m : 35m).ToString("C", currencyCulture)}");
                Console.WriteLine();
            }

            Console.WriteLine("===================================================");
            Console.WriteLine("End of demo. Press any key to exit...");
            Console.ReadKey();
        }
    }
}