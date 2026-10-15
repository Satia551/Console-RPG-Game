using System;

namespace TextGame;

class Dragon
{
    public int Hp { get; set; } = 500;
    public int MaxHp { get; set; } = 500;
    private bool _isEnraged = false;
    public bool IsStunned { get; set; } = false;
    public void TakeDamage(string heroName, string skillName, int damage)
    {
        Hp -= damage;
        Console.WriteLine($"The dragon roars in pain! {heroName} deal {damage} damage from {skillName} ability.");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        if (Hp < 0){ Hp = 0;}
        Console.WriteLine($"Dragon HP: {Hp}");
        Console.ResetColor();
    }

    public void CounterAttack(Hero hero)
    {
        Random random = new Random();
        int attackChoice = random.Next(1, 5);
        int damage = 0;
        string attackName = "";
        if (attackChoice == 1)
        {
            attackName = "Bite";
            damage = random.Next(5, 16);
        }else if (attackChoice == 2)
        {
            attackName = "Claw";
            damage = random.Next(12, 26);

        }else if (attackChoice == 3)
        {
            attackName = "Tail Whip";
            damage = random.Next(18, 25);

        }else if (attackChoice == 4)
        {
            attackName = "Fire Breath";
            damage = random.Next(35, 41);
            ScreenEffects.Blink(ConsoleColor.DarkRed, 150);
            Console.ResetColor();
        }

        if (Hp < 100)
        {
            if (!_isEnraged)
            {
                Console.WriteLine($"Dragon became ENRAGED! He is dealing yet 2x damage more!");
                _isEnraged = true;
            }
            
            damage *= 2;
        }

        int dodgeRoll = random.Next(1, 101);
        if (dodgeRoll <= hero.heroAgility)
        {
            Console.WriteLine($"Dragon's turn! The dragon tries to use {attackName}!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[DODGE] You avoided {damage} damage from {attackName}!");
            Console.ResetColor();
        }
        else
        {
            hero.Hp -= damage;
            Console.WriteLine($"Dragon's turn! The dragon uses {attackName}!");
            Console.WriteLine($"It deals {damage} damage to {hero.Name}");
        } 
        
    }
    public void DisplayStatus()
    {
        string dragonRed = "\x1b[38;2;192;57;43m"; 
        
        ScreenEffects.ResourceBar("Dragon HP", Hp, MaxHp, dragonRed);
        Console.WriteLine();
    }
}