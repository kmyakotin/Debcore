using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Debcore.Model
{
    class ParticipantsManager
    {
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
            var debtors = Participants.Where(x => x.GetCalcDeb() > 0).Select(x => new Debtor(x, x.GetCalcDeb())).ToList();
            var borrowers = Participants.Where(x => x.GetCalcDeb() < 0).Select(x => new Borrower(x, x.GetCalcDeb() * -1)).ToList();

            foreach (var d in debtors)
            {
                foreach (var b in borrowers)
                {
                    d.PayDeb(b);
                }
            }

            return debtors;
        }

        public void ParseScript(string script)
        {
            var lines = script.Split(new[] { "\r\n", ";" }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("lines:");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            foreach (var line in lines)
            {
                ParseLine(line);
            }
        }

        private void ParseLine(string line)
        {
            var name = new string(line.TakeWhile(c => !", ".Contains(c)).ToArray());
            var buyRx = new Regex(@"[\w_]*?\$\d+\.?\d*");
            var products = new Dictionary<string, decimal>();
            buyRx.Matches(line)
                .Select(x => x.Value)
                .ToList()
                .ForEach(m =>
                {
                    var args = m.Split('$');
                    products.Add(args[0], decimal.Parse(args[1]));
                });

            Console.WriteLine($"name {name}");
            foreach (var kvp in products)
            {
                Console.WriteLine($"{kvp.Key} {kvp.Value}");
            }

            //pice of shit
            var consume = new string(line.SkipWhile(x => x != ':').SkipWhile(x => !"*0".Contains(x)).ToArray());
            var isConsumeAll = consume[0] == '*';
            var removeRx= new Regex(@"\-[\w_а-яА-Я]*");
            var addRx= new Regex(@"\+[\w_а-яА-Я]*");
            var remove = removeRx.Matches(consume).Select(x => x.Value.TrimStart('-'));
            var add = addRx.Matches(consume).Select(x => x.Value.TrimStart('+'));

            Console.WriteLine($"{(isConsumeAll ? "allExcept" : "None bug")}");
            Console.WriteLine("remove \t");
            foreach (var r in remove)
            {
                Console.WriteLine(r);
            }

            Console.WriteLine("add \t");
            foreach (var r in add)
            {
                Console.WriteLine(r);
            }
        }
    }
}