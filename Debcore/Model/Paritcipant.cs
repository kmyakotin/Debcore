using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace Debcore.Model
{
    public class Person
    {
        public Person()
        {
            Id = Guid.NewGuid();
        }

        public Person(string name) : this()
        {
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; set; }
        public List<Product> BuyProducts { get; set; } = new List<Product>();
        public List<Product> ConsumeProducts { get; set; } = new List<Product>();

        /// <summary>
        /// сколько должен - разница между потратил и потребил и уплатил
        /// </summary>
        /// <returns></returns>
        internal decimal GetCalcDeb()
        {
            return ConsumeProducts.Sum(p => p.PricePerPerson()) - GetPaied();
        }

        /// <summary>
        ///  потратил
        /// </summary>
        /// <returns></returns>
        public decimal GetPaied()
        {
            return BuyProducts.Sum(x => x.Price);
        }

        public Person Buy(Product product, bool isConsume = true)
        {
            BuyProducts.Add(product);
            if (isConsume)
                return Consume(product);
            return this;
        }

        private Person Consume(Product product)
        {
            if (product.ParticipantsGuids.Any(pp => pp == this.Id))
                return this;

            product.ParticipantsGuids.Add(this.Id);
            ConsumeProducts.Add(product);
            return this;
        }

        public void Consume(IEnumerable<Product> products)
        {
            foreach (var p in products)
            {
                Consume(p);
            }
        }

        public void RemoveConsume(Product product)
        {
            product.ParticipantsGuids.Remove(this.Id);
            ConsumeProducts.Remove(product);
        }
    }
}