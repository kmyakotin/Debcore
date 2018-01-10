using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Debcore.Model
{
    /// <summary>
    /// Class of party info, the root, the home of one single party. Keeps info about participants and products 
    /// </summary>
    public class Party
    {
        [BsonId]
        public ObjectId BsonId { get; set; }

        [BsonElement("partyId")]
        public Guid PartyId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Party(string name, string description = null)
        {
            Name = name;
            Description = description;
            PartyId = Guid.NewGuid();
        }

        public List<Person> Participants { get; set; } = new List<Person>();

        public List<Product> Products { get; set; } = new List<Product>();

        public Person AddParticipant(string name)
        {
            var p = new Person(name);
            Participants.Add(p);
            return p;
        }

        public Product AddProduct(string name, decimal price)
        {
            var p = new Product(name, price);
            Products.Add(p);
            return p;
        }

        public List<Debtor> GetDebsDetails()
        {
            var debtors = Participants.Where(x => x.GetCalcDeb() > 0).Select(x => new Debtor(x, x.GetCalcDeb()))
                .ToList();
            var borrowers = Participants.Where(x => x.GetCalcDeb() < 0)
                .Select(x => new Borrower(x, x.GetCalcDeb() * -1)).ToList();

            foreach (var d in debtors)
            {
                foreach (var b in borrowers)
                {
                    d.PayDeb(b);
                }
            }

            return debtors;
        }

        //todo remove test method
        public Party Testify()
        {
            this.AddParticipant("Андрей");
            this.AddParticipant("Анна К.");
            this.AddParticipant("Анна П.");
            this.AddParticipant("Анна Х.").Buy(this.AddProduct("вино", 1160));
            this.AddParticipant("Антон");
            this.AddParticipant("Вова");
            this.AddParticipant("Денис");
            this.AddParticipant("Дима").Buy(this.AddProduct("ballentimes", 2800));
            this.AddParticipant("Костя").Buy(this.AddProduct("крепкое", 6028));
            this.AddParticipant("Ксюша");
            this.AddParticipant("Маша");
            this.AddParticipant("Настя");
            this.AddParticipant("Наташа");
            this.AddParticipant("Полина").Buy(this.AddProduct("розовое", 600));
            this.AddParticipant("Саша");
            this.AddParticipant("Сусанна");
            this.AddParticipant("Юра");
            this.AddParticipant("Ян").Buy(this.AddProduct("продукты", 5642));
            this.AddParticipant("Яна");

            foreach (var p in this.Participants)
            {
                p.Consume(this.Products);
            }

            return this;
        }
    }
}