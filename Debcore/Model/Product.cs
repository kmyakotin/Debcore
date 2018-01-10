using System;
using System.Collections.Generic;

namespace Debcore.Model
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Product(string name, decimal price) : this()
        {
            Name = name;
            Price = price;
        }

        public Guid Id { get; }
        public string Name { get; set; }

        public decimal Price { get; set; }
//        public List<Person> Participants { get; set; } = new List<Person>();

        internal decimal PricePerPerson()
        {
            throw new NotImplementedException("uncomment stub");
//            return decimal.Divide(Price, Participants.Count);
        }
    }
}