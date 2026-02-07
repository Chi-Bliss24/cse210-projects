using System;
using System.Collections.Generic;

namespace OnlineOrderingProgram
{
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
}