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
        public List<Guid> ParticipantsGuids { get; set; } = new List<Guid>();

        internal decimal PricePerPerson()
        {
            return decimal.Divide(Price, ParticipantsGuids.Count);
        }
    }
}