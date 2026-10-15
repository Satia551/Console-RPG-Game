namespace TextGame;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Set your's hero name:");
        string userNick = Console.ReadLine() ?? "Unknown hero";
        string inputClass = "";
        while (inputClass != "mage" && inputClass != "warrior")
        {
            Console.WriteLine($"Hello {userNick}! Select your class (Mage or Warrior)");
            inputClass = Console.ReadLine()?.ToLower().Trim() ?? "";


        }

        Hero hero;
        if (inputClass == "mage")
        {
            hero = new Mage(userNick);
        }
        else
        {
            hero = new Warrior(userNick);
        }
        Dragon dragon = new Dragon();
        CombatLog log = new CombatLog();

        hero.OnAttack += dragon.TakeDamage;
        hero.OnAttack += log.LogBattle;

        while (dragon.Hp > 0 && hero.Hp > 0)
        {
            hero.Attack(dragon);
            if (dragon.Hp > 0)
            {
                if (dragon.IsStunned)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[STUN] The dragon is dizzy and cannot attack this round.");
                    dragon.IsStunned = false;
                    Console.ResetColor();
                }
                else
                {
                    dragon.CounterAttack(hero);
                }

            }
        }

        if (hero.Hp > 0)
        {
            Console.WriteLine("Victory!! The dragon has been defeated!");
        }
        else
        {
            Console.WriteLine("GAME OVER! You were burned to crisp by the dragon... :(");
        }
    }
}