using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indexers
{
    class Program
        {
            static void Main(string[] args)
            {
                DebitCard card = new DebitCard();

                Console.WriteLine("Enter the card pin: ");
                string inputPin = Console.ReadLine();
      
                if (card.Pin == inputPin)
                {
                    Console.WriteLine("Correct PIN");
                }
                Console.ReadKey();
            }
        }
    }



