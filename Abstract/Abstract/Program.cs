using System;


namespace Abstract
{
    public class Program
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

       


            public Program(string name, int health, int attack, int defense)
            {
                Name = name;
                Health = health;
                Attack = attack;
                Defense = defense;
            }

        public virtual void AttackTarget(Program target)
        {
            int damage = Attack - target.Defense;
            if (damage > 0)
            {
                target.Health -= damage;
                Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{Name}'s attack failed against {target.Name}!");
            }
        }

        public virtual void Defend()
        {
            Console.WriteLine($"{Name} is defending.");


        }
    

    }
}

        // Main method as the entry point of the program
