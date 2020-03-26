using wServer.logic.behaviors;
using wServer.logic.transitions;
using wServer.logic.loot;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ ManorOfTheImmortals = () => Behav()
            .Init("Lord Ruthven",
                new State(
                    new DropPortalOnDeath("Realm Portal", 100),
                    new State("1",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new PlayerWithinTransition(12, "2")
                        ),
                    new State("2",
                        new HpLessTransition(0.75, "3"),
                        new Prioritize(
                            new Wander(0.1)
                            ),
                        new Shoot(10, 3, 10, 0, coolDown: 500)
                        ),
                    new State("3",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new SetAltTexture(2),
                        new State("4",
                            new Shoot(99, 24, 15, 1, 0, 0, coolDown: 999999, coolDownOffset: 2000),
                            new Shoot(99, 24, 15, 1, 0, 7, coolDown: 999999, coolDownOffset: 2650),
                            new TimedTransition(3400, "5")
                            ),
                        new State("5",
                            new TossObject("Coffin Creature", 10, 0, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 90, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 180, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 270, coolDown: 999999),
                            new TimedTransition(1500, "6")
                            )
                        ),
                    new State("6",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new SetAltTexture(1),
                        new Spawn("Vampire Bat Swarmer 1", 10, 0),
                        new State("9",
                            new Prioritize(
                                new Follow(1, 10, 0)
                            ),
                            new Reproduce("Vampire Bat Swarmer 1", 99, 20, 0, 100),
                            new TimedTransition(10000, "10")
                            ),
                        new State("10",
                            new ReturnToSpawn(true, 1),
                            new Reproduce("Vampire Bat Swarmer 1", 99, 20, 0, 100),
                            new TimedTransition(7000, "11")
                            ),
                        new State("11",
                            new SetAltTexture(0),
                            new Aoe(2, true, 55, 110, false, 0xFFFFFF),
                            new OrderOnce(999, "Vampire Bat Swarmer 1", "die"),
                            new TimedTransition(2000, "12")
                            )
                        ),
                    new State("12",
                        new HpLessTransition(0.5, "13"),
                        new Prioritize(
                            new Wander(0.1)
                            ),
                        new Shoot(10, 5, 10, 0, coolDown: 500)
                        ),
                    new State("13",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new SetAltTexture(2),
                        new State("14",
                            new Shoot(99, 24, 15, 1, 0, 0, coolDown: 999999, coolDownOffset: 2000),
                            new Shoot(99, 24, 15, 1, 0, 7, coolDown: 999999, coolDownOffset: 2650),
                            new Shoot(99, 24, 15, 1, 0, 0, coolDown: 999999, coolDownOffset: 3300),
                            new TimedTransition(4050, "15")
                            ),
                        new State("15",
                            new TossObject("Coffin Creature", 10, 0, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 90, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 180, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 270, coolDown: 999999),
                            new TimedTransition(1500, "16")
                            )
                        ),
                    new State("16",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new SetAltTexture(1),
                        new Spawn("Vampire Bat Swarmer 1", 10, 0),
                        new State("17",
                            new Prioritize(
                                new Follow(1, 10, 0)
                            ),
                            new Reproduce("Vampire Bat Swarmer 1", 99, 20, 0, 100),
                            new TimedTransition(10000, "18")
                            ),
                        new State("18",
                            new ReturnToSpawn(true, 1),
                            new Reproduce("Vampire Bat Swarmer 1", 99, 20, 0, 100),
                            new TimedTransition(7000, "19")
                            ),
                        new State("19",
                            new SetAltTexture(0),
                            new Aoe(2, true, 55, 110, false, 0xFFFFFF),
                            new OrderOnce(999, "Vampire Bat Swarmer 1", "die"),
                            new TimedTransition(2000, "20")
                            )
                        ),
                    new State("20",
                        new HpLessTransition(0.25, "21"),
                        new Prioritize(
                            new Wander(0.1)
                            ),
                        new Shoot(10, 9, 10, 0, coolDown: 500)
                        ),
                    new State("21",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new SetAltTexture(2),
                        new State("22",
                            new Shoot(99, 24, 15, 1, 0, 0, coolDown: 999999, coolDownOffset: 2000),
                            new Shoot(99, 24, 15, 1, 0, 7, coolDown: 999999, coolDownOffset: 2650),
                            new Shoot(99, 24, 15, 1, 0, 0, coolDown: 999999, coolDownOffset: 3300),
                            new Shoot(99, 24, 15, 1, 0, 7, coolDown: 999999, coolDownOffset: 3950),
                            new TimedTransition(4700, "23")
                            ),
                        new State("23",
                            new TossObject("Coffin Creature", 10, 0, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 90, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 180, coolDown: 999999),
                            new TossObject("Coffin Creature", 10, 270, coolDown: 999999),
                            new TimedTransition(1500, "24")
                            )
                        ),
                    new State("24",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new SetAltTexture(1),
                        new Spawn("Vampire Bat Swarmer 1", 10, 0),
                        new State("25",
                            new Prioritize(
                                new Follow(1, 10, 0)
                            ),
                            new Reproduce("Vampire Bat Swarmer 1", 99, 20, 0, 100),
                            new TimedTransition(10000, "26")
                            ),
                        new State("26",
                            new ReturnToSpawn(true, 1),
                            new Reproduce("Vampire Bat Swarmer 1", 99, 20, 0, 100),
                            new TimedTransition(7000, "27")
                            ),
                        new State("27",
                            new SetAltTexture(0),
                            new Aoe(2, true, 55, 110, false, 0xFFFFFF),
                            new OrderOnce(999, "Vampire Bat Swarmer 1", "die"),
                            new TimedTransition(2000, "28")
                            )
                        ),
                    new State("28",
                        new Prioritize(
                            new Wander(0.1)
                            ),
                        new Shoot(10, 18, 10, 0, coolDown: 500)
                        )
                    ),
            new MostDamagers(3,
                new ItemLoot("Potion of Attack", 1)
                ),
            new Threshold(0.01,
                new TierLoot(8, ItemType.Weapon, 0.12),
                new TierLoot(9, ItemType.Weapon, 0.10),
                new TierLoot(10, ItemType.Weapon, 0.08),
                new TierLoot(8, ItemType.Armor, 0.14),
                new TierLoot(9, ItemType.Armor, 0.12),
                new TierLoot(4, ItemType.Ability, 0.10),
                new TierLoot(5, ItemType.Ability, 0.08),
                new TierLoot(4, ItemType.Ring, 0.07),
                new ItemLoot("Holy Water", 0.2),
                new ItemLoot("Holy Water", 0.2),
                new ItemLoot("Holy Water", 0.2),
                new ItemLoot("Holy Water", 0.2),
                new ItemLoot("St. Abraham's Wand", 0.04),
                new ItemLoot("Chasuble of Holy Light", 0.04),
                new ItemLoot("Ring of Divine Faith", 0.04),
                new ItemLoot("Bone Dagger", 0.04),
                new ItemLoot("Holy Cross", 0.03),
                new ItemLoot("Golden Candelabra", 0.03),
                new ItemLoot("Wine Cellar Incantation", 0.05),
                new ItemLoot("Tome of Purification", 0.01)
                )
            )
        .Init("Coffin Creature",
            new State(
                new TransformOnDeath("Lil Feratu", 1, 4),
                new State("1",
                    new TimedTransition(1000, "2")
                    ),
                new State("2",
                    new Shoot(10, 1, coolDown: 200),
                    new TimedTransition(3000, "3")
                    ),
                new State("3",
                    new TimedTransition(600, "1")
                    )
                )
            )
        .Init("Vampire Bat Swarmer 1",
            new State(
                new State("1",
                    new Wander(0.4),
                    new Protect(2, "Lord Ruthven", 999, 2, 1),
                    new Protect(2, "Lord Ruthven Deux", 999, 2, 1),
                    new Shoot(10, 1, coolDown: 500)
                    ),
                new State("die",
                    new Suicide()
                    )
                )
            )
        .Init("Vampire Bat Swarmer",
            new State(
                new Wander(0.4),
                new Prioritize(
                    new Follow(1.5, 8, 1)
                ),
                new Shoot(10, 1, coolDown: 500)
                    )
            )
        .Init("Nosferatu",
            new State(
                new Prioritize(
                    new Follow(0.1, 8, 1)
                ),
                new Shoot(10, count: 5, shootAngle: 8, projectileIndex: 1, coolDown: 1000),
                new Shoot(9, count: 3, shootAngle: 35, projectileIndex: 0, coolDown: 1500)
                    ),
            new Threshold(0.01,
                new ItemLoot("Bone Dagger", 0.01),
                new ItemLoot("Magic Potion", 0.05),
                new TierLoot(6, ItemType.Weapon, 0.09),
                new TierLoot(7, ItemType.Weapon, 0.07),
                new TierLoot(8, ItemType.Weapon, 0.05),
                new TierLoot(6, ItemType.Armor, 0.09),
                new TierLoot(7, ItemType.Armor, 0.07),
                new TierLoot(8, ItemType.Armor, 0.05),
                new TierLoot(3, ItemType.Ability, 0.07),
                new TierLoot(4, ItemType.Ability, 0.05),
                new TierLoot(3, ItemType.Ring, 0.07),
                new TierLoot(4, ItemType.Ring, 0.05)
                )
            )
        .Init("Armor Guard",
            new State(
                new ConditionalEffect(ConditionEffectIndex.Armored),
                new Wander(0.1),
                new TossObject("RockBomb", 7, coolDown: 4500),
                new Shoot(12, 2, 10, coolDown: 3500),
                new State("1",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 0, coolDown: 999999),
                    new TimedTransition(500, "2")
                    ),
                new State("2",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 45, coolDown: 999999),
                    new TimedTransition(500, "3")
                    ),
                new State("3",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 90, coolDown: 999999),
                    new TimedTransition(500, "4")
                    ),
                new State("4",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 135, coolDown: 999999),
                    new TimedTransition(500, "5")
                    ),
                new State("5",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 180, coolDown: 999999),
                    new TimedTransition(500, "6")
                    ),
                new State("6",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 225, coolDown: 999999),
                    new TimedTransition(500, "7")
                    ),
                new State("7",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 270, coolDown: 999999),
                    new TimedTransition(500, "8")
                    ),
                new State("8",
                    new Shoot(99, 4, 10, projectileIndex: 1, fixedAngle: 0, angleOffset: 315, coolDown: 999999),
                    new TimedTransition(500, "9")
                    ),
                new State("9",
                    new TimedTransition(2000, "1")
                    )
                ),
            new Threshold(0.01,
                new ItemLoot("Health Potion", 0.05),
                new TierLoot(6, ItemType.Weapon, 0.09),
                new TierLoot(7, ItemType.Weapon, 0.07),
                new TierLoot(8, ItemType.Weapon, 0.05),
                new TierLoot(6, ItemType.Armor, 0.09),
                new TierLoot(7, ItemType.Armor, 0.07),
                new TierLoot(8, ItemType.Armor, 0.05),
                new TierLoot(3, ItemType.Ability, 0.07),
                new TierLoot(4, ItemType.Ability, 0.05),
                new TierLoot(3, ItemType.Ring, 0.07),
                new TierLoot(4, ItemType.Ring, 0.05)
                )
            )
        .Init("RockBomb",
            new State(
                new State("1",
                    new TimedTransition(2000, "2")
                    ),
                new State("2",
                    new Shoot(99, 8, 45, fixedAngle: 0),
                    new Suicide()
                    )
                )
            )
        .Init("Hellhound",
            new State(
                new Shoot(10, 5, 10, coolDown: 1500),
                new Charge(1.5, 10, 3600),
                new Prioritize(
                    new Wander(0.25)
                    )
                ),
            new Threshold(0.01,
                new ItemLoot("Health Potion", 0.05),
                new TierLoot(6, ItemType.Weapon, 0.09),
                new TierLoot(6, ItemType.Armor, 0.09),
                new TierLoot(3, ItemType.Ability, 0.07),
                new TierLoot(3, ItemType.Ring, 0.07)
                )
            )
        .Init("Lesser Bald Vampire",
            new State(
                new Shoot(10, 3, 6, coolDown: 1000),
                new Prioritize(
                    new Follow(0.35, 8, 1)
                    )
                ),
            new Threshold(0.01,
                new ItemLoot("Health Potion", 0.05)
                )
            )
        .Init("Coffin",
            new State(
                new State("1",
                    new DamageTakenTransition(1, "2")
                    ),
                new State("2",
                    new TimedRandomTransition(0, false, "3", "4")
                    ),
                new State("3",
                    new SetLootState("1"),
                    new Spawn("Vampire Bat Swarmer", 20, 0),
                    new Spawn("Nosferatu", 1, 0)
                    ),
                new State("4",
                    new SetLootState("2"),
                    new Transform("Coffin Creature")
                    )
                ),
            new LootState("1",
                new Threshold(0.01,
                    new ItemLoot("Potion of Attack", 0.1),
                    new TierLoot(8, ItemType.Weapon, 0.12),
                    new TierLoot(9, ItemType.Weapon, 0.10),
                    new TierLoot(10, ItemType.Weapon, 0.08),
                    new TierLoot(8, ItemType.Armor, 0.14),
                    new TierLoot(9, ItemType.Armor, 0.12),
                    new TierLoot(4, ItemType.Ability, 0.10),
                    new TierLoot(5, ItemType.Ability, 0.08),
                    new TierLoot(4, ItemType.Ring, 0.07),
                    new ItemLoot("Holy Water", 0.2),
                    new ItemLoot("Holy Water", 0.2),
                    new ItemLoot("Holy Water", 0.2),
                    new ItemLoot("Holy Water", 0.2),
                    new ItemLoot("St. Abraham's Wand", 0.04),
                    new ItemLoot("Chasuble of Holy Light", 0.04),
                    new ItemLoot("Ring of Divine Faith", 0.04),
                    new ItemLoot("Bone Dagger", 0.04),
                    new ItemLoot("Holy Cross", 0.03),
                    new ItemLoot("Golden Candelabra", 0.03),
                    new ItemLoot("Wine Cellar Incantation", 0.05),
                    new ItemLoot("Tome of Purification", 0.01)
                    ),
                new LootState("2")
                )
            )
        .Init("Lil Feratu",
            new State(
                new Prioritize(
                    new Follow(0.35, 8, 1)
                    ),
                new Shoot(9, 5, 6, coolDown: 1000)
                )
            )
        ;
    }
}
