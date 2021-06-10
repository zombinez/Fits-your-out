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
                    && (currentItem.Color.suitableColors.Contains(item.Color.Color) || item.Type == ClothesType.Accesouries);
                if (condition)
                    foreach (var tag in tags)
                        condition = item.Tags.Contains(tag) && condition;
                return condition;
            }).ToArray();

        public static Item[] GetItemsByPrice(double minPrice, double maxPrice, IEnumerable<Item> collection) =>
            collection.Where(item => item.Price <= maxPrice && item.Price >= minPrice).ToArray();

        public static Item[] GetItemsByGender(this IEnumerable<Item> collection, Gender gender) =>
            collection.Where(item => gender == Gender.All || item.Gender == gender).ToArray();

        public static Item[] GetSuitableItemsBySingleTag(this Item currentItem, IEnumerable<Item> collection, Tags tag) =>
            collection.Where(item =>
            {
                var condition = currentItem.Type != item.Type
                    && (item.Tags.Contains(tag) || item.Type == ClothesType.Accesouries);
                return condition;
            }).ToArray();

        public static Dictionary<string, Item> GetItemsCollection()
        {
            return new Dictionary<string, Item>
            {
                { "a1", new Item(
                "Shirt \"Oranges\"", 
                "a1",
                "Short-sleeved shirt in printed viscose. \nMaterials: \n - Viscose 100%.",  
                new HashSet<Tags> { Tags.Casual }, 
                19.99, 
                ClothesType.Torso, 
                new ClothesColorClass(ClothesColor.WWW), 
                Gender.Male) },

                { "b2", new Item(
                "Shorts", 
                "b2",
                "Crisp, woven shorts with smooth sleeves. \nMaterials: \n - Polyester 68%, \n - Viscose 28%, \n - Elastane 4%.", 
                new HashSet<Tags> { Tags.Casual, Tags.Sport }, 
                15.99, 
                ClothesType.Pants, 
                new ClothesColorClass(ClothesColor.BLBLBL), 
                Gender.Male) },

                { "c3", new Item(
                "Sneakers", 
                "c3",
                "Chunky sole with padded upper edge and tongue and lace-up front. \nMaterials: \n - Thermoplastic rubber 100%.", 
                new HashSet<Tags> { Tags.Casual, Tags.Sport }, 
                30.00, 
                ClothesType.Boots, 
                new ClothesColorClass(ClothesColor.WWW), 
                Gender.Male) },

                { "d4", new Item(
                "Shirt", 
                "d4",
                "Short-sleeved shirt in printed viscose. \nMaterials: \n - Viscose 100%.", 
                new HashSet<Tags> { Tags.Casual }, 
                11.99, 
                ClothesType.Torso, 
                new ClothesColorClass(ClothesColor.GGB), 
                Gender.Male) },

                { "e6", new Item(
                "TShirt \"R&M\"", 
                "e6",
                "T-shirt in soft cotton jersey with a crew neck and a theme print on the front. \nMaterials:\n - Cotton 100%.", 
                new HashSet<Tags> { Tags.Casual }, 
                10.99, 
                ClothesType.Torso, 
                new ClothesColorClass(ClothesColor.BLBLBL), 
                Gender.Male) },

                { "f6", new Item(
                "TShirt", 
                "f6",
                "Cropped top in jersey with round neck and short sleeves. \nMaterials: \n - Cotton 100%.", 
                new HashSet<Tags> { Tags.Casual }, 
                10.00, 
                ClothesType.Torso, 
                new ClothesColorClass(ClothesColor.GBB), 
                Gender.Female) },

                { "g6", new Item(
                "Bermuda shorts", 
                "g6",
                "Longline shorts with five pockets in cotton denim. Unfinished, cropped edge at the bottom. \nMaterials: \n - Cotton 100%.", 
                new HashSet<Tags> { Tags.Casual, Tags.Grunge}, 
                19.99, 
                ClothesType.Pants, 
                new ClothesColorClass(ClothesColor.GBB), 
                Gender.Female) },

                { "h6", new Item(
                "Derby boots", 
                "h6",
                "Faux leather platform derby boots with lace-up top. \nMaterials: \n - Artificial leather 100%, \n - Thermoplastic rubber (Sole) 100%", 
                new HashSet<Tags> { Tags.Casual, Tags.Grunge }, 
                30.00, 
                ClothesType.Boots, 
                new ClothesColorClass(ClothesColor.BLBLBL),
                Gender.Female) },

                { "i6", new Item(
                "Pumps", 
                "i6",
                "Faux leather pumps with pointed toes and covered heel. Heel 5 cm. \nMaterials: \n - Imitation leather 100%, \n - Thermoplastic polyurethane (Sole) 100%.", 
                new HashSet<Tags> { Tags.Romantic }, 
                16.89, 
                ClothesType.Boots, 
                new ClothesColorClass(ClothesColor.BRR),
                Gender.Female) },

                { "j6", new Item(
                "Dress", 
                "j6",
                "Long dress with a printed pattern. Ruffle straps and v-neck. On the back there is an insert \non the assembly with small puffs. \nMaterials: \n - Polyester 100%.", 
                new HashSet<Tags> { Tags.Romantic }, 
                18.89, 
                ClothesType.Torso, 
                new ClothesColorClass(ClothesColor.BRR), 
                Gender.Female) },

                { "k6", new Item(
                "Straw hat", 
                "k6",
                "Hat made from woven paper straw. A string is tied under the chin. \nMaterials:\n - Paper 100%.", 
                new HashSet<Tags> { Tags.Romantic }, 
                7.00, ClothesType.Head, 
                new ClothesColorClass(ClothesColor.WWW), 
                Gender.Female) },

                { "l6", new Item(
                "Sport Tshirt",
                "l6",
                "Sports T-shirt in heavy jersey with sealed details. \nMaterials:\n - Polyester 56%,\n - Cotton 44%.",
                new HashSet<Tags> { Tags.Sport },
                12.00, ClothesType.Torso,
                new ClothesColorClass(ClothesColor.WBLBL), 
                Gender.Male) },

                { "m6", new Item(
                "Sport Pants",
                "m6",
                "Sports joggers in heavy jersey with sealed details. \nMaterials:\n - Polyester 58%,\n - Cotton 42%.",
                new HashSet<Tags> { Tags.Sport },
                12.00, ClothesType.Pants,
                new ClothesColorClass(ClothesColor.WBLBL),
                Gender.Male) },

                { "n6", new Item(
                "Polarized glasses",
                "n6",
                "Metal sunglasses with UV-resistant polarized lenses. The sunglasses are sold in a fabric drawstring case. \nMaterials:\n - Metal 66%,\n - Plastic 34%. \nWeight - 35 g.",
                new HashSet<Tags>(),
                14.00, ClothesType.Accesouries,
                new ClothesColorClass(ClothesColor.BLBLBL),
                Gender.Male) },

                { "o6", new Item(
                "Shopper",
                "o6",
                "Metal sunglasses with UV-resistant polarized lenses. The sunglasses are sold in a fabric drawstring case. \nMaterials:\n - Metal 66%,\n - Plastic 34%. \nWeight - 35 g.",
                new HashSet<Tags>(),
                14.00, ClothesType.Accesouries,
                new ClothesColorClass(ClothesColor.WWW),
                Gender.Female) },
            };
        }
    }
}
