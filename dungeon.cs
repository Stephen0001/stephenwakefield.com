//THIS IS A C# APPLICATION AND WILL ONLY WORK IN VISUAL STUDIO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonLibrary; //added
using DungeonMonsters; //added

namespace DungeonApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "The Dungeon";
            Console.WriteLine("Welcome to the Dungeon!");

            //Using the Weapon class to use an argument for the Player class
            //public Weapon (maxDamage, minDamage, name, bonusHitChance, isTwoHanded)
            Weapon sword = new Weapon(6, 1, "Sword", 5, true);

            //public Player(name, maxLife, hitChance, WEAPON EQIPPEDWEAPON, block, characterRace, life)
            Player player = new Player("Lerooooy Jenkins", 20, 30, sword, 2, Race.Drawf, 15);

            bool exit = false;

            do
            {
                //a random room is generated
                Console.WriteLine(GetRoom());

                //Rabbit(maxDamage, maxLife, description, name, hitChance, block, minDamage, life, isFluffy)
                Rabbit r1 = new Rabbit(6, 15, "He looks harmless","Easy","Baby Rabbit", 20, 1, 1, 15, false);
                Rabbit r2 = new Rabbit(7, 20, "He's as cold as ice", "Medium", "Blue Rabbit", 22, 2, 3, 20, false);
                Rabbit r3 = new Rabbit(10, 20, "His Eyes are firey red", "Hard", "Red Rabbit", 30, 3, 8, 20, true);
                Frankenstein f1 = new Frankenstein(25, 35, "Undead Monster!", "Very Hard" ,"Frankenstein (FINAL BOSS)", 35, 4, 10, 35);

                Monster[] monsters =
                {
                    //increase chances of r1 to make it easier for the player
                    r1, r1, r1, r1, r1, r1, r1, r1, r1, r2, r3, f1
                };

                Random rand = new Random();
                int randomNbr = rand.Next(monsters.Length);
                //monster contains a random monster   i.e. r2
                Monster monster = monsters[randomNbr];
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\nMonster in this room: \n{monster.Name}");
                Console.ResetColor();

                //CONDITION TO LOOP OR BREAK LOOP
                bool reload = false;

                //do while loop
                do
                {
                    Console.Write("\nPlease choose and action:\n" + 
                        "A) Attack\n" +
                        "R) Run Away\n" +
                        "P) Player info\n" +
                        "M) Monster info\n" +
                        "E) Exit\n" +
                        "Choose your fate");

                    //get variable for keypress
                    ConsoleKey userChoice = Console.ReadKey().Key;

                    Console.Clear();

                    switch (userChoice)
                    {
                        case ConsoleKey.A:
                            //Attack Method
                            //this is looped until someone dies
                            Combat.DoBattle(player, monster);

                            //if you defeat Frankenstein (FINAL BOSS)
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
                            //if any other monster dies
                            else if (monster.Life <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("\nYou defeated {0}\n",
                                    monster.Name);
                                Console.ResetColor();

                                //regenerate the players health bar and add color
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{player.Name}, your health has increased and been restored\n");
                                Console.ResetColor();
                                player.MaxLife += 2;
                                player.Life = player.MaxLife;

                                //player gets upgrades
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

                                reload = true; //exit only the first loop to see if player died?!
                            }//end else if
                            break;
                        case ConsoleKey.R:
                            Console.WriteLine("Run Away!");
                            //monster (the attacker) has a chance to hit the player (the defender)
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
            //return rooms[new Random().Next(rooms.Length)];  //SHORTCUT TO CODE ABOVE
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
    //"Abstract" will never be used other than to help create other classes, properties, and methods (incomplete implementation)
    //Can't create an instance of an abstract class
    //Use the abstract modifier in a class declaration to indicate that the class is inteded only to be a base (parent) for other classes
    //PARENT CLASS
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


        //constructors
        //Since we don't inherit constructors from the parent AND this class is ABSTRACT, we are not going
        //to create any CTORS here, we still get the free default constructor, but cannot use it b/c we
        //can not create an instance of a character


        //methods
        public virtual int CalcBlock()
        {
            return Block;
        }//end CalcBlock()

        //MINI-LAB: 
        //make a method called CalcHitChance() that returns the HitChance
        //make it overridable for the future
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
        //"Attacker" and "Defender" alternate b/t "player" and "monster"
        public static void DoAttackMethod(Character attacker, Character defender)
        {
            Random rand = new Random();
            int diceRoll = rand.Next(1,66);
            //need a sleep b/c Next() will register the same rand number b/c it runs by timespan
            System.Threading.Thread.Sleep(30);

            //i.e. if diceRoll  is less than:  (35 (hitChance + weapon bonusHitChance) - 2 (block))
            if (diceRoll < (attacker.CalcHitChance() - defender.CalcBlock()))
            {
                int damageDealt = attacker.CalcDamage();
                defender.Life -= damageDealt;
                Console.ForegroundColor = ConsoleColor.Red; //if attacker hits (player or monster)
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
            //player is the attacker, monster is the defender
            DoAttackMethod(player, monster);

            //as long as the monster is still alive
            if (monster.Life > 0)
            {
                //monster is the attacker, player is the defender
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
    //Monster is Child Of the Character class
    public class Monster : Character
    {
        //properties
        private int _minDamage; //field  //business rule!

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
            }
        }//end MinDamage


        //constructors
        public Monster() { }//end default CTOR

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
            // return a number b/t 1 and 6
            return new Random().Next(MinDamage, MaxDamage + 1);
        }
    }//end class
}//end namespace


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    //New classes default to an INTERNAL access modifier, so you make them public, if you intend to 
    //use them outside a project where they are created
    //Player is a child of Character
    public class Player : Character
    {
        //fields
        //private int _life;

        //properties
        //(automatic properties) - prop tab tab
        //public string Name { get; set; }
        //public int MaxLife { get; set; }
        //public int HitChance { get; set; }
        public Weapon EquippedWeapon { get; set; }
        //public int Block { get; set; }
        //don't use enum here... There is a Race enum
        public Race CharacterRace { get; set; }
        //Traditional property with Business Rule (long way)
        //public int Life
        //{
        //    get { return _life; }
        //    set
        //    {
        //        if (value <= MaxLife)
        //        {
        //            _life = value;
        //        }//end if
        //        else
        //        {
        //            _life = MaxLife;
        //        }//end else
        //    }//end set
        //}//end Life


        //constructors
        //only make an FQCTOR... We never want a blank Player
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

            //CharacterRace is an enum
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
            //HitChance + BonusHitChance from weapon
            //i.e. return 30 + 5 = 35
            return base.CalcHitChance() + EquippedWeapon.BonusHitChance;
        }//end CalcHitChance()

        public override int CalcDamage()
        {
            Random rand = new Random();
            //generate a random number b/t:  1 and 6
            int damage = rand.Next(EquippedWeapon.MinDamage, EquippedWeapon.MaxDamage + 1);
            //i.e. return 5
            return damage;
        }//end CalcDamage()

        //ALL THE REST OF THE METHODS ARE INHERITED FROM THE CHARACTER PARENT CLASS

    }//end Player
}//end namespace        


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    //You can not add a ENUM through V.S. interface
    //INSTEAD... you can to create public class with the enum keyword (instead of class)
    public enum Race
    {
        //Single values, comma separated, no spaces
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
    //The default access modifier fro a class is INTERNAL (it is only accessible inside the project where it was created)
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

        //Applying a Business Rule!
        public int MinDamage
        {
            get { return _minDamage; }
            set
            {
                //_minDamage can not be greater than MaxDamage
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

        //REMEMBER: MaxDamage has a DEPENDENCY on MinDamage
        public Weapon (int maxDamage, int minDamage, string name, int bonusHitChance, bool isTwoHanded)
        {
            //<Property> = <args>
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
        }

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

        //properties (inherited)
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

            //methods (using inheritence with overrides)
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
