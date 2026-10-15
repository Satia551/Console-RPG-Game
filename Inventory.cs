using System;
using System.Collections.Generic;

namespace TextGame;
using System.Linq;
internal class Inventory
{
    private List<Consumable> _items = new List<Consumable>()
    {
        new HealthPotion(1, 50),
        new HealthPotion(1, 50),
        new HealthPotion(1, 50),
    };

    public int CountHealthPotions()
    {
        return _items.Count(item => item is HealthPotion);
    }

    public bool UseItem(string choice, Hero target)
    {
        if (choice == "1")
        {
            var item = _items.FirstOrDefault(i => i is HealthPotion);
            if (item != null)
            {
                item.Use(target);
                _items.Remove(item);
                return true;
            }
            {
                Console.WriteLine("You don't have any Health Potions left!");
                return false;
            }
            
        }
        return false;
    }
}