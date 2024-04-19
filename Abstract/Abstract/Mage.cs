using System;


namespace Abstract
{
    public class Mage : Program
    {
        public Mage(string name, int health, int attack, int defense) : base(name, health, attack, defense) { }

        public void CastSpell(Program target)
        {
            Console.WriteLine($"{Name} casts a spell at {target.Name}!");
        }

        public override void Defend()
        {
            Console.WriteLine($"{Name} seals off further attempts to defend!");
        }
    }
}


