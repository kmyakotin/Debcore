using System;
using System.Collections.Generic;
using System.Linq;

namespace Debcore.Model
{
    public class Debtor
    {
        public Person Person { get; set; }

        public decimal InitialDeb { get; }
        public Dictionary<Borrower, decimal> Borrowers { get; private set; } = new Dictionary<Borrower, decimal>();
        public decimal Debt { get; private set; }

        public Debtor(Person debtor, decimal deb)
        {
            Person = debtor;
            Debt = InitialDeb = deb;
        }

        public void PayDeb(Borrower b)
        {
            var value = Math.Min(b.Borrow, Debt);
            if (value <= 0)
                return;

            Debt -= value;
            Borrowers.Add(b, value);
            b.Decrease(value);
        }

        public override string ToString()
        {
            var separator = "\t";
            return $"{Person.Name}{separator}{Math.Round(InitialDeb, 2)}{separator}" +
                   $"{string.Concat(Borrowers.Select(x => $"\r\n{separator}debs to{separator}{x.Key.Person.Name}{separator}{Math.Round(x.Value, 2)}"))}";
        }
    }

    public class Borrower
    {
        public Person Person { get; set; }
        public decimal InitialBorrow { get; }
        public decimal Borrow { get; private set; }

        public Borrower(Person person, decimal borrows)
        {
            Person = person;
            InitialBorrow = borrows;
            Borrow = borrows;
        }

        public void Decrease(decimal value)
        {
            if (Borrow < value)
                throw new Exception("невозможно вернуть долга больше, чем был займ");

            Borrow -= value;
        }
    }
}