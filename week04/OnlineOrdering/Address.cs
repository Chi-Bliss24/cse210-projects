using System;

namespace OnlineOrderingProgram
{
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
}