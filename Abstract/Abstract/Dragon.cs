using System;


namespace Abstract
{

    public class Dragon : Program
    {
        
            public Dragon(string name, int health, int attack, int defense) : base(name, health, attack, defense) { }

            public void BreatheFire(Program target)
            {
                Attack *= 2;
                Console.WriteLine($"{Name} breathes fire at {target.Name}!");
            }

            public override void AttackTarget(Program target)
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
                Attack /= 2; // Resetting the attack back to normal after using BreatheFire
            }
        }

    }

