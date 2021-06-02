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
                {
                    foreach (var tag in tags)
                        condition = item.Tags.Contains(tag) && condition;
                }
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
                { "Рубашка в клетку", new Item("Рубашка в клетку", "A1", "",  new HashSet<Tags> { Tags.Casual }, 1000, ClothesType.Torso, new ClothesColorClass(ClothesColor.RRR)) },
                { "Джинсы", new Item("Джинсы", "B2", "", new HashSet<Tags> { Tags.Casual, Tags.Sport }, 500, ClothesType.Pants, new ClothesColorClass(ClothesColor.OOG)) },
                { "Кроссовки", new Item("Кроссовки", "C3", "", new HashSet<Tags> { Tags.Sport, Tags.Casual }, 600, ClothesType.Boots, new ClothesColorClass(ClothesColor.RRR)) },
                { "Спортивные штаны", new Item("Спортивные штаны", "D4", "", new HashSet<Tags> { Tags.Sport }, 400, ClothesType.Pants, new ClothesColorClass(ClothesColor.OOG)) }
            };
        }
    }
}
