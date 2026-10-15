using System;
using System.Threading;

namespace TextGame;

public class ScreenEffects
{
    public static void Blink(ConsoleColor color, int durationMs = 100)
    {
        ConsoleColor originalBackground = Console.BackgroundColor;
        ConsoleColor originalForeground = Console.ForegroundColor;
        Console.BackgroundColor = color;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(">>> !!! DRAGON'S INFERNO FLASH !!!  <<<");
        Thread.Sleep(durationMs);
        Console.BackgroundColor = originalBackground;
        Console.ForegroundColor = originalForeground;
        Console.ResetColor();
        Console.WriteLine(new string(' ', 300));
        Thread.Sleep(500);
        Console.WriteLine();
        Thread.Sleep(500);
        Console.WriteLine(); ;
    }

    public static void ResourceBar(string label, int currentVal, int maxVal, string colorCode)
    {
        
        int totalBlocks = 10;
        float percentage = (float)currentVal / maxVal;
        int filledBlocks = (int)Math.Round(percentage * totalBlocks);
        if (filledBlocks < 0) filledBlocks = 0;
        if (filledBlocks > totalBlocks) filledBlocks = totalBlocks;

        Console.Write($"{label}: [");
        Console.Write(colorCode);
        for (int i = 0; i < filledBlocks; i++)
        {
            Console.Write("■");
        }
        Console.Write("\x1b[38;2;100;100;100m");
        for (int i = filledBlocks; i < totalBlocks; i++)
        {
            Console.Write("■");
        }
        Console.Write("\x1b[0m");
        Console.WriteLine($"] {currentVal}/{maxVal}");

    }
}