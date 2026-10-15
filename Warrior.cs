using System;

namespace TextGame;

internal class Warrior : Hero
{
    public int Rage { get; set; } = 0;
    public int MaxRage { get; set; } = 100;
    private int ShieldSlamCooldown = 0;
    private string skillName = "";
    public Warrior(string chosenName) : base(chosenName, "warrior") { }

    public override void DisplayStatus()
    {
        base.DisplayStatus();
        string orange = "\x1b[38;2;255;128;0m";
        ScreenEffects.ResourceBar("Rage", Rage, MaxRage, orange);
    }

    public override void Attack(Dragon dragon)
    {
        if (ShieldSlamCooldown > 0)
        {
            ShieldSlamCooldown--;
        }
        
        int damage = 0;
        bool isActionValid = false;
        Random random = new Random();
        Console.WriteLine("********************************");
        Console.WriteLine($"Your turn, {Name}!");
        DisplayStatus();
        dragon.DisplayStatus();
        Console.WriteLine("Select your action:");
        while (isActionValid == false)
        {
            Console.WriteLine($"1. Heavy Strike (25-45) [Generates +25 Rage]");
            Console.WriteLine(ShieldSlamCooldown ==0
                ? "2. Shield Slam (10-20) [STUN] [Cost: 20 Rage] [READY]"
                : $"2. Shield Slam (10-20) [STUN] [Cost: 20 Rage] [COOLDOWN: {ShieldSlamCooldown}]");
            Console.WriteLine($"3. Berserker Execute (70-110) [Cost: 40 Rage]");
            Console.WriteLine($"4. Open Backpack");
            Console.WriteLine("*************************************************");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                int rageGenerate = 25;
                int rageBeforeGenerate = Rage;
                skillName = "Heavy Strike";
                damage = random.Next(25, 46);
                Rage = Math.Min(MaxRage, Rage + rageGenerate);
                int realRageAmount = Rage - rageBeforeGenerate;

                if (realRageAmount > 0)
                {
                    Console.WriteLine($"You used {skillName} and generated +{realRageAmount} Rage!");
                }
                else
                {
                    Console.WriteLine($"You used {skillName}, but you can't have more than {MaxRage} Rage! No Rage was generated.");
                }
                isActionValid = true;

            }else if (choice == "2")
            {

                if (Rage >=20 && ShieldSlamCooldown == 0)
                {
                    skillName = "Shield Slam";
                    damage = random.Next(10, 21);
                    Rage -= 20;
                    Console.WriteLine($"You used {skillName} for 20 Rage! Dragon is stunned for one round.");
                    ShieldSlamCooldown = 3;
                    dragon.IsStunned = true;
                    isActionValid = true;
                }
                else if (Rage <20)
                {
                    Console.WriteLine("You don't have enough Rage!");
                }
                else
                {
                    Console.WriteLine($"Shield Slam is on Cooldown! You have to wait {ShieldSlamCooldown} rounds more!");
                }
            }else if (choice == "3")
            {
                if (Rage >= 40)
                {
                    skillName = "Berserker Execute";
                    damage = random.Next(70, 111);
                    Rage -= 40;
                    Console.WriteLine($"You used {skillName} for 40 Rage!");
                    isActionValid = true;
                }
                else
                {
                    Console.WriteLine("You don't have enough Rage!");
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
            
            if (isActionValid)
            {
                int criticalChance = random.Next(1, 101);
                if (criticalChance <= 20)
                {
                    damage *= 2;
                    skillName = "CRITICAL " + skillName;
                    TriggerCritical(skillName, damage);
                }
                TriggerAttack(skillName, damage);
                break;
            }}
    }
}