using System;
using System.Collections.Generic;
using System.Globalization;

namespace OnlineOrderingProgram
{
    // Address class (encapsulates street, city, state/province, country)
    public class Address
    {
        private string _street;
        private string _city;
        private string _stateOrProvince;
        private string _country;

        public Address(string street, string city, string stateOrProvince, string country)
        {
            _street = street ?? throw new ArgumentNullException(nameof(street));
            _city = city ?? throw new ArgumentNullException(nameof(city));
            _stateOrProvince = stateOrProvince ?? throw new ArgumentNullException(nameof(stateOrProvince));
            _country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public bool IsInUSA()
        {
            // Normalize to allow "USA", "United States", "United States of America"
            var c = _country.Trim().ToLowerInvariant();
            return c == "usa" || c == "united states" || c == "united states of america" || c == "us";
        }

        public override string ToString()
        {
            // Multi-line representation for shipping label
            return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
        }

        // Optional read-only properties
        public string Street => _street;
        public string City => _city;
        public string StateOrProvince => _stateOrProvince;
        public string Country => _country;
    }

    // Customer class (encapsulates name and Address)
    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public string Name => _name;
        public Address Address => _address;

        public bool LivesInUSA()
        {
            return _address.IsInUSA();
        }
    }

    // Product class (encapsulates name, id, price, quantity)
    public class Product
    {
        private string _name;
        private string _productId;
        private decimal _pricePerUnit;
        private int _quantity;

        public Product(string name, string productId, decimal pricePerUnit, int quantity)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _productId = productId ?? throw new ArgumentNullException(nameof(productId));
            if (pricePerUnit < 0) throw new ArgumentOutOfRangeException(nameof(pricePerUnit));
            if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            _pricePerUnit = pricePerUnit;
            _quantity = quantity;
        }

        public string Name => _name;
        public string ProductId => _productId;
        public decimal PricePerUnit => _pricePerUnit;
        public int Quantity => _quantity;

        public decimal GetTotalCost()
        {
            return _pricePerUnit * _quantity;
        }
    }

    // Order class (encapsulates list of products and the customer)
    public class Order
    {
        private List<Product> _products = new List<Product>();
        private Customer _customer;

        public Order(Customer customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }

        public void AddProduct(Product p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            _products.Add(p);
        }

        public IReadOnlyList<Product> Products => _products.AsReadOnly();
        public Customer Customer => _customer;

        private decimal GetShippingCost()
        {
            // Shipping cost rules: USA = $5, non-USA = $35
            return _customer.LivesInUSA() ? 5m : 35m;
        }

        public decimal CalculateTotalPrice()
        {
            decimal subtotal = 0m;
            foreach (var p in _products)
            {
                subtotal += p.GetTotalCost();
            }

            return subtotal + GetShippingCost();
        }

        public string GetPackingLabel()
        {
            // Lists the name and product id of each product
            var lines = new List<string> { "Packing Label:" };
            foreach (var p in _products)
            {
                lines.Add($"{p.Name} (ID: {p.ProductId})");
            }
            return string.Join("\n", lines);
        }

        public string GetShippingLabel()
        {
            // Lists the name and address of the customer
            return $"Shipping Label:\n{_customer.Name}\n{_customer.Address}";
        }
    }

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
