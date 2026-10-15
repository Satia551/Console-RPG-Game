using System;

namespace TextGame;

internal abstract class Consumable
{
    public string Name { get; set; }
    public int Amount { get; set; }

    public Consumable(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }

    public abstract void Use(Hero target);
}

internal class HealthPotion : Consumable
{
    private int _healAmount;

    public HealthPotion(int amount, int healAmount) : base("Health Potion", 3)
    {
        _healAmount = healAmount;
    }


    public override void Use(Hero target)
    {
            int hpBeforeHeal = target.Hp;
            target.Hp = Math.Min(target.maxHp, target.Hp + _healAmount);
            Amount--;
            int realHealAmount = target.Hp - hpBeforeHeal;
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[ITEM] You healed yourself for {realHealAmount} by using {Name}!");
            Console.ResetColor();
        
        
    }
}
