using System;

namespace Abstract
{
    public class Warrior : Program
    { 
     
            public Warrior(string name, int health, int attack, int defense) : base(name, health, attack, defense) { }

            public void Charge(Program target)
            {
                Console.WriteLine($"{Name} charges at {target.Name}!");
            }

            public override void Defend()
            {
                Defense += 5;
                Console.WriteLine($"{Name} is defending with increased defense!");
            }
        }
    }


    
