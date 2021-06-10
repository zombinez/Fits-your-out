using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FitsYourOut
{
    public class ClothesColorClass
    {
        public readonly ClothesColor Color;
        public ClothesColor[] suitableColors
        {
            get
            {
                if (Color > ClothesColor.WWW && Color <= ClothesColor.BLBLBL)
                    return
                        new ClothesColor[] { ClothesColor.WWW, ClothesColor.WWBL, ClothesColor.WBLBL, ClothesColor.BLBLBL };
                if (Color == ClothesColor.WWW)
                    return new ClothesColor[]
                    {
                        ClothesColor.RRR,
                        ClothesColor.RRO,
                        ClothesColor.ROO,
                        ClothesColor.OOO,
                        ClothesColor.OOG,
                        ClothesColor.OGG,
                        ClothesColor.GGG,
                        ClothesColor.GGB,
                        ClothesColor.GBB,
                        ClothesColor.BBB,
                        ClothesColor.BBR,
                        ClothesColor.BRR,
                        ClothesColor.WWW,
                        ClothesColor.WWBL,
                        ClothesColor.WBLBL,
                        ClothesColor.BLBLBL
                    };
                return new ClothesColor[]
                {
                        Color,
                        ClothesColor.WWW,
                        (ClothesColor)((int)(Color + 4) % 12),
                        (ClothesColor)((int)(Color + 5) % 12),
                        (ClothesColor)((int)(Color + 8) % 12),
                        (ClothesColor)((int)(Color + 9) % 12)
                };
            }
        }

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
        public readonly Gender Gender;
        public readonly double Price;
        public readonly ClothesType Type;
        public readonly ClothesColorClass Color;
        public readonly string Articul;
        public readonly string Description;

        public Item(string name, string articul, string description, 
            HashSet<Tags> tags, double price, ClothesType type, ClothesColorClass color, Gender gender)
        {
            Image = (Image)Properties.Resources.ResourceManager.GetObject(articul);
            Name = name;
            Tags = tags;
            Price = price;
            Type = type;
            Color = color;
            Articul = $"#{articul}";
            Description = description;
            Gender = gender;
        }

        public override bool Equals(object obj)
        {
            return (Item)obj != null && ((Item)obj).Articul == Articul;
        }
    }
}
