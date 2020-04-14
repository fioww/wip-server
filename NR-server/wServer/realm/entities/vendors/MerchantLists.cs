using System;
using System.Collections.Generic;
using System.Linq;
using common;
using common.resources;
using log4net;
using wServer.realm.terrain;

namespace wServer.realm.entities.vendors
{
    public class ShopItem : ISellableItem
    {
        public ushort ItemId { get; private set; }
        public int Price { get; }
        public int Count { get; }
        public string Name { get; }

        public ShopItem(string name, ushort price, int count = -1)
        {
            ItemId = ushort.MaxValue;
            Price = price;
            Count = count;
            Name = name;
        }

        public void SetItem(ushort item)
        {
            if (ItemId != ushort.MaxValue)
                throw new AccessViolationException("Can't change item after it has been set.");

            ItemId = item;
        }
    }
    
    internal static class MerchantLists
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MerchantLists));

        private static readonly List<ISellableItem> Offense = new List<ISellableItem>
        {
            new ShopItem("Dagger of Foul Malevolence", 500),
            new ShopItem("Bow of Covert Havens", 500),
            new ShopItem("Staff of the Cosmic Whole", 500),
            new ShopItem("Wand of Recompense", 500), 
            new ShopItem("Sword of Acclaim", 500),
            new ShopItem("Masamune", 500),
            new ShopItem("Cloak of Ghostly Concealment", 500),
            new ShopItem("Quiver of Elvish Mastery", 500),
            new ShopItem("Elemental Detonation Spell", 500),
            new ShopItem("Tome of Holy Guidance", 500),
            new ShopItem("Helm of the Great General", 500),
            new ShopItem("Colossus Shield", 500),
            new ShopItem("Seal of the Blessed Champion", 500),
            new ShopItem("Baneserpent Poison", 500),
            new ShopItem("Bloodsucker Skull", 500),
            new ShopItem("Giantcatcher Trap", 500),
            new ShopItem("Planefetter Orb", 500),
            new ShopItem("Prism of Apparitions", 500),
            new ShopItem("Scepter of Storms", 500),
            new ShopItem("Doom Circle", 500)
        };

        private static readonly List<ISellableItem> Abilities = new List<ISellableItem>
        {
            new ShopItem("Cloak of Ghostly Concealment", 500),
            new ShopItem("Quiver of Elvish Mastery", 500),  
            new ShopItem("Elemental Detonation Spell", 500),
            new ShopItem("Tome of Holy Guidance", 500),
            new ShopItem("Helm of the Great General", 500),
            new ShopItem("Colossus Shield", 500), 
            new ShopItem("Seal of the Blessed Champion", 500),
            new ShopItem("Baneserpent Poison", 500),
            new ShopItem("Bloodsucker Skull", 500),
            new ShopItem("Giantcatcher Trap", 500),
            new ShopItem("Planefetter Orb", 500),
            new ShopItem("Prism of Apparitions", 500),
            new ShopItem("Scepter of Storms", 500),
            new ShopItem("Doom Circle", 500)
        };

        private static readonly List<ISellableItem> Defense = new List<ISellableItem>
        {
            new ShopItem("Robe of the Illusionist", 50),
            new ShopItem("Robe of the Grand Sorcerer", 500),
            new ShopItem("Studded Leather Armor", 50),
            new ShopItem("Hydra Skin Armor", 500),
            new ShopItem("Mithril Armor", 50),
            new ShopItem("Acropolis Armor", 500),
            new ShopItem("Ring of Paramount Attack", 100),
            new ShopItem("Ring of Paramount Defense", 100),
            new ShopItem("Ring of Paramount Speed", 100),
            new ShopItem("Ring of Paramount Dexterity", 100),
            new ShopItem("Ring of Paramount Vitality", 100),
            new ShopItem("Ring of Paramount Wisdom", 100),
            new ShopItem("Ring of Paramount Health", 100),
            new ShopItem("Ring of Paramount Magic", 100),
            new ShopItem("Ring of Unbound Attack", 750),
            new ShopItem("Ring of Unbound Defense", 750),
            new ShopItem("Ring of Unbound Speed", 750),
            new ShopItem("Ring of Unbound Dexterity", 750),
            new ShopItem("Ring of Unbound Vitality", 750),
            new ShopItem("Ring of Unbound Wisdom", 750),
            new ShopItem("Ring of Unbound Health", 750),
            new ShopItem("Ring of Unbound Magic", 750)
        };

        private static readonly List<ISellableItem> Rings = new List<ISellableItem>
        {
            new ShopItem("Ring of Paramount Attack", 100),
            new ShopItem("Ring of Paramount Defense", 100),
            new ShopItem("Ring of Paramount Speed", 100),
            new ShopItem("Ring of Paramount Dexterity", 100),
            new ShopItem("Ring of Paramount Vitality", 100),
            new ShopItem("Ring of Paramount Wisdom", 100),
            new ShopItem("Ring of Paramount Health", 100),
            new ShopItem("Ring of Paramount Magic", 100),
            new ShopItem("Ring of Unbound Attack", 750),
            new ShopItem("Ring of Unbound Defense", 750),
            new ShopItem("Ring of Unbound Speed", 750),
            new ShopItem("Ring of Unbound Dexterity", 750),
            new ShopItem("Ring of Unbound Vitality", 750),
            new ShopItem("Ring of Unbound Wisdom", 750),
            new ShopItem("Ring of Unbound Health", 750),
            new ShopItem("Ring of Unbound Magic", 750)
        };

        private static readonly List<ISellableItem> Keys = new List<ISellableItem>
        {
            new ShopItem("Undead Lair Key", 40),
            new ShopItem("Sprite World Key", 40),
            new ShopItem("Davy's Key", 130),
            new ShopItem("The Crawling Depths Key", 200),
            new ShopItem("Candy Key", 90),
            new ShopItem("Abyss of Demons Key", 45),
            new ShopItem("Totem Key", 30),
            new ShopItem("Pirate Cave Key", 10),
            new ShopItem("Shatters Key", 220),
            new ShopItem("Beachzone Key", 30),
            new ShopItem("Draconis Key", 200),
            new ShopItem("Lab Key", 40),
            new ShopItem("Manor Key", 60),
            new ShopItem("Cemetery Key", 100),
            new ShopItem("Ocean Trench Key", 200),
            new ShopItem("Snake Pit Key", 30),
            new ShopItem("Bella's Key", 70),
            new ShopItem("Shaitan's Key", 200),
            new ShopItem("Spider Den Key", 20),
            new ShopItem("Tomb of the Ancients Key", 200),
            //new ShopItem("Battle Nexus Key", 150),
            new ShopItem("Deadwater Docks Key", 100),
            new ShopItem("Woodland Labyrinth Key", 160),
            new ShopItem("Theatre Key", 80),
            new ShopItem("Ice Cave Key", 120)
        };

        private static readonly List<ISellableItem> PurchasableFame = new List<ISellableItem>
        {
            new ShopItem("50 Fame", 50),
            new ShopItem("100 Fame", 100),
            new ShopItem("500 Fame", 500),
            new ShopItem("1000 Fame", 1000),
            new ShopItem("5000 Fame", 5000)
        };

        private static readonly List<ISellableItem> Consumables = new List<ISellableItem>
        {
            new ShopItem("Saint Patty's Brew", 80),
            new ShopItem("Snake Oil", 5),
            new ShopItem("Pollen Powder", 5),
            new ShopItem("Mad God Ale", 25),
            new ShopItem("XP Booster 20 min", 35),
            new ShopItem("Backpack", 300),
            new ShopItem("Loot Drop Potion", 250)
            //Level 20 potion
            //Dye removers
        };

        private static readonly List<ISellableItem> Skins = new List<ISellableItem>
        {
            new ShopItem("Stanley the Spring Bunny Skin (SB)", 220),
            new ShopItem("Shoveguy Skin (SB)", 260),
            new ShopItem("B.B. Wolf Skin (SB)", 300),
            new ShopItem("Agent Skin (SB)", 300),
            new ShopItem("Brigand Skin (SB)", 220),
            new ShopItem("Sorceress Skin (SB)", 260),
            new ShopItem("Iceman Skin (SB)", 220),
            new ShopItem("Scarlett Skin (SB)", 220),
            new ShopItem("Lil Red Skin (SB)", 300),
            new ShopItem("Platinum Warrior Skin (SB)", 300),
            new ShopItem("Ranger Skin (SB)", 260),
            new ShopItem("Huntsman Skin (SB)", 260),
            new ShopItem("Holy Avenger Skin (SB)", 260),
            new ShopItem("Hunchback Skin (SB)", 260),
            new ShopItem("Witch Doctor Skin (SB)", 220),
            new ShopItem("Nun Skin (SB)", 220),
            new ShopItem("Witch Skin (SB)", 220),
            new ShopItem("Artemis Skin (SB)", 260),
            new ShopItem("Bashing Bride Skin (SB)", 300),
            new ShopItem("Eligible Bachelor Skin (SB)", 260),
            new ShopItem("Dark Elf Huntress Skin (SB)", 300),
            new ShopItem("Drow Trickster Skin (SB)", 260),
            new ShopItem("Ascended Sorcerer Skin (SB)", 300),
            new ShopItem("King Knifeula Skin (SB)", 300),
            new ShopItem("Platinum Knight Skin (SB)", 300),
            new ShopItem("Platinum Rogue Skin (SB)", 300),
            new ShopItem("Deadly Vixen Skin (SB)", 300)
        };
        
        public static readonly Dictionary<TileRegion, Tuple<List<ISellableItem>, CurrencyType, /*Rank Req*/int>> Shops = 
            new Dictionary<TileRegion, Tuple<List<ISellableItem>, CurrencyType, int>>()
        {
            { TileRegion.Store_1, new Tuple<List<ISellableItem>, CurrencyType, int>(Offense, CurrencyType.Fame, 0) },
            { TileRegion.Store_2, new Tuple<List<ISellableItem>, CurrencyType, int>(Defense, CurrencyType.Fame, 0) },
            { TileRegion.Store_3, new Tuple<List<ISellableItem>, CurrencyType, int>(Keys, CurrencyType.Fame, 0) },
            { TileRegion.Store_4, new Tuple<List<ISellableItem>, CurrencyType, int>(PurchasableFame, CurrencyType.Fame, 5) },
            { TileRegion.Store_5, new Tuple<List<ISellableItem>, CurrencyType, int>(Consumables, CurrencyType.Fame, 0) },
            { TileRegion.Store_6, new Tuple<List<ISellableItem>, CurrencyType, int>(Skins, CurrencyType.Fame, 0) },
            //Store Max = 8
        };
        
        public static void Init(RealmManager manager)
        {
            foreach (var shop in Shops)
                foreach (var shopItem in shop.Value.Item1.OfType<ShopItem>())
                {
                    ushort id;
                    if (!manager.Resources.GameData.IdToObjectType.TryGetValue(shopItem.Name, out id))
                        Log.WarnFormat("Item name: {0}, not found.", shopItem.Name);
                    else
                        shopItem.SetItem(id);
                }
        }
    }
}