using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jckHW1
{
    class Program
    {
        class Enemy
        {
            protected int health;
            protected string speak;
            public virtual void func()
            {
                speak = "You are going down.";
                health = 10;
            }
            public int damage(int d)
            {
                health -= d;
                return health;
            }
            public int Attack()
            {
                Random rng = new Random();
                return rng.Next(10);
            }
            public int rhealth()
            {
                return health;
            }
        }
        class Enemy1 : Enemy
        {
            private string name = "Auric Goldfinger";

            public override void func()
            {
                base.speak = "Mr Bond, I expect you to die.";
                base.health = 10;
            }
            public string call()
            {
                return speak;
            }
            public string rname()
            {
                return name;
            }
        }
        class Enemy2 : Enemy
        {
            private string name = "Xenia Onatopp";

            public override void func()
            {
                base.speak = "This time Mr. Bond the pleasure will be all mine!";
                base.health = 10;
            }
            public string call()
            {
                return speak;
            }
            public string rname()
            {
                return name;
            }
        }
        class Player
        {
            private int health;
            private string speak;
            private string name = "James Bond";
            
            public virtual void func()
            {
                speak = "The things I do for England.";
                health = 10;
            }
            public int damage(int d)
            {
                health -= d;
                return health;
            }
            public int Attack()
            {
                Random rng = new Random();
                return rng.Next(10);
            }
            public int rhealth()
            {
                return health;
            }
            public string rname()
            {
                return name;
            }
            public string call()
            {
                return speak;
            }
        }
        static void Main(string[] args)
        {
            Enemy1 e1 = new Enemy1();
            Enemy2 e2 = new Enemy2();
            e1.func();
            e2.func();
            Player p = new Player();
            p.func();
            Random rng = new Random();

            int phealth, e1health, e2health, attack;

            phealth = p.rhealth();
            e1health = e1.rhealth();
            e2health = e2.rhealth();

            while (phealth >= 0 && e1health >= 0)
            {
                Console.WriteLine(p.call());
                attack = rng.Next(10);
                e1health = e1.damage(attack);
                Console.WriteLine("{0} attacks {1} for {2} damage. {3} health remaining.", p.rname(), e1.rname(), attack, e1health);
                Console.WriteLine();
                if (e1health > 0)
                {
                    Console.WriteLine(e1.call());
                    attack = rng.Next(10);
                    phealth = p.damage(attack);
                    Console.WriteLine("{0} attacks {1} for {2} damage. {3} health remaining.", e1.rname(), p.rname(), attack, phealth);
                    Console.WriteLine();
                }
                if (phealth <= 0)
                {
                    Console.WriteLine("{0} Wins!", e1.rname());
                    Console.WriteLine();
                }
                if (e1health <= 0)
                {
                    Console.WriteLine("{0} Wins!", p.rname());
                    Console.WriteLine();
                }
            }
            while (phealth >= 0 && e2health >= 0)
            {
                Console.WriteLine(p.call());
                attack = rng.Next(10);
                e2health = e2.damage(attack);
                Console.WriteLine("{0} attacks {1} for {2} damage. {3} health remaining.", p.rname(), e2.rname(), attack, e2health);
                Console.WriteLine();
                if (e2health > 0)
                {
                    Console.WriteLine(e2.call());
                    attack = rng.Next(10);
                    phealth = p.damage(attack);
                    Console.WriteLine("{0} attacks {1} for {2} damage. {3} health remaining.", e2.rname(), p.rname(), attack, phealth);
                    Console.WriteLine();
                }
                if (phealth <= 0)
                {
                    Console.WriteLine("{0} Wins!", e2.rname());
                    Console.WriteLine();
                }
                if (e2health <= 0)
                {
                    Console.WriteLine("{0} Wins!", p.rname());
                    Console.WriteLine();
                }
            }
        }
    }
}
