using System;

namespace TextGame;

class CombatLog
{
    public void LogBattle(string heroName, string skillName, int damage)
    {
        if (skillName.Contains("Potion") || skillName.Contains("Item"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{heroName} accesed inventory to use {skillName}!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"LOG: {damage} damage points were dealt using {skillName}");
            Console.ResetColor();
        }
    }
    
}