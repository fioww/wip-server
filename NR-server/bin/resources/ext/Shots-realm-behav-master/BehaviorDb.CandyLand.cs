using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.realm;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ CandyLand = () => Behav()
        .Init("Candy Gnome",
            new State(
                new DropPortalOnDeath("Candyland Portal", percent: 50, PortalDespawnTimeSec: 120),
                new Prioritize(
                    new StayBack(1.5, 55),
                    new Wander(1.4)
                    )
                ),
            new Threshold(0.18,
                new ItemLoot("Red Gumball", 0.5),
                new ItemLoot("Purple Gumball", 0.5),
                new ItemLoot("Blue Gumball", 0.5),
                new ItemLoot("Green Gumball", 0.5),
                new ItemLoot("Yellow Gumball", 0.5)
                )
            )
        .Init("Candyland Spawner",
            new State(
                new State("checkmobs",
                    new ConditionalEffect(ConditionEffectIndex.Invincible),
                    new EntityNotExistsTransition("Big Creampuff", 9999, "despawnbeans")
                    ),
                new State("despawnbeans",
                    new Order(9999, "Candyland Boss Spawner", "spawnbosses"),
                    new Suicide()
                    )
                )
            )
        .Init("Candyland Boss Spawner",
            new State(
                new State("idle",
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    ),
                new State("spawnbosses",
                    new Spawn("Gigacorn", 1, 1, coolDown: 999999),
                    new Suicide()
                    )
                )
            )
        .Init("Candy Jelly Bean",
            new State(
                new State("idle",
                    new EntityNotExistsTransition("Big Creampuff", 9999, "despawn")
                    ),
                new State("despawn",
                    new Decay(0)
                    )
                )
            )
            .Init("Desire Troll",
                new State(
                    new HpLessTransition(0.15, "gimmeloot"),
                    new State(
                        new Wander(0.5),
                        new Grenade(6, 200, range: 8, coolDown: 3000),
                        new Shoot(15, 3, 5, angleOffset: 30 / 3, projectileIndex: 0, coolDown: 2100),
                        new Shoot(15, 5, 10, angleOffset: 60 / 3, projectileIndex: 2, coolDown: 1950),
                        new Shoot(15, 1, 0, angleOffset: 30 / 3, projectileIndex: 1, coolDown: 1950)
                        ),
                    new State("gimmeloot",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt(true, "It's cannot be!"),
                        new TimedTransition(4000, "ripperino")
                        ),
                    new State("ripperino",
                        new Shoot(35, projectileIndex: 1, count: 30),
                        new Suicide()
                        )
                ),
                new MostDamagers(5,
                    new OnlyOne(
                        new ItemLoot("Candy-Coated Armor", 0.02)
                        ),
                    new ItemLoot("Candy Ring", 0.5),
                    new ItemLoot("Fairy Plate", 0.03),
                    new ItemLoot("Pixie-Enchanted Sword", 0.02),
                    new ItemLoot("Seal of the Enchanted Forest", 0.07),
                    new ItemLoot("Ring of Pure Wishes", 0.05),
                    new ItemLoot("Peppermint Snail Egg", 0.8),
                    new ItemLoot("Wine Cellar Incantation", 0.05),
                    new TierLoot(6, ItemType.Weapon, 0.8),
                    new TierLoot(7, ItemType.Weapon, 0.7),
                    new TierLoot(7, ItemType.Armor, 0.6),
                    new TierLoot(8, ItemType.Armor, 0.7),
                    new TierLoot(3, ItemType.Ring, 0.5),
                    new TierLoot(4, ItemType.Ring, 0.6),
                    new TierLoot(3, ItemType.Ability, 0.7),
                    new TierLoot(4, ItemType.Ability, 0.8),
                    new ItemLoot("The Sun Tarot Card", 0.08),
                    new ItemLoot("Yellow Gumball", 0.3),
                    new ItemLoot("Green Gumball", 0.3),
                    new ItemLoot("Blue Gumball", 0.3),
                    new ItemLoot("Red Gumball", 0.3),
                    new ItemLoot("Blue Gumball", 0.3)
                    )
            )
            .Init("Gigacorn",
                new State(
                    new TransformOnDeath("Desire Troll", 1, 1, 1, true),
                    //new DropPortalOnDeath("Desire Troll", percent: 35, dropDelaySec: 10, XAdjustment: 0, YAdjustment: 0, PortalDespawnTimeSec: 999999),
                    new HpLessTransition(0.15, "gimmeloot"),
                    new State(
                        new Wander(0.5),
                        new Charge(2.0, 10f, 4000),
                        new Shoot(20, 1, 40, angleOffset: 60 / 3, projectileIndex: 0, coolDown: 2100),
                        new Shoot(20, 1, 40, angleOffset: 60 / 3, projectileIndex: 0, coolDown: 2200),
                        new Shoot(20, 1, 40, angleOffset: 60 / 3, projectileIndex: 0, coolDown: 2300),
                        new Shoot(20, 1, 40, angleOffset: 60 / 3, projectileIndex: 0, coolDown: 2400),
                        new Shoot(20, 3, 15, angleOffset: 40 / 3, projectileIndex: 1, coolDown: 4000),
                        new Shoot(20, 3, 15, angleOffset: 20 / 3, projectileIndex: 1, coolDown: 2000)
                        ),
                    new State("gimmeloot",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Taunt(true, "No waaaaaaaaay! Good luck if you can spawn Desire Troll, it can spawn before my death..."),
                        new TimedTransition(4000, "ripperino")
                        ),
                    new State("ripperino",
                        new Shoot(35, projectileIndex: 1, count: 30),
                        new Suicide()
                        )
                ),
                       new MostDamagers(5,
                    new OnlyOne(
                        new ItemLoot("Candy-Coated Armor", 0.02)
                        ),
                    new ItemLoot("Candy Ring", 0.5),
                    new ItemLoot("Fairy Plate", 0.02),
                    new ItemLoot("Pixie-Enchanted Sword", 0.03),
                    new ItemLoot("Seal of the Enchanted Forest", 0.07),
                    new ItemLoot("Ring of Pure Wishes", 0.05),
                    new ItemLoot("Peppermint Snail Egg", 0.8),
                    new ItemLoot("Wine Cellar Incantation", 0.05),
                    new TierLoot(6, ItemType.Weapon, 0.8),
                    new TierLoot(7, ItemType.Weapon, 0.7),
                    new TierLoot(7, ItemType.Armor, 0.6),
                    new TierLoot(8, ItemType.Armor, 0.7),
                    new TierLoot(3, ItemType.Ring, 0.5),
                    new TierLoot(4, ItemType.Ring, 0.6),
                    new TierLoot(3, ItemType.Ability, 0.7),
                    new TierLoot(4, ItemType.Ability, 0.8),
                    new ItemLoot("The Sun Tarot Card", 0.08),
                    new ItemLoot("Yellow Gumball", 0.3),
                    new ItemLoot("Green Gumball", 0.3),
                    new ItemLoot("Blue Gumball", 0.3),
                    new ItemLoot("Red Gumball", 0.3),
                    new ItemLoot("Blue Gumball", 0.3)
                    )
            )
            .Init("Spoiled Creampuff",
                new State(
                    new Shoot(20, 2, 40, angleOffset: 60 / 3, projectileIndex: 0, coolDown: 1500),
                    new Shoot(20, 4, 15, angleOffset: 40 / 3, projectileIndex: 1, coolDown: 1000),
                    new Spawn("Big Creampuff", maxChildren: 2, initialSpawn: 2, coolDown: 5000)
            ),
                       new MostDamagers(5,
                    new OnlyOne(
                        new ItemLoot("Candy-Coated Armor", 0.02)
                        ),
                    new ItemLoot("Candy Ring", 0.5),
                    new ItemLoot("Fairy Plate", 0.02),
                    new ItemLoot("Pixie-Enchanted Sword", 0.03),
                    new ItemLoot("Seal of the Enchanted Forest", 0.07),
                    new ItemLoot("Ring of Pure Wishes", 0.05),
                    new ItemLoot("Peppermint Snail Egg", 0.8),
                    new ItemLoot("Wine Cellar Incantation", 0.05),
                    new TierLoot(6, ItemType.Weapon, 0.8),
                    new TierLoot(7, ItemType.Weapon, 0.7),
                    new TierLoot(7, ItemType.Armor, 0.6),
                    new TierLoot(8, ItemType.Armor, 0.7),
                    new TierLoot(3, ItemType.Ring, 0.5),
                    new TierLoot(4, ItemType.Ring, 0.6),
                    new TierLoot(3, ItemType.Ability, 0.7),
                    new TierLoot(4, ItemType.Ability, 0.8),
                    new ItemLoot("The Sun Tarot Card", 0.08),
                    new ItemLoot("Yellow Gumball", 0.3),
                    new ItemLoot("Green Gumball", 0.3),
                    new ItemLoot("Blue Gumball", 0.3),
                    new ItemLoot("Red Gumball", 0.3),
                    new ItemLoot("Blue Gumball", 0.3)
                )
            )
            .Init("Big Creampuff",
                new State(
                    new Follow(0.5, range: 12),
                    new Charge(2, 7, coolDown: 7000),
                    new Shoot(20, 1, 0, angleOffset: 40 / 3, projectileIndex: 0, coolDown: 1000),
                    new Spawn("Small Creampuff", maxChildren: 2, initialSpawn: 0.5, coolDown: 5000)
                )
            )
            .Init("Small Creampuff",
                new State(
                    new Follow(1, range: 6),
                    new Charge(2, 9, coolDown: 7000),
                    new Shoot(20, 3, 30, angleOffset: 40 / 3, projectileIndex: 1, coolDown: 1400)
                )
            );
    }
}
