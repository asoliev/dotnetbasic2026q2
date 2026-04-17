using System;

namespace Task1;
public class Product(string name, double price)
{
    public string Name { get; set; } = name;
    public double Price { get; set; } = price;

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
