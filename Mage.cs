using System;
using System.Globalization;

namespace TextGame;

internal class Mage : Hero
{

    private int UltimateCooldown;

    public Mage(string chosenName) : base(chosenName, "mage")
    {
        Mp = 100;
        MaxMp = 100;
    }

    public override void DisplayStatus()
    {
        base.DisplayStatus();
        string blue = "\x1b[38;2;52;152;219m";
        ScreenEffects.ResourceBar("Mana", Mp, MaxMp, blue);
        
    }
    public override void Attack(Dragon dragon)
    {
        if (UltimateCooldown > 0)
        {
            UltimateCooldown--;
        }
        
        string skillName = "";
        int damage = 0;
        bool isActionValid = false;
        Random random = new Random();
        Console.WriteLine("********************************");
        Console.WriteLine($"Your turn, {Name}!");
        DisplayStatus();
        dragon.DisplayStatus();

        while (!isActionValid)
        {
            Console.WriteLine("Select your action: ");
            Console.WriteLine("1. Spark Attack (10-30 dmg) [Generates +20 MP]");
            Console.WriteLine($"2. Frost Bolt (40-60 dmg) [Cost: 30 MP]");
            Console.WriteLine(UltimateCooldown ==0
                ? "3. Pyroblast (80-120 dmg) [Cost 60 MP] [READY]"
                : $"3. Pyroblast (80-120 dmg) [Cost 60 MP] [COOLDOWN: {UltimateCooldown}]");
            Console.WriteLine($"4. Open Backpack");

            Console.WriteLine("*************************************************");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                skillName = "Spark Attack";
                damage = random.Next(10,31);
                Mp = Math.Min(MaxMp, Mp + 20);
                Console.WriteLine($"[MP CHARGE] Your spark generated mana! (+20 MP). Current MP: {Mp}");
                isActionValid = true;
            }
            
            else if (choice == "2")
            {
                if (Mp >= 30)
                {
                    skillName = "Frost Bolt";
                    damage = random.Next(40, 61);
                    Mp -= 30;
                    Console.WriteLine($"You casted Frost Bolt! (-30 MP). Current MP: {Mp}");
                    isActionValid = true;
                }
                else
                {
                    Console.WriteLine("You dont have enough mana! Choose another action.");
                }
            }
            else if (choice == "3")
            {
                if (UltimateCooldown == 0 && Mp >= 60)
                {
                    skillName = "Pyroblast";
                    damage = random.Next(80,121);
                    Mp -= 60;
                    UltimateCooldown = 4;
                    Console.WriteLine($"You casted Pyroblast! (-60MP). Current MP: {Mp}");
                    isActionValid = true;
                    
                }
                else if (Mp < 60)
                {
                    Console.WriteLine($"You need more mana! Current MP: {Mp}. Select another spell.");
                }
                else if (UltimateCooldown > 0)
                {
                    Console.WriteLine($"Pyroblast is on cooldown! Current cooldown: {UltimateCooldown}. Select another spell.");
                }
            } else if (choice == "4")
            {
                bool inBackpack = true;
                while (inBackpack)
                {
                    int hpPotions = Backpack.CountHealthPotions();
                    Console.WriteLine("Select item, which you want to use: ");
                    Console.WriteLine($"1. Health Potion [{hpPotions} left]");
                    Console.WriteLine("2. Back to attack");
                    choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        Backpack.UseItem(choice, this);
                        return;
                    }else if (choice == "2")
                    {
                        inBackpack = false;
                    }
                }

            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.");
            }

            if (isActionValid)
            {
                if (string.IsNullOrEmpty(skillName))
                {
                    break;
                }
                int criticalChance = random.Next(1, 101);
                if (criticalChance <= 20)
                {
                    damage *= 2;
                    skillName = "CRITICAL " + skillName;
                    TriggerCritical(skillName, damage);
                }
                TriggerAttack(skillName, damage);
                break;
            }

        }
        
    }

}