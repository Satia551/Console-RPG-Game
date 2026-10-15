using System;

namespace TextGame;

internal class Hero
{
    public event CombatEventHandler OnAttack;
    public event CombatEventHandler OnCriticalHit;
    public Inventory Backpack { get; set; } = new Inventory();
    public string Name { get; set ; }
    public int Hp { get; set; } = 150;
    public int maxHp { get; set; } = 150;
    public int Mp { get; set; }
    public int MaxMp { get; set; }
    public string HeroClass { get; set; }
    public int heroAgility = 20;
    public virtual void Attack(Dragon dragon)
    {
    }

    public Hero(string chosenname, string chosenClass)
    {
        Name = chosenname;
        HeroClass = chosenClass.ToLower();
        OnCriticalHit += HealedByCritical;
    }

    private void HealedByCritical(string heroName, string skillName, int damage)
    {
        int hpBeforeHeal = Hp;
        Hp += 10;
        if (Hp > maxHp)
        {
            Hp = maxHp;
        }

        int realHeal = Hp - hpBeforeHeal;
        if (realHeal > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"!! CRITICAL HEAL !!! Passive ability triggered +{realHeal}HP! Current HP: {Hp}");
            Console.ResetColor();
        }
    }

    public virtual void DisplayStatus()
    {
        string green = "\x1b[38;2;46;204;113m";
        ScreenEffects.ResourceBar("Health", Hp, maxHp, green);
        // string blue = "\x1b[38;2;52;152;219m";  
        // string orange = "\x1b[38;2;255;128;0m";
        // ScreenEffects.ResourceBar("Health", Hp, maxHp, green);
        // if (HeroClass == "mage")
        // {
        //    ScreenEffects.ResourceBar("Mana", Mp, MaxMp, blue);
        // }
        // else if (HeroClass == "warrior")
        // {
        //     if (this is Warrior warrior)
        //     {
        //         ScreenEffects.ResourceBar("Rage", warrior.Rage, warrior.MaxRage, orange);
        //         
        //     }
        // }

    }
    public void TriggerAttack(string skillName, int damage)
    {
        OnAttack?.Invoke(Name, skillName, damage);
    }
    
    public void TriggerCritical(string skillName, int damage)
    {
        OnCriticalHit?.Invoke(Name, skillName, damage);
    }
}