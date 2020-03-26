using wServer.logic.behaviors;
using wServer.logic.transitions;
using wServer.logic.loot;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ TheHive = () => Behav()
        .Init("TH Small Bees",
            new State(
                new Wander(0.5),
                new State("idle",
                    new Charge(0.5, 5, 2000)
                )
            )
        )
        .Init("TH Fat Bees",
            new State(
                new Wander(0.5),
                new State("idle",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new PlayerWithinTransition(10, "follow")
                    ),
                new State("follow",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new Shoot(10, 1, 0, 0, coolDown: 1200)
                    //new Follow(0.5, 10, 4)
                    ),
                new State("wander/follow",
                    new Shoot(10, 1, 0, 0, coolDown: 1200),
                    new Follow(0.5, 10, 4)
                    )
                ),
            new Threshold(0.05,
                new TierLoot(3, ItemType.Weapon, 0.5),
                new TierLoot(4, ItemType.Weapon, 0.3),
                new TierLoot(5, ItemType.Weapon, 0.1),
                new TierLoot(3, ItemType.Armor, 0.5),
                new TierLoot(4, ItemType.Armor, 0.3),
                new TierLoot(5, ItemType.Armor, 0.1)
                )
            )
        .Init("TH Red Fat Bees",
            new State(
                new Wander(0.5),
                new State("idle",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new PlayerWithinTransition(10, "follow")
                    ),
                new State("follow",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new Shoot(10, 1, 0, 0, coolDown: 800)
                    //new Follow(0.5, 10, 4)
                    ),
                new State("wander/follow",
                    new Shoot(10, 1, 0, 0, coolDown: 800),
                    new Follow(0.5, 10, 4)
                    )
                )
            )
        .Init("TH Mini Hive",
            new State(
                new State("lol"
                    )
                ),
            new Threshold(0.5,
                new ItemLoot("TH Honey bottle", 0.3)
                )
            )
        .Init("TH Fat Bees 2",
            new State(
                new Wander(0.5),
                new State("idle",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new PlayerWithinTransition(10, "follow")
                    ),
                new State("follow",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new Shoot(10, 1, 0, 0, coolDown: 1200)
                    //new Follow(0.5, 10, 4)
                    ),
                new State("wander/follow",
                    new Shoot(10, 1, 0, 0, coolDown: 1200),
                    new Follow(0.5, 10, 4)
                    )
                )
            )
        .Init("TH Red Fat Bees 2",
            new State(
                new Wander(0.5),
                new State("idle",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new PlayerWithinTransition(10, "follow")
                    ),
                new State("follow",
                    new Orbit(0.2, 2, 10, "TH Mini Hive"),
                    new EntityNotExistsTransition("TH Mini Hive", 5, "wander/follow"),
                    new Shoot(10, 1, 0, 0, coolDown: 800)
                    //new Follow(0.5, 10, 4)
                    ),
                new State("wander/follow",
                    new Shoot(10, 1, 0, 0, coolDown: 800),
                    new Follow(0.5, 10, 4)
                    )
                )
            )
        .Init("TH Queen Bee",
            new State(
                new RealmPortalDrop(),
                new Wander(0.5),
                new Follow(0.3, 10, 5),
                new State("idle",
                    new Shoot(10, 5, 72, 0, coolDown: 1000),
                    new Grenade(4, 50, 10, coolDown: 1500),
                    new TimedTransition(5000, "blink")
                    ),
                new State("blink",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0x00ff00, 2, 1),
                    new TimedTransition(3000, "idle2")
                    ),
                new State("idle2",
                    new Shoot(10, 3, 45, 1, coolDown: 1000),
                    new TimedTransition(5000, "blink2")
                    ),
                new State("blink2",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new Flash(0x00ff00, 2, 1),
                    new TimedTransition(3000, "idle")
                    )
                ),
            new Threshold(0.1,
                new ItemLoot("Potion of Dexterity", 0.8),
                new ItemLoot("Wine Cellar Incantation", 0.01)
                //new ItemLoot("Orb of Sweet Demise", 0.05),
                //new ItemLoot("HoneyScepter", 0.05)
                )
            )
        .Init("TH Maggot Egg1",
            new State(
                new State("idle",
                    new TransformOnDeath("TH Maggots")
                    )
                )
            )
        .Init("TH Maggot Egg2",
            new State(
                new State("idle",
                    new TransformOnDeath("TH Maggots")
                    )
                )
            )
        .Init("TH Maggot Egg3",
            new State(
                new State("idle",
                    new TransformOnDeath("TH Maggots")
                    )
                )
            )
        .Init("TH Maggots",
            new State(
                new Wander(0.2),
                new State("idle",
                    new Shoot(2, 1, 0, 0, coolDown: 800)
                    )
                )
        );
    }
}