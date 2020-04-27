﻿using common.resources;
using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ CrawlingDepths = () => Behav()
                        /*  .Init("Son of Arachna",
                              new State(
                                  new RealmPortalDrop(),
                                  new State("default",
                                      new PlayerWithinTransition(7.2, "fight")
                                      ),
                                  new State("fight",
                                  new If(
                                       new EntityCountGreaterThan("Yellow Son of Arachna Giant Egg Sac", 9999, 0),
                                       new Shoot(25, projectileIndex: 0, count: 8, coolDown: 3000, coolDownOffset: 4000)
                                      )
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
                                      new Shoot(10, projectileIndex: 2, predictive: 1, coolDown: 2000),
                                      new Shoot(10, projectileIndex: 2, predictive: 1, coolDownOffset: 300, coolDown: 2000),
                                      new Shoot(10, 3, projectileIndex: 3, predictive: 1, coolDownOffset: 100, coolDown: 2000),
                                      new Shoot(10, 3, projectileIndex: 3, predictive: 1, coolDownOffset: 400, coolDown: 2000),
                                      new TimedTransition(10000, "normalize")
                                      ),
                                  new State("normalize",
                                      new Wander(0.3),
                                      new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                                      new ChangeSize(-20, 100),
                                      new TimedTransition(1000, "basic")
                                      )
                                  ),
                              new Threshold(0.03,
                                  new TierLoot(10, ItemType.Weapon, 0.06),
                                  new TierLoot(11, ItemType.Weapon, 0.05),
                                  new TierLoot(12, ItemType.Weapon, 0.04),
                                  new TierLoot(5, ItemType.Ability, 0.06),
                                  new TierLoot(6, ItemType.Ability, 0.04),
                                  new TierLoot(11, ItemType.Armor, 0.06),
                                  new TierLoot(12, ItemType.Armor, 0.05),
                                  new TierLoot(13, ItemType.Armor, 0.04),
                                  new TierLoot(5, ItemType.Ring, 0.05),
                                  new ItemLoot("Potion of Mana", 1),
                                  new ItemLoot("Doku No Ken", 0.01)
                                  )
                          )*/
                        .Init("Son of Arachna",
                new State(
                    new ScaleHP(12000, 0),
                    new RealmPortalDrop(),
                    new State("Idle",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new PlayerWithinTransition(9, "MakeWeb")
                    ),
                    new State("MakeWeb",
                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                        new TossObject("Epic Arachna Web Spoke 1", range: 10, angle: 0, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 7", range: 6, angle: 0, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 2", range: 10, angle: 60, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 3", range: 10, angle: 120, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 8", range: 6, angle: 120, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 4", range: 10, angle: 180, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 5", range: 10, angle: 240, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 9", range: 6, angle: 240, coolDown: 100000),
                        new TossObject("Epic Arachna Web Spoke 6", range: 10, angle: 300, coolDown: 100000),
                        new TimedTransition(3500, "Attack")
                        ),
                    new State("Attack",
                        new Shoot(1, projectileIndex: 0, count: 8, coolDown: 2200, shootAngle: 45, fixedAngle: 0),
                        new Shoot(10, projectileIndex: 1, coolDown: 3000),
                        new State("Follow",
                            new Prioritize(
                                new StayAbove(.6, 1),
                                new StayBack(.6, distance: 8),
                                new Wander(.7)
                                ),
                            new TimedTransition(1500, "Return")
                                ),
                        new State("Return",
                            new StayCloseToSpawn(.4, 1),
                            new TimedTransition(2500, "Follow")
                            ))
                        ),
                        new Threshold(0.03,
                                  new TierLoot(10, ItemType.Weapon, 0.06),
                                  new TierLoot(11, ItemType.Weapon, 0.05),
                                  new TierLoot(12, ItemType.Weapon, 0.04),
                                  new TierLoot(5, ItemType.Ability, 0.06),
                                  new TierLoot(6, ItemType.Ability, 0.04),
                                  new TierLoot(11, ItemType.Armor, 0.06),
                                  new TierLoot(12, ItemType.Armor, 0.05),
                                  new TierLoot(13, ItemType.Armor, 0.04),
                                  new TierLoot(5, ItemType.Ring, 0.05),
                                  new ItemLoot("Potion of Mana", 1),
                                  new ItemLoot("Doku No Ken", 0.014)
                                  )
                        )
           .Init("Crawling Depths Egg Sac",
                new State(
                    new State("CheckOrDeath",
                    new PlayerWithinTransition(2, "Urclose"),
                    new TransformOnDeath("Crawling Spider Hatchling", 5, 7)
                    ),
                new State("Urclose",
                    new Spawn("Crawling Spider Hatchling", 6),
                    new Suicide()
            )))
         .Init("Crawling Spider Hatchling",
                new State(
                    new Prioritize(
                        new Wander(.4)
                    ),
                    new Shoot(7, count: 1, shootAngle: 0, coolDown: 650),
                    new Shoot(7, count: 1, shootAngle: 0, projectileIndex: 1, predictive: 1, coolDown: 850)
                )
            )
                 .Init("Crawling Grey Spotted Spider",
                new State(
                    new Prioritize(
                        new Charge(2, 8, 1050),
                        new Wander(.4)
                    ),
                    new Shoot(10, count: 1, shootAngle: 0, coolDown: 500)
                ),
                new ItemLoot("Healing Ichor", 0.2),
                new ItemLoot("Health Potion", 0.3)
            )
          .Init("Crawling Grey Spider",
                new State(
                    new Prioritize(
                        new Charge(2, 8, 1050),
                        new Wander(.4)
                    ),
                    new Shoot(9, count: 1, shootAngle: 0, coolDown: 850)
                ),
                new ItemLoot("Healing Ichor", 0.2),
                new ItemLoot("Health Potion", 0.3)
            )
        .Init("Crawling Red Spotted Spider",
                new State(
                    new Prioritize(
                        new Wander(.4)
                    ),
                    new Shoot(8, count: 1, shootAngle: 0, coolDown: 750)
                ),
                new ItemLoot("Healing Ichor", 0.2),
                new ItemLoot("Health Potion", 0.3)
            )
         .Init("Crawling Green Spider",
                new State(
                    new Prioritize(
                        new Follow(.6, 11, 1),
                        new Wander(.4)
                    ),
                    new Shoot(8, count: 3, shootAngle: 10, coolDown: 400)
                ),
                new ItemLoot("Healing Ichor", 0.2),
                new ItemLoot("Health Potion", 0.3)
            )
         .Init("Yellow Son of Arachna Giant Egg Sac",
                new State(
                    new TransformOnDeath("Yellow Egg Summoner"),
                new State("Spawn",
                    new Spawn("Crawling Green Spider", 2),
                    new EntityNotExistsTransition("Crawling Green Spider", 20, "Spawn2")
                    ),
                new State("Spawn2",
                    new Spawn("Crawling Grey Spider", 2),
                    new EntityNotExistsTransition("Crawling Grey Spider", 20, "Spawn3")
                    ),
                new State("Spawn3",
                    new Spawn("Crawling Red Spotted Spider", 2),
                    new EntityNotExistsTransition("Crawling Red Spotted Spider", 20, "Spawn4")
                    ),
                 new State("Spawn4",
                    new Spawn("Crawling Spider Hatchling", 2),
                    new EntityNotExistsTransition("Crawling Spider Hatchling", 20, "Spawn5")
                     ),
                 new State("Spawn5",
                    new Spawn("Crawling Grey Spotted Spider", 2),
                    new EntityNotExistsTransition("Crawling Grey Spotted Spider", 20, "Spawn")
            )),
                new Threshold(0.15,
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(12, ItemType.Armor, 0.1),
                new ItemLoot("Doku No Ken", 0.015)
                    )
            )
         .Init("Blue Son of Arachna Giant Egg Sac",
                new State(
                    new State("DeathSpawn",
                    new TransformOnDeath("Crawling Spider Hatchling", 5, 7)

            )),
                new Threshold(0.15,
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(12, ItemType.Armor, 0.1),
                new ItemLoot("Doku No Ken", 0.04)
                    ))
         .Init("Red Son of Arachna Giant Egg Sac",
                new State(
                    new State("DeathSpawn",
                    new TransformOnDeath("Crawling Red Spotted Spider", 3, 3)

            )),
                new Threshold(0.15,
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(12, ItemType.Armor, 0.1),
                new ItemLoot("Doku No Ken", 0.015)
                    ))
         .Init("Silver Son of Arachna Giant Egg Sac",
                new State(
                    new State("DeathSpawn",
                    new TransformOnDeath("Crawling Grey Spider", 3, 3)

            )),
                new Threshold(0.15,
                new TierLoot(11, ItemType.Weapon, 0.1),
                new TierLoot(12, ItemType.Armor, 0.1),
                new ItemLoot("Doku No Ken", 0.015)
                    ))
         .Init("Silver Egg Summoner",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    )
            )
         .Init("Yellow Egg Summoner",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    )
            )
         .Init("Red Egg Summoner",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    )
            )
         .Init("Blue Egg Summoner",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invincible)
                    )
            )
         .Init("Epic Arachna Web Spoke 1",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 180, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 120, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 240, coolDown: 150)
                    )
            )
           .Init("Epic Arachna Web Spoke 2",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 240, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 180, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 300, coolDown: 150)
                    )
            )
           .Init("Epic Arachna Web Spoke 3",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 300, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 240, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 0, coolDown: 150)
                    )
            )
           .Init("Epic Arachna Web Spoke 4",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 0, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 60, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 300, coolDown: 150)
                    )
            )
           .Init("Epic Arachna Web Spoke 5",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 60, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 0, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 120, coolDown: 150)
     )
            )
           .Init("Epic Arachna Web Spoke 6",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 120, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 60, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 180, coolDown: 150)
                    )
            )
           .Init("Epic Arachna Web Spoke 7",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 180, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 120, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 240, coolDown: 150)
                    )
            )
           .Init("Epic Arachna Web Spoke 8",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 360, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 240, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 300, coolDown: 150)
                    )
            )
           .Init("Epic Arachna Web Spoke 9",
                new State(
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                new Shoot(200, count: 1, fixedAngle: 0, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 60, coolDown: 150),
                new Shoot(200, count: 1, fixedAngle: 120, coolDown: 150)
                    )
            );
    }
}