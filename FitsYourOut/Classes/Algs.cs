using System.Collections.Generic;
using System.Linq;

namespace FitsYourOut
{
    public static class Algorythms
    {
        public static Item[] GetSuitableItems(this Item currentItem, IEnumerable<Item> collection, IEnumerable<Tags> tags) =>
            collection.Where(item =>
            {
                var condition = currentItem.Type != item.Type
                    && (item.Color.Color == currentItem.Color.Color
                            || currentItem.Color.suitableColors.Contains(item.Color.Color));
                if (condition)
                    foreach (var tag in tags)
                        condition = item.Tags.Contains(tag) && condition;
                return condition;
            }).ToArray();

        public static Item[] GetItemsByPrice(double minPrice, double maxPrice, IEnumerable<Item> collection) =>
            collection.Where(item => item.Price <= maxPrice && item.Price >= minPrice).ToArray();

        public static Item[] GetSuitableItemsBySingleTag(this Item currentItem, IEnumerable<Item> collection, Tags tag) =>
            collection.Where(item =>
            {
                var condition = currentItem.Type != item.Type
                    && (item.Color.Color == currentItem.Color.Color
                            || currentItem.Color.suitableColors.Contains(item.Color.Color)) && item.Tags.Contains(tag);
                return condition;
            }).ToArray();

        public static Dictionary<string, Item> GetItemsCollection()
        {
            return new Dictionary<string, Item>
            {
                { "Зеленые Штаны", new Item("pants1",
                "Green pants", 
                "a1",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et \ndolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex \nea commodo consequat. Duis aute irure dolor " +
                "in reprehenderit in voluptate velit esse cillum dolore \neu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, " +
                "sunt in culpa qui officia deserunt \nmollit anim id est laborum.",  
                new HashSet<Tags> { Tags.Casual }, 
                10.50, 
                ClothesType.Pants, 
                new ClothesColorClass(ClothesColor.GGG)) },
                { "Джинсы", new Item("pants1","Blue Jeans", "B2", "", new HashSet<Tags> { Tags.Casual,  }, 500, ClothesType.Pants, new ClothesColorClass(ClothesColor.GGG)) },
                { "Кроссовки", new Item("pants1","Yellow Sneakers", "C3", "", new HashSet<Tags> {Tags.Casual }, 600, ClothesType.Boots, new ClothesColorClass(ClothesColor.GGG)) },
                { "Спортивные штаны", new Item("pants1","Sport pants", "D4", "", new HashSet<Tags> { Tags.Casual }, 400, ClothesType.Pants, new ClothesColorClass(ClothesColor.GGG)) },
                { "", new Item("pants1", "Hat", "E6", "", new HashSet<Tags> { Tags.Casual }, 350, ClothesType.Head, new ClothesColorClass(ClothesColor.GGG)) },
                { "1", new Item("pants1", "1", "E6", "", new HashSet<Tags> { Tags.Casual }, 350, ClothesType.Head, new ClothesColorClass(ClothesColor.GGG)) },
                { "2", new Item("pants1", "2", "E6", "", new HashSet<Tags> { Tags.Casual }, 350, ClothesType.Head, new ClothesColorClass(ClothesColor.GGG)) },
                { "3", new Item("pants1", "3", "E6", "", new HashSet<Tags> { Tags.Casual }, 350, ClothesType.Head, new ClothesColorClass(ClothesColor.GGG)) },
                { "4", new Item("pants1", "4", "E6", "", new HashSet<Tags> { Tags.Casual }, 350, ClothesType.Head, new ClothesColorClass(ClothesColor.GGG)) },
                { "5", new Item("pants1", "5", "E6", "", new HashSet<Tags> { Tags.Casual }, 350, ClothesType.Head, new ClothesColorClass(ClothesColor.GGG)) },
                { "6", new Item("pants1", "6", "E6", "", new HashSet<Tags> { Tags.Casual }, 350, ClothesType.Head, new ClothesColorClass(ClothesColor.GGG)) },
            };
        }
    }
}
