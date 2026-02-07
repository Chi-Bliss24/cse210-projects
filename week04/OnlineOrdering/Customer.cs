using System;

namespace OnlineOrderingProgram
{
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
}