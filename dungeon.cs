//C# CONSOLE APPLICATION (Visual Studio)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary; //added (class library)
using DungeonMonsters; //added (class library)

namespace DungeonApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "The Dungeon";
            Console.WriteLine("Welcome to the Dungeon!");
            Weapon sword = new Weapon(6, 1, "Sword", 5, true);
            Player player = new Player("Lerooooy Jenkins", 20, 30, sword, 2, Race.Drawf, 15);
            bool exit = false;

            do
            {
                Console.WriteLine(GetRoom());

                Rabbit r1 = new Rabbit(6, 15, "He looks harmless","Easy","Baby Rabbit", 20, 1, 1, 15, false);
                Rabbit r2 = new Rabbit(7, 20, "He's as cold as ice", "Medium", "Blue Rabbit", 22, 2, 3, 20, false);
                Rabbit r3 = new Rabbit(10, 20, "His Eyes are firey red", "Hard", "Red Rabbit", 30, 3, 8, 20, true);
                Frankenstein f1 = new Frankenstein(25, 35, "Undead Monster!", "Very Hard" ,"Frankenstein (FINAL BOSS)", 35, 4, 10, 35);

                Monster[] monsters =
                {
                    r1, r1, r1, r1, r1, r1, r1, r1, r1, r2, r3, f1
                };

                Random rand = new Random();
                int randomNbr = rand.Next(monsters.Length);
                Monster monster = monsters[randomNbr];
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\nMonster in this room: \n{monster.Name}");
                Console.ResetColor();

                bool reload = false;

                do
                {
                    Console.Write("\nPlease choose and action:\n" + 
                        "A) Attack\n" +
                        "R) Run Away\n" +
                        "P) Player info\n" +
                        "M) Monster info\n" +
                        "E) Exit\n" +
                        "Choose your fate");

                    ConsoleKey userChoice = Console.ReadKey().Key;

                    Console.Clear();

                    switch (userChoice)
                    {
                        case ConsoleKey.A:
                            Combat.DoBattle(player, monster);
                            
                            if (monster.Name == "Frankenstein (FINAL BOSS)" && monster.Life <= 0)
                            {
                                #region ASCII ART
                                Console.WriteLine(@"
                                      /|
                                     |\|
                                     |||
                                     |||
                                     |||
                                     |||
                                     |||
                                     |||
                                  ~-[{o}]-~
                                     |/|
              ___                    |/|
             ///~`     |\\_          `0'         =\\\\         
            ,  |='  ,))\_| ~-_                    _)  \     _ __
           / ,' ,;((((((    ~ \                  `~~~\-~-_ /~ (_/\
         /' -~/~)))))))'\_   _/'                      \_  /'  D   |
        (       (((((( ~-/ ~-/                          ~-;  /    \--_
         ~~--|   ))''    ')  `                            `~~\_    \   )
             :        (_  ~\           ,                    /~~-     ./
              \        \_   )--__  /(_/)                   |    )    )|
    ___       |_     \__/~-__    ~~   ,'      /,_;,   __--(   _/      |
  //~~\`\    /' ~~~----|     ~~~~~~~~'        \-  ((~~    __-~        |
((()   `\`\_(_     _-~~-\                      ``~~ ~~~~~~   \_      /
                                     -Tua Xiong

");

                                #endregion
                                Console.WriteLine("CONGRATS!!! YOU WON THE GAME!!!");
                                reload = true;
                                exit = true;
                            }//end if
                            else if (monster.Life <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\nYou defeated {0}\n",
                                    monster.Name);
                                Console.ResetColor();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{player.Name}, your health has increased and been restored\n");
                                Console.ResetColor();
                                player.MaxLife += 2;
                                player.Life = player.MaxLife;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{player.Name}, your {player.EquippedWeapon.Name} hit chance increased by 2\n");
                                if (player.EquippedWeapon.MaxDamage == 10)
                                {
                                    player.EquippedWeapon.Name = "Marbled Sword";
                                    Console.WriteLine($"{monster.Name} has yielded the {player.EquippedWeapon.Name}!");
                                }
                                Console.WriteLine($"Your {player.EquippedWeapon.Name} Possible Max Damage has increased by 1\n");
                                Console.ResetColor();
                                player.EquippedWeapon.MaxDamage += 1;
                                player.EquippedWeapon.MinDamage += 1;
                                player.EquippedWeapon.BonusHitChance += 2;

                                reload = true;
                            }//end else if
                            break;
                        case ConsoleKey.R:
                            Console.WriteLine("Run Away!");
                            Combat.DoAttackMethod(monster, player);
                            reload = true; //exit only the first loop
                            break;
                        case ConsoleKey.P:
                            Console.WriteLine("Player Info");
                            Console.WriteLine(player);
                            break;
                        case ConsoleKey.M:
                            Console.WriteLine("Monster Info");
                            Console.WriteLine(monster);
                            break;
                        case ConsoleKey.E:
                            Console.WriteLine("E");
                            Console.WriteLine("X");
                            Console.WriteLine("You fleed the dungeon");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("That was not a valid choice");
                            break;
                    }//end switch on userChoice
                    if (player.Life <= 0)
                    {
                        Console.WriteLine("You Died!");
                        exit = true; //exit the OUTSIDE loop
                    }

                } while (!exit && !reload); //end do while menu

            } while (!exit); //end do while reload

        }//end Main()

        private static string GetRoom()
        {
            string[] rooms = {
                "This small room is lined with benchlike seats on all the walls",
                "A wall that holds a seat with a hole in it is in this chamber. It's a privy!",
                "Many doors fill the room ahead. Doors of varied shape, size, and design are set in every wall and even the ceiling and floor.",
                "Unlike the flagstone common throughout the dungeon, this room is walled and floored with black marble veined with white.",
                "This tiny room holds a curious array of machinery. Winches and levers project from every wall, and chains with handles dangle from the ceiling.",
                "As the door opens, it scrapes up frost from a floor covered in ice. The room before you looks like an ice cave.",
                "A flurry of bats suddenly flaps through the doorway, their screeching barely audible as they careen past your heads.",
                "This otherwise bare room has one distinguishing feature. The stone around one of the other doors has been pulled over its edges."
            };

            Random rand = new Random();
            int roomIndex = rand.Next(rooms.Length);
            string room = rooms[roomIndex];
            return room;
        }//end GetRoom()
    }//end class
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public abstract class Character
    {
        //fields
        private int _life; //business rule

        //properties
        public string Name { get; set; }
        public int MaxLife { get; set; }
        public int HitChance { get; set; }
        public int Block { get; set; }
        public int Life
        {
            get { return _life; }
            set
            {
                if (value <= MaxLife)
                {
                    _life = value;
                }//end if
                else
                {
                    _life = MaxLife;
                }//end else
            }//end set
        }//end Life

        //methods
        public virtual int CalcBlock()
        {
            return Block;
        }//end CalcBlock()
        
        public virtual int CalcHitChance()
        {
            return HitChance;
        }//end CalcHitChance()

        public virtual int CalcDamage()
        {
            return 0;
        }//end CalcDamage()
    }//end class
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    //METHODS
    public class Combat
    {
        public static void DoAttackMethod(Character attacker, Character defender)
        {
            Random rand = new Random();
            int diceRoll = rand.Next(1,66);
            System.Threading.Thread.Sleep(30);

            if (diceRoll < (attacker.CalcHitChance() - defender.CalcBlock()))
            {
                int damageDealt = attacker.CalcDamage();
                defender.Life -= damageDealt;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} hit {1} for {2} damage!",
                    attacker.Name,
                    defender.Name,
                    damageDealt);
                Console.ResetColor(); 
            }//end if
            else
            {
                Console.WriteLine($"{attacker.Name} missed!\n");
            }//end else
        } //end DoAttackMethod()

        public static void DoBattle(Player player, Monster monster)
        {
            DoAttackMethod(player, monster);
            if (monster.Life > 0)
            {
                DoAttackMethod(monster, player);
            }//end if
        }//end DoBattle()
    }//end Combat class
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Monster : Character
    {
        //field
        private int _minDamage;

        //properties
        public int MaxDamage { get; set; }
        public string Description { get; set; }
        public string EnemyClass { get; set; }
        public int MinDamage
        {
            get { return _minDamage; }
            set
            {
                if (value > 0 && value <= MaxDamage)
                {
                    _minDamage = value;
                }
                else
                {
                    _minDamage = 1;
                }
            }//end set
        }//end MinDamage


        //constructors
        public Monster() { }

        public Monster(int maxDamage, int maxLife, string description, string enemyClass, string name, int hitChance, int block, int minDamage, int life)
        {
            MaxDamage = maxDamage; //Business Rule!!!
            MaxLife = maxLife; //Business Rule!!!
            Description = description;
            EnemyClass = enemyClass;
            Name = name;
            HitChance = hitChance;
            Block = block;
            MinDamage = minDamage; 
            Life = life;
        }//end FQCTOR

        //methods
        public override string ToString()
        {
            return string.Format("\n{0}\nLife: {1} of {2}\nDamage: {3} to {4}\nBlock: {5}\nDescription:\n{6}\nDifficulty: {7}\n",
                Name,
                Life,
                MaxLife,
                MinDamage,
                MaxDamage,
                Block,
                Description,
                EnemyClass);
        }//end ToString()

        public override int CalcDamage()
        {
            return new Random().Next(MinDamage, MaxDamage + 1);
        }//end CalcDamage()
    }//end class
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Player : Character
    {
        //fields
        public Weapon EquippedWeapon { get; set; }
        public Race CharacterRace { get; set; }
        
        //constructors
        public Player(string name, int maxLife, int hitChance, Weapon eqippedWeapon, int block, Race characterRace, int life)
        {
            Name = name;
            MaxLife = maxLife;
            HitChance = hitChance;
            EquippedWeapon = eqippedWeapon;
            Block = block;
            CharacterRace = characterRace;
            Life = life;
        }

        //methods
        public override string ToString()
        {
            string description = "";
            
            switch (CharacterRace)
            {
                case Race.BloodElf:
                    description = "You are a Blood Elf";
                    break;
                case Race.Human:
                    description = "You are a Human";
                    break;
                case Race.Khajit:
                    description = "You are a Khajit";
                    break;
                case Race.Halfling:
                    description = "You are a Halfling";
                    break;
                case Race.Drawf:
                    description = "You are a Drawf";
                    break;
                case Race.Gnome:
                    description = "You are a Gnome";
                    break;
            }//end switch

            return string.Format("\nName: {0} \nLife: {1} of {2}\nHit Chance: {3}%\nWeapon:\n{4}\nDescription: {5}",
                Name,
                Life,
                MaxLife,
                HitChance,
                EquippedWeapon,
                description);
        }//end ToString()

        public override int CalcHitChance()
        {
            return base.CalcHitChance() + EquippedWeapon.BonusHitChance;
        }//end CalcHitChance()

        public override int CalcDamage()
        {
            Random rand = new Random();
            int damage = rand.Next(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage + 1);
            return damage;
        }//end CalcDamage()
    }//end Player
}//end namespace        


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public enum Race
    {
        BloodElf,
        Human,
        Khajit,
        Halfling,
        Drawf,
        Gnome
    }//end race
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Weapon
    {
        //fields
        private string _name;
        private int _minDamage; //Has Business Rules
        private int _maxDamage;
        private bool _isTwoHanded;
        private int _bonusHitChance;

        //properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }//end Name
        
        public bool IsTwoHanded
        {
            get { return _isTwoHanded; }
            set { _isTwoHanded = value; }
        }//end IsTwoHanded

        public int BonusHitChance
        {
            get { return _bonusHitChance; }
            set { _bonusHitChance = value; }
        }//end BonusHitChance

        public int MaxDamage
        {
            get { return _maxDamage; }
            set { _maxDamage = value; }
        }//end MaxDamage

        public int MinDamage
        {
            get { return _minDamage; }
            set
            {
                if (value > 0 && value <= MaxDamage )
                {
                    _minDamage = value;
                }
                else
                {
                    _minDamage = 1;
                }
            }
        }//end MinDamage

        //constructors
        public Weapon() { }

        public Weapon (int maxDamage, int minDamage, string name, int bonusHitChance, bool isTwoHanded)
        {
            MaxDamage = maxDamage;
            MinDamage = minDamage;
            Name = name;
            BonusHitChance = bonusHitChance;
            IsTwoHanded = isTwoHanded;
        }//end FQCTOR

        //methods
        public override string ToString()
        {
            return string.Format("{0}:\t{1} to {2} damage\nBonus Hit: {3}%\t{4}",
                Name,
                MinDamage,
                MaxDamage,
                BonusHitChance,
                IsTwoHanded ? "Two-Handed: Yes" : "Two-Handed: No");
        }//end ToString()
    }//end weapon class
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary;

namespace DungeonMonsters
{
    public class Frankenstein : Monster
    {
        //fields (inherited)

        //properties (inherited)

        //constructors
        public Frankenstein() { }

        public Frankenstein(int maxDamage, int maxLife, string description, string enemyClass, string name, int hitChance, int block, int minDamage, int life)
        {
            MaxDamage = maxDamage; //Business Rule!!!
            MaxLife = maxLife; //Business Rule!!!
            Description = description;
            EnemyClass = enemyClass;
            Name = name;
            HitChance = hitChance;
            Block = block;
            MinDamage = minDamage;
            Life = life;
        }

        //methods (using inheritence with overrides)
        public override string ToString()
        {
            return base.ToString();
        }//end ToString()
    }//end class
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary;

namespace DungeonMonsters
{
    public class Rabbit : Monster
    {
        //fields(inherted)

        //properties
        public bool IsFluffy { get; set; }

        //constructors
        public Rabbit() { }

        public Rabbit(int maxDamage, int maxLife, string description, string enemyClass, string name, int hitChance, int block, int minDamage, int life, bool isFluffy)
        {
            MaxDamage = maxDamage; //Business Rule!!!
            MaxLife = maxLife; //Business Rule!!!
            Description = description;
            Name = name;
            HitChance = hitChance;
            Block = block;
            MinDamage = minDamage;
            Life = life;
            EnemyClass = enemyClass;
            IsFluffy = isFluffy; //not inherited
        }

        //methods
        public override string ToString()
        {
            return base.ToString() + ((IsFluffy) ? "It's quite fluffy" : "Not so fluffy");
        }

        public override int CalcBlock()
        {
            int calculatedBlock = Block;
            if (IsFluffy)
            {
                calculatedBlock += (calculatedBlock / 2);
                
            }
            return calculatedBlock;
        } //end CalcDamage
    }//end class
}//end namespace
