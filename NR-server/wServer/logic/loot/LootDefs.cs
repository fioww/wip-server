using System;
using System.Collections.Generic;
using System.Linq;
using common.resources;
using log4net;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.loot
{
    public interface ILootDef
    {
        void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
                      Random rand, IList<LootDef> lootDefs);
    }

    internal class MostDamagers : ILootDef
    {
        private readonly ILootDef[] _loots;
        private readonly int _amount;

        public MostDamagers(int amount, params ILootDef[] loots)
        {
            _amount = amount;
            _loots = loots;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand, IList<LootDef> lootDefs)
        {
            var data = enemy.DamageCounter.GetPlayerData();
            var mostDamage = GetMostDamage(data);
            foreach (var loot in mostDamage.Where(pl => pl.Equals(playerDat)).SelectMany(pl => _loots))
                loot.Populate(manager, enemy, null, rand, lootDefs);
        }

        private IEnumerable<Tuple<Player, int>> GetMostDamage(IEnumerable<Tuple<Player, int>> data)
        {
            var damages = data.Select(_ => _.Item2).ToList();
            var len = damages.Count < _amount ? damages.Count : _amount;
            for (var i = 0; i < len; i++)
            {
                var val = damages.Max();
                yield return data.FirstOrDefault(_ => _.Item2 == val);
                damages.Remove(val);
            }
        }
    }

    public class OnlyOne : ILootDef
    {
        private readonly ILootDef[] _loots;

        public OnlyOne(params ILootDef[] loots)
        {
            _loots = loots;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat, Random rand, IList<LootDef> lootDefs)
        {
            _loots[rand.Next(0, _loots.Length)].Populate(manager, enemy, playerDat, rand, lootDefs);
        }
    }

    public class ItemLoot : ILootDef
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ItemLoot));
        private readonly string _item;
        private readonly double _probability;

        public ItemLoot(string item, double probability)
        {
            _item = item;
            _probability = probability;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
                             Random rand, IList<LootDef> lootDefs)
        {
            if (playerDat != null) return;
            var dat = manager.Resources.GameData;
            if (dat.IdToObjectType.ContainsKey(_item)
                && dat.Items.ContainsKey(dat.IdToObjectType[_item]))
            {
                try
                {
                    lootDefs.Add(new LootDef(dat.Items[dat.IdToObjectType[_item]], _probability));

                }
                catch
                {
                    Log.Warn($"Problem adding {_item} to mob loot table.");
                }
            }
        }
    }

    public enum LItemType
    {
        Weapon,
        Ability,
        Armor,
        Ring,
        Potion
    }

    public class TierLoot : ILootDef
    {
        public static readonly int[] WeaponT = { 1, 2, 3, 8, 17, 24, 29, 34 };
        public static readonly int[] AbilityT = { 4, 5, 11, 12, 13, 15, 16, 18, 19, 20, 21, 22, 23, 27, 28, 30, 32, 33, 35, 36 };
        public static readonly int[] ArmorT = { 6, 7, 14 };
        public static readonly int[] RingT = { 9 };
        public static readonly int[] PotionT = { 10 };

        private readonly byte _tier;
        private readonly int[] _types;
        private readonly double _probability;

        public TierLoot(byte tier, ItemType type, double probability)
        {
            _tier = tier;
            switch (type)
            {
                case ItemType.Weapon:
                    _types = WeaponT; break;
                case ItemType.Ability:
                    _types = AbilityT; break;
                case ItemType.Armor:
                    _types = ArmorT; break;
                case ItemType.Ring:
                    _types = RingT; break;
                case ItemType.Potion:
                    _types = PotionT; break;
                default:
                    throw new NotSupportedException(type.ToString());
            }
            _probability = probability;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
                             Random rand, IList<LootDef> lootDefs)
        {
            if (playerDat != null) return;
            var candidates = manager.Resources.GameData.Items
                .Where(item => Array.IndexOf(_types, item.Value.SlotType) != -1)
                .Where(item => item.Value.Tier == _tier)
                .Select(item => item.Value)
                .ToArray();
            foreach (var i in candidates)
                lootDefs.Add(new LootDef(i, _probability / candidates.Length));
        }
    }

    public static class LootTemplates
    {
        public static ILootDef[] StatPots()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new ItemLoot("Potion of Defense", 1),
                    new ItemLoot("Potion of Attack", 1),
                    new ItemLoot("Potion of Speed", 1),
                    new ItemLoot("Potion of Vitality", 1),
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("Potion of Dexterity", 1)
                )
             };
        }

        public static ILootDef[] StatPotsEvents()
        {
            return new ILootDef[]
            {
                new OnlyOne(
                    new ItemLoot("Potion of Defense", 0.1),
                    new ItemLoot("Potion of Attack", 0.1),
                    new ItemLoot("Potion of Speed", 0.1),
                    new ItemLoot("Potion of Vitality", 0.1),
                    new ItemLoot("Potion of Wisdom", 0.1),
                    new ItemLoot("Potion of Dexterity", 0.1)
                )
             };
        }


    }

    public class Threshold : ILootDef
    {
        private readonly double _threshold;
        private readonly ILootDef[] _children;

        public Threshold(double threshold, params ILootDef[] children)
        {
            _threshold = threshold;
            _children = children;
        }

        public void Populate(RealmManager manager, Enemy enemy, Tuple<Player, int> playerDat,
                             Random rand, IList<LootDef> lootDefs)
        {
            if (playerDat != null && playerDat.Item2 / (double)enemy.ObjectDesc.MaxHP >= (_threshold - (_threshold / Math.Max(enemy.Owner.Players.Count() / 2, 1))))
            {
                foreach (var i in _children)
                    i.Populate(manager, enemy, null, rand, lootDefs);
            }
        }
    }
}