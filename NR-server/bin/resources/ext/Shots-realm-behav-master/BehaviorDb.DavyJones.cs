﻿#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
using wServer.realm;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ DavyJones = () => Behav()
            .Init("Ghost Lanturn Off",
                    new State(
                        new State("Start",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new TimedTransition(50, "gogogo")
                        ),
                new State("gogogo",
                    new TransformOnDeath("Ghost Lanturn On")
                    )
                    )
            )
        .Init("Ghost Lanturn On",
                new State(
                    new State("idle",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TimedTransition(5000, "deactivate")
                        ),
                    new State("deactivate",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "shoot"),
                        new TimedTransition(10000, "gone")
                        ),
                    new State("shoot",
                        new Shoot(10, 6, coolDown: 9000001, coolDownOffset: 100),
                        new TimedTransition(100, "gone")
                        ),
                    new State("DeavyVauln",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                        ),
                    new State("gone",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new Transform("Ghost Lanturn Off")
                        )
                    )
            )
            .Init("GhostShip PurpleDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Purple Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip PurpleDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Purple Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("Lost Soul",
                new State(
                    new State("Default",
                        new Prioritize(
                            new Orbit(0.3, 3, 20, "Ghost of Roger"),
                            new Wander(0.1)
                            ),
                        new PlayerWithinTransition(4, "Default1")
                        ),
                    new State("Default1",
                       new Charge(0.5, 8, coolDown: 2000),
                       new TimedTransition(2200, "Blammo")
                        ),
                     new State("Blammo",
                       new Shoot(10, count: 6, projectileIndex: 0, coolDown: 2000),
                       new Suicide()
                    )
                )
            ).Init("Ghost of Roger",
                new State(
                    new State("spawn",
                        new Spawn("Lost Soul", 3, 1, 5000),
                        new TimedTransition(100, "Attack")
                    ),
                    new State("Attack",
                        new Shoot(13, 1, 0, 0, coolDown: 10),
                        new TimedTransition(20, "Attack2")
                    ),
                    new State("Attack2",
                        new Shoot(13, 1, 0, 0, coolDown: 10),
                        new TimedTransition(20, "Attack3")
                    ),
                    new State("Attack3",
                        new Shoot(13, 1, 0, 0, coolDown: 10),
                        new TimedTransition(20, "Wait")
                    ),
                    new State("Wait",
                        new TimedTransition(1000, "Attack")
                    )
                )
            )
            .Init("GhostShip GreenDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Green Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip GreenDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Green Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip YellowDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Yellow Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip YellowDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Yellow Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip RedDoor Rt",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Red Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("GhostShip RedDoor Lf",
                new State(
                    new State("Idle",
                        new EntityNotExistsTransition("Red Key", 500, "Cycle")

                    ),
                    new State("Cycle",
                        new PlayerWithinTransition(2, "Cycle2")

                    ),
                    new State("Cycle2",
                        new Decay(1000)
                    )
               //248, 305
               )
            )
            .Init("Purple Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Purple Key has been found!"),
                        new Decay(200)



                    )
                )
            )
            .Init("Red Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Red Key has been found!"),
                        new Decay(200)



                    )
                )
            )
            .Init("Green Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Green Key has been found!"),
                        new Decay(200)



                    )
                )
            )
            .Init("Yellow Key",
                new State(
                    new State("Idle",
                        new PlayerWithinTransition(1, "Cycle")

                    ),
                    new State("Cycle",
                        new Taunt(true, "Yellow Key has been found!"),
                        new Decay(200)



                    )
                )
            )

  .Init("Lil' Ghost Pirate",
                new State(
                    new ChangeSize(30, 120),
                    new Shoot(10, count: 1, projectileIndex: 0, coolDown: 2000),
                    new State("Default",
                        new Prioritize(
                            new Follow(0.6, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default1")
                        ),
                    new State("Default1",
                       new StayBack(0.2, 3),
                       new TimedTransition(1850, "Default")
                    )
                )
            )
                 .Init("Zombie Pirate Sr",
                new State(
                    new Shoot(10, count: 1, projectileIndex: 0, coolDown: 2000),
                    new State("Default",
                        new Prioritize(
                            new Follow(0.3, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default1")
                        ),
                    new State("Default1",
                       new ConditionalEffect(ConditionEffectIndex.Armored),
                       new Prioritize(
                            new Follow(0.3, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default")
                    )
                )
            )
           .Init("Zombie Pirate Jr",
                new State(
                    new Shoot(10, count: 1, projectileIndex: 0, coolDown: 2500),
                    new State("Default",
                        new Prioritize(
                            new Follow(0.4, 8, 1),
                            new Wander(0.1)
                            ),
                        new TimedTransition(2850, "Default1")
                        ),
                    new State("Default1",
                       new Swirl(0.2, 3),
                       new TimedTransition(1850, "Default")
                    )
                )
            )
        .Init("Captain Summoner",
                new State(
                    new State("Default",
                        new ConditionalEffect(ConditionEffectIndex.Invincible)
                        )
                )
            )
           .Init("GhostShip Rat",
                new State(
                    new State("Default",
                        new Shoot(10, count: 1, projectileIndex: 0, coolDown: 1750),
                        new Prioritize(
                            new Follow(0.55, 8, 1),
                            new Wander(0.1)
                            )
                        )
                )
            )
        .Init("Violent Spirit",
                new State(
                    new State("Default",
                        new ChangeSize(35, 120),
                        new Shoot(10, count: 3, projectileIndex: 0, coolDown: 1750),
                        new Prioritize(
                            new Follow(0.25, 8, 1),
                            new Wander(0.1)
                            )
                        )
                )
            )
           .Init("School of Ghostfish",
                new State(
                    new State("Default",
                        new Shoot(10, count: 3, shootAngle: 18, projectileIndex: 0, coolDown: 4000),
                        new Wander(0.35)
                        )
                )
            )
                    .Init("Davy Jones",
                new State(
                    new State("Startup",
                        new DropPortalOnDeath("Realm Portal", 100),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(50, "WaitforKeyYY")
                        ),
                        new State("WaitforKeyYY",
                        new EntityNotExistsTransition("Yellow Key", 9999, "Floating")
                        ),
                        // 100% HP
                        new State("Floating",
                        new ChangeSize(100, 100),
                        new SetAltTexture(1),
                        new Wander(.2),
                        new Shoot(10, 5, 10, 0, coolDown: 2000),
                        new Shoot(10, 1, 10, 1, coolDown: 4000),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                        ),
                        new State("Charge",
                        new SetAltTexture(3),
                        new Charge(1.0, 10f, 4000),
                        new Shoot(10, 5, 10, 0, coolDown: 2000),
                        new Shoot(10, 1, 10, 1, coolDown: 4000),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(4000, "Floating")
                        ),
                        new State("CheckOffLanterns",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable")
                        ),
                        new State("Vunerable",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "DeavyVauln"),
                        new TimedTransition(14000, "deactivate"),
                        new HpLessTransition(0.6, "deactivate2")
                        ),
                        new State("deactivate",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "shoot"),
                        new TimedTransition(160, "Restart")
                        ),
                        new State("Restart",
                        new Order(100, "Ghost Lanturn On", "gone"),
                        new EntityNotExistsTransition("Ghost Lanturn On", 10, "Charge")
                        ),
                        // 60% HP
                        new State("Floating2",
                        new StayCloseToSpawn(.1, 3),
                        new ChangeSize(100, 100),
                        new SetAltTexture(1),
                        new Wander(.2),
                        new Shoot(10, 7, 10, 0, coolDown: 2000),
                        new Shoot(10, 2, 10, 1, coolDown: 4000),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable2"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                        ),
                        new State("Charge2",
                        new SetAltTexture(3),
                        new Charge(1.0, 10f, 4000),
                        new Shoot(10, 7, 10, 0, coolDown: 2000),
                        new Shoot(10, predictive: 0.5, shootAngle: 45, projectileIndex: 1, coolDown: 4000),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(4000, "Floating2")
                        ),
                        new State("CheckOffLanterns2",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable2")
                        ),
                        new State("Vunerable2",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "DeavyVauln"),
                        new HpLessTransition(0.3, "deactivate3"),
                        new TimedTransition(14000, "deactivate2")
                        ),
                        new State("deactivate2",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "shoot"),
                        new TimedTransition(160, "Restart2")
                        ),
                        new State("Restart2",
                        new Order(100, "Ghost Lanturn On", "gone"),
                        new EntityNotExistsTransition("Ghost Lanturn On", 10, "Charge2")
                        ),
                        // 30% HP
                        new State("Floating3",
                        new StayCloseToSpawn(.1, 3),
                        new ChangeSize(100, 100),
                        new SetAltTexture(1),
                        new Wander(.2),
                        new StayCloseToSpawn(.1, 3),
                        new Shoot(10, 9, 10, 0, coolDown: 2000),
                        new Shoot(10, 4, 10, 1, coolDown: 4000),
                        new TossObject("Zombie Pirate Sr", 7, 0, coolDown: 10000, randomToss: true),
                        new TossObject("Zombie Pirate Sr", 7, 0, coolDown: 10000, randomToss: true),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable3"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                        ),
                        new State("Charge3",
                        new SetAltTexture(3),
                        new Charge(1.0, 10f, 4000),
                        new Shoot(10, 9, 10, 0, coolDown: 2000),
                        new Shoot(10, predictive: 0.5, shootAngle: 90, projectileIndex: 1, coolDown: 4000),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(4000, "Floating3")
                        ),
                        new State("CheckOffLanterns3",
                        new SetAltTexture(3),
                        new StayCloseToSpawn(.1, 3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable3")
                        ),
                        new State("Vunerable3",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "DeavyVauln"),
                        new HpLessTransition(0.3, "deactivate4"),
                        new TimedTransition(14000, "deactivate3")
                        ),
                        new State("deactivate3",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "shoot"),
                        new TimedTransition(160, "Restart3")
                        ),
                        new State("Restart3",
                        new Order(100, "Ghost Lanturn On", "gone"),
                        new EntityNotExistsTransition("Ghost Lanturn On", 10, "Charge3")
                        ),
                        // 10% HP
                        new State("Floating4",
                        new ChangeSize(100, 100),
                        new SetAltTexture(1),
                        new Wander(.2),
                        new StayCloseToSpawn(.1, 0),
                        new Shoot(10, 12, 10, 0, coolDown: 2000),
                        new Shoot(10, predictive: 0.5, shootAngle: 90, projectileIndex: 1, coolDown: 3000),
                        new TossObject("Zombie Pirate Sr", 7, 0, coolDown: 10000, randomToss: true),
                        new TossObject("Zombie Pirate Sr", 7, 0, coolDown: 10000, randomToss: true),
                        new TossObject("Zombie Pirate Sr", 7, 0, coolDown: 10000, randomToss: true),
                        new TossObject("Zombie Pirate Sr", 7, 0, coolDown: 10000, randomToss: true),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable4"),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable)
                        ),
                        new State("Charge4",
                        new SetAltTexture(3),
                        new Charge(1.0, 10f, 4000),
                        new Shoot(10, 12, 10, 0, coolDown: 2000),
                        new Shoot(10, predictive: 0.5, shootAngle: 90, projectileIndex: 1, coolDown: 3000),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable, true),
                        new TimedTransition(4000, "Floating4")
                        ),
                        new State("CheckOffLanterns4",
                        new SetAltTexture(3),
                        new StayCloseToSpawn(.1, 3),
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new EntityNotExistsTransition("Ghost Lanturn Off", 10, "Vunerable4")
                        ),
                        new State("Vunerable4",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "DeavyVauln"),
                        new TimedTransition(14000, "deactivate4")
                        ),
                        new State("deactivate4",
                        new SetAltTexture(2),
                        new StayCloseToSpawn(.1, 0),
                        new Order(100, "Ghost Lanturn On", "shoot"),
                        new TimedTransition(160, "Restart4")
                        ),
                        new State("Restart4",
                        new Order(100, "Ghost Lanturn On", "gone"),
                        new EntityNotExistsTransition("Ghost Lanturn On", 10, "Charge4")
                        )
                    ),
                 new MostDamagers(2,
                    new ItemLoot("Ghostly Prism", 0.02),
                    new ItemLoot("Spirit Dagger", 0.025),
                    new ItemLoot("Wine Cellar Incantation", 0.02)
                    ),
                    new MostDamagers(3,
                    new ItemLoot("Potion of Wisdom", 1),
                    new ItemLoot("Potion of Attack", 0.5),
                    new ItemLoot("Captain's Ring", 0.05),
                    new ItemLoot("Spectral Cloth Armor", 0.022)
                    ),
                    new MostDamagers(5,
                    new ItemLoot("Ruby Gemstone", 0.01),
                    new ItemLoot("Golden Chalice", 0.015),
                    new ItemLoot("Pearl Necklace", 0.020)
                    ),
                    new Threshold(0.15,
                    new TierLoot(8, ItemType.Weapon, 0.4),
                    new TierLoot(9, ItemType.Weapon, 0.06),
                    new TierLoot(3, ItemType.Ring, 0.6),
                    new TierLoot(7, ItemType.Armor, 0.7),
                    new TierLoot(8, ItemType.Armor, 0.6),
                    new TierLoot(4, ItemType.Ability, 0.4)
                    ),
                    new Threshold(0.10,
                    new TierLoot(10, ItemType.Weapon, 0.03),
                    new TierLoot(11, ItemType.Weapon, 0.02),
                    new TierLoot(4, ItemType.Ring, 0.04),
                    new TierLoot(9, ItemType.Armor, 0.07),
                    new TierLoot(10, ItemType.Armor, 0.06),
                    new TierLoot(5, ItemType.Ability, 0.06)
                    ),
                    new Threshold(0.07,
                    new TierLoot(5, ItemType.Ring, 0.01),
                    new TierLoot(11, ItemType.Armor, 0.05)
                    )
                 );
    }
}