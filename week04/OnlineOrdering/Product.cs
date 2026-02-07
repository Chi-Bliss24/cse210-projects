using System;

namespace OnlineOrderingProgram
{
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
}