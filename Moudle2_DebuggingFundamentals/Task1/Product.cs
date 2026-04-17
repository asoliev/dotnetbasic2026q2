using System;

namespace Task1
{
    public class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Product other)
                return false;

            return string.Equals(Name, other.Name, StringComparison.Ordinal)
                && Price.Equals(other.Price);
        }

        public override int GetHashCode() => HashCode.Combine(Name, Price);
    }
}
