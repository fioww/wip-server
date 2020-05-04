﻿using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Abyss = () => Behav()
            .Init("Archdemon Malphas",
                new State(
                    new ScaleHP(2000, 0),
                    new RealmPortalDrop(),
                    new State("default",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new PlayerWithinTransition(8, "basic")
                        ),
                    new State("basic",
                        new Prioritize(
                            new Follow(0.3, range: 2),
                            new Wander(0.2)
                            ),
                        new Reproduce("Malphas Missile", densityMax: 1,  coolDown: 1500),
                        new Shoot(10, predictive: 1, coolDown: 1200),
                        new TimedTransition(10000, "shrink")
                        ),
                    new State("shrink",
                        new Wander(0.4),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(-15, 25),
                        new TimedTransition(1000, "smallAttack")
                        ),
                    new State("smallAttack",
                        new Prioritize(
                            new Follow(1, acquireRange: 15, range: 8),
                            new Wander(1)
                            ),
                        new Shoot(10, predictive: 1, coolDown: 750),
                        new Shoot(10, 6, projectileIndex: 1, predictive: 1, coolDown: 1000),
                        new TimedTransition(10000, "grow")
                        ),
                    new State("grow",
                        new Wander(0.1),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(35, 200),
                        new TimedTransition(1050, "bigAttack")
                        ),
                    new State("bigAttack",
                        new Prioritize(
                            new Follow(0.2),
                            new Wander(0.1)
                            ),
                        new Shoot(10, projectileIndex: 2, predictive: 1, coolDown: 2400),
                        new Shoot(10, projectileIndex: 2, predictive: 1, coolDownOffset: 800, coolDown: 2400),
                        new Shoot(10, 3, projectileIndex: 3, predictive: 1, coolDownOffset: 400, coolDown: 2000),
                        new Shoot(10, 3, projectileIndex: 3, predictive: 1, coolDownOffset: 990, coolDown: 2000),
                        new TimedTransition(10000, "normalize")
                        ),
                    new State("normalize",
                        new Wander(0.3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new ChangeSize(-20, 100),
                        new TimedTransition(1000, "basic")
                        )
                    ),
                 new MostDamagers(2,
                    new OnlyOne(
                        new ItemLoot("Large Bold Diamond Cloth", 0.03),
                        new ItemLoot("Small Bold Diamond Cloth", 0.03),
                        new ItemLoot("Large Brown Stitch Cloth", 0.03),
                        new ItemLoot("Small Brown Stitch Cloth", 0.03)
                     )
                 ),
                new Threshold(0.025,
                    new ItemLoot("Demon Blade", 0.008),
                    new ItemLoot("Potion of Vitality", 1.0),
                    new ItemLoot("Potion of Defense", 0.33),
                    new TierLoot(9, ItemType.Weapon, 0.1),
                    new TierLoot(4, ItemType.Ability, 0.1),
                    new TierLoot(9, ItemType.Armor, 0.1),
                    new TierLoot(3, ItemType.Ring, 0.05),
                    new TierLoot(10, ItemType.Armor, 0.05),
                    new TierLoot(10, ItemType.Weapon, 0.05),
                    new TierLoot(4, ItemType.Ring, 0.025)
                )
            )
            .Init("Malphas Missile",
                new State(
                    new State(
                        new Prioritize(
                            new Follow(0.4, acquireRange: 7, range: 0),
                            new Wander(0.5)
                        ),
                        new HpLessTransition(0.5, "die"),
                        new PlayerWithinTransition(1.5, "die"),
                        new TimedTransition(3000, "die")
                    ),
                    new State("die",
                        new Flash(0xFFFFFF, 0.2, 5),
                        new TimedTransition(1000, "explode")
                        ),
                    new State("explode",
                        new Shoot(10, 8),
                        new Decay(100)
                        )
                    )
            )
            .Init("Imp of the Abyss",
                new State(
                    new Wander(0.2),
                    new Shoot(8, 5, 10, coolDown: 3200)
                    ),
                new ItemLoot("Magic Potion", 0.1),
                new ItemLoot("Health Potion", 0.1),
                new Threshold(0.5,
                    new ItemLoot("Cloak of the Red Agent", 0.01),
                    new ItemLoot("Felwasp Toxin", 0.01)
                    )
            )
            .Init("Demon of the Abyss",
                new State(
                    new Prioritize(
                        new Follow(0.4, 8, 5),
                        new Wander(0.25)
                        ),
                    new Shoot(8, 3, shootAngle: 10, coolDown: 5000)
                    ),
                new ItemLoot("Fire Bow", 0.05),
                new Threshold(0.5,
                    new ItemLoot("Mithril Armor", 0.01)
                    )
            )
            .Init("Demon Warrior of the Abyss",
                new State(
                    new Prioritize(
                        new Follow(0.5, 8, 5),
                        new Wander(0.25)
                        ),
                    new Shoot(8, 3, shootAngle: 10, coolDown: 3000)
                    ),
                new ItemLoot("Fire Sword", 0.025),
                new ItemLoot("Steel Shield", 0.025)
            )
            .Init("Demon Mage of the Abyss",
                new State(
                    new Prioritize(
                        new Follow(0.4, 8, 5),
                        new Wander(0.25)
                        ),
                    new Shoot(8, 3, shootAngle: 10, coolDown: 3400)
                    ),
                new ItemLoot("Fire Nova Spell", 0.02),
                new Threshold(0.1,
                    new ItemLoot("Wand of Dark Magic", 0.01),
                    new ItemLoot("Avenger Staff", 0.01),
                    new ItemLoot("Robe of the Invoker", 0.01),
                    new ItemLoot("Essence Tap Skull", 0.01),
                    new ItemLoot("Demonhunter Trap", 0.01)
                    )
            )
            .Init("Brute of the Abyss",
                new State(
                    new Prioritize(
                        new Follow(0.7, 8, 1),
                        new Wander(0.25)
                        ),
                    new Shoot(8, 3, shootAngle: 10, coolDown: 800)
                    ),
                new ItemLoot("Magic Potion", 0.1),
                new Threshold(0.1,
                    new ItemLoot("Obsidian Dagger", 0.02),
                    new ItemLoot("Steel Helm", 0.02)
                    )
            )
            .Init("Brute Warrior of the Abyss",
                new State(
                    new Prioritize(
                        new Follow(0.4, 8, 1),
                        new Wander(0.25)
                        ),
                    new Shoot(8, 3, shootAngle: 10, coolDown: 800)
                    ),
                new ItemLoot("Spirit Salve Tome", 0.02),
                new Threshold(0.5,
                    new ItemLoot("Glass Sword", 0.01),
                    new ItemLoot("Ring of Greater Dexterity", 0.01),
                    new ItemLoot("Magesteel Quiver", 0.01)
                    )
            )
            ;
    }
}