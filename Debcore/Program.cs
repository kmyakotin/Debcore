using Debcore.Model;
using System;
using System.Linq;

namespace Debcore
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new ParticipantsManager();

            if (args.Length == 0)
            {
                Console.WriteLine("Test args");
                var script = 
                    //or some thing like this, or fuck it
@"вова,вино$100,milk$33:*-вино-алкашка-мясо+вино+алкашка+мясо;
маша,алкашка$200:0
коля,0$0:*-вино -мясо;";
                manager.ParseScript(script);
            }
            else if (args.Length == 1)
            {
                manager.ParseScript(args[0]);
            }
            else
            {
                Console.WriteLine("Invalid argument exception");
                return;
            }
            
            // todo: person, product aliases, to be able handy manage consumes details.
            // todo: script parsing?
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

            foreach (var p in manager.Participants)
            {
                p.Consume(manager.Products);
            }

            manager.Participants.ForEach(x => Console.WriteLine($"{x.Name} {x.GetCalcDeb()}"));

            Console.WriteLine(); Console.WriteLine();
            manager.GetDebsDetails().ForEach(x => Console.WriteLine(x.ToString()));

            Console.ReadKey();
        }
    }
}
