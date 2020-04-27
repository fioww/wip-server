using common.resources;
using System;
using System.Collections.Generic;
using System.Linq;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.loot
{
    public struct LootDef
    {
        public LootDef(Item item, double probabilty) {
            Item = item;
            Probability = probabilty;
        }

        public readonly Item Item;
        public readonly double Probability;
    }

    public class Loot : List<ILootDef>
    {
        public Loot() {
        }

        public Loot(params ILootDef[] lootDefs) {  //For independent loots(e.g. chests)
            AddRange(lootDefs);
        }

        private static readonly Random Rand = new Random();

        public IEnumerable<Item> GetLoots(RealmManager manager, int min, int max) {  //For independent loots(e.g. chests)
            var consideration = new List<LootDef>();
            foreach (var i in this)
                i.Populate(manager, null, null, Rand, consideration);

            var retCount = Rand.Next(min, max);
            foreach (var i in consideration) {
                if (Rand.NextDouble() < i.Probability) {
                    yield return i.Item;
                    retCount--;
                }
                if (retCount == 0)
                    yield break;
            }
        }

        public void Handle(Enemy enemy, RealmTime time) {
            var consideration = new List<LootDef>();

            var sharedLoots = new List<Item>();
            foreach (var i in this)
                i.Populate(enemy.Manager, enemy, null, Rand, consideration);
            foreach (var i in consideration) {
                if (Rand.NextDouble() < i.Probability)
                    sharedLoots.Add(i.Item);
            }

            var dats = enemy.DamageCounter.GetPlayerData();
            var loots = enemy.DamageCounter.GetPlayerData().ToDictionary(
                d => d.Item1, d => (IList<Item>)new List<Item>());

            foreach (var loot in sharedLoots.Where(item => item.Soulbound))
                loots[dats[Rand.Next(dats.Length)].Item1].Add(loot);

            foreach (var dat in dats) {
                consideration.Clear();
                foreach (var i in this)
                    i.Populate(enemy.Manager, enemy, dat, Rand, consideration);

                var lootDropBoost = dat.Item1.LDBoostTime > 0 ? 1.5 : 1;
                var luckStatBoost = 1 + dat.Item1.Stats.Boost[10] / 100.0;

                var playerLoot = loots[dat.Item1];
                foreach (var i in consideration) {
                    if (Rand.NextDouble() < i.Probability * lootDropBoost * luckStatBoost)
                        playerLoot.Add(i.Item);
                }
            }

            AddBagsToWorld(enemy, sharedLoots, loots);
        }

        private static void AddBagsToWorld(Enemy enemy, IList<Item> shared, IDictionary<Player, IList<Item>> soulbound) {
            var pub = new List<Player>();  //only people not getting soulbound
            foreach (var i in soulbound) {
                if (i.Value.Count > 0)
                    ShowBags(enemy, i.Value, i.Key);
                else
                    pub.Add(i.Key);
            }
            if (pub.Count > 0 && shared.Count > 0)
                ShowBags(enemy, shared, pub.ToArray());
        }

        private static void ShowBags(Enemy enemy, IEnumerable<Item> loots, params Player[] owners) {
            var ownerIds = owners.Select(x => x.AccountId).ToArray();
            var bagType = 0;
            var items = new Item[8];
            var idx = 0;

            foreach (var i in loots) {
                if (i.BagType > bagType) bagType = i.BagType;
                items[idx] = i;
                idx++;

                if (idx == 8) {
                    ShowBag(enemy, ownerIds, bagType, items);

                    bagType = 0;
                    items = new Item[8];
                    idx = 0;
                }
            }

            if (idx > 0)
                ShowBag(enemy, ownerIds, bagType, items);
        }

        private static void ShowBag(Enemy enemy, int[] owners, int bagType, Item[] items) {
            ushort bag = 0x0087; // Brown Bag
            switch (bagType)
            {
                case 0:
                    bag = 0x0087;
                    break;

                case 1:
                    bag = 0x0088; // Pink Bag
                    break;

                case 2:
                    bag = 0x0086; // Purple Bag
                    break;

                case 3:
                    bag = 0x008a; // Egg Bag
                    break;

                case 4:
                    bag = 0x008b; // Cyan Bag
                    break;

                case 5:
                    bag = 0x008c; // Blue Bag
                    break;

                case 6:
                    bag = 0x11aa; // Green Bag      (Skin/Reskin Bag)
                    break;

                case 7:
                    bag = 0x11ac; // Orange Bag     (Special Tiered Bag)
                    break;

                case 8:
                    bag = 0x008d; // White Bag
                    break;

                case 9:
                    bag = 0x11ab; // Yellow Bag     (Legendary Bag)
                    break;
            }

            var dats = enemy.DamageCounter.GetPlayerData();
            foreach (var dat in dats)
            {
                if (dat.Item1.LDBoostTime > 0 || dat.Item1.Stats.Boost[10] > 0 && bag < 6) // temporary until we have boosted bags for each bag
                    bag = 0x0090; // Red/Boosted Bag
            }

            var container = new Container(enemy.Manager, bag, 1000 * 60, true);
            for (var j = 0; j < 8; j++)
                container.Inventory[j] = items[j];
            container.BagOwners = owners;
            container.Move(
                enemy.X + (float)((Rand.NextDouble() * 2 - 1) * 0.55),
                enemy.Y + (float)((Rand.NextDouble() * 2 - 1) * 0.55));
            container.SetDefaultSize(bagType > 6 ? 120 : bagType > 3 ? 100 : 80);
            enemy.Owner.EnterWorld(container);
        }
    }
}