using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FitsYourOut
{
    public class ClothesColorClass
    {
        public readonly ClothesColor Color;
        public ClothesColor[] suitableColors 
            => new ClothesColor[] 
            { 
                Color, 
                (ClothesColor)((int)(Color + 4) % 12), 
                (ClothesColor)((int)(Color + 5) % 12), 
                (ClothesColor)((int)(Color + 8) % 12), 
                (ClothesColor)((int)(Color + 9) % 12) 
            };

        public ClothesColorClass(ClothesColor color)
        {
            Color = color;
        }
    }

    public class Item
    {
        public readonly Image Image;
        public readonly string Name;
        public readonly HashSet<Tags> Tags;
        public readonly double Price;
        public readonly ClothesType Type;
        public readonly ClothesColorClass Color;
        public readonly string Articul;
        public readonly string Description;

        public Item(string name, string articul, string description, HashSet<Tags> tags, double price, ClothesType type, ClothesColorClass color)
        {
            //Image = (Image)Properties.Resources.ResourceManager.GetObject(name);
            Name = name;
            Tags = tags;
            Price = price;
            Type = type;
            Color = color;
            Articul = $"#{articul}";
            Description = description;
        }

        public override bool Equals(object obj)
        {
            return (Item)obj != null && ((Item)obj).Articul == Articul;
        }
    }
}
