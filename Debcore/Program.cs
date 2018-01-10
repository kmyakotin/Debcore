using Debcore.Model;
using System;
using System.Linq;

namespace Debcore
{
    class Program
    {
        static void Main(string[] args)
        {
            var party = new Party("test");

            //manager.AddParticipant("Андрей");
            //manager.AddParticipant("Анна К.");
            //manager.AddParticipant("Анна П.");
            //manager.AddParticipant("Анна Х.").Buy(manager.AddProduct("вино", 1160));
            //manager.AddParticipant("Антон");
            //manager.AddParticipant("Вова");
            //manager.AddParticipant("Денис");
            //manager.AddParticipant("Дима").Buy(manager.AddProduct("ballentimes", 2800));
            //manager.AddParticipant("Костя").Buy(manager.AddProduct("крепкое", 6028));
            //manager.AddParticipant("Ксюша");
            //manager.AddParticipant("Маша");
            //manager.AddParticipant("Настя");
            //manager.AddParticipant("Наташа");
            //manager.AddParticipant("Полина").Buy(manager.AddProduct("розовое", 600));
            //manager.AddParticipant("Саша");
            //manager.AddParticipant("Сусанна");
            //manager.AddParticipant("Юра");
            //manager.AddParticipant("Ян").Buy(manager.AddProduct("продукты", 5642));
            //manager.AddParticipant("Яна");

            foreach (var p in party.Participants)
            {
                p.Consume(party.Products);
            }

            party.Participants.ForEach(x => Console.WriteLine($"{x.Name} {x.GetCalcDeb()}"));

            Console.WriteLine(); Console.WriteLine();
            party.GetDebsDetails().ForEach(x => Console.WriteLine(x.ToString()));

            Console.ReadKey();
        }
    }
}
