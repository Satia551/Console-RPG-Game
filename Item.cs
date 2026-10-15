namespace TextGame;

public class Item
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string EffectType { get; set; }
    public int Value { get; set; }

    public Item(string name, string type, string effectType, int value)
    {
        Name = name;
        Type = type;
        EffectType = effectType;
        Value = value;
    }
}