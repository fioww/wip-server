#region

using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
using wServer.realm;

#endregion

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ MoltenLab = () => Behav()
               .Init("Archangel Alanus",
                   new State(
                       new State("Startup",
                        new ConditionalEffect(ConditionEffectIndex.StunImmune, true),
                        new ConditionalEffect(ConditionEffectIndex.StasisImmune, true),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new TimedTransition(100, "CPillars")
                        ),
                        new State("CPillars",
                        new EntitiesNotExistsTransition(9999, "AllDead", "ml Activator Titanum")
                        ),
                        new State("AllDead",
                        new PlayerWithinTransition(7, "Startup")
                        ),
                        new State("Startup",
                        new Taunt(true, "{PLAYER}, you come to my domain to challenge us?"),
                        new TimedTransition(4000, "LastWords")
                        ),
                        new State("LastWords",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt(true, "Bring it then, pray god is on your side."),
                        new Flash(0x000000, 1, 9000001),
                        new TimedTransition(5000, "Begin1/3")
                        ),
                        new State("Begin1/3", // PI0 = GGebShot / PI1 = GGebShot MOREDAMAGE / PI2 = GGebShot MOREDAMAGEX2
                        new Order(100, "ML SupportTower", "Grenade"),
                        new Wander(0.4),
                        new Shoot(15, count: 3, projectileIndex: 0, predictive: 1, shootAngle: 25, coolDown: 300, coolDownOffset: 100),
                        new Shoot(15, count: 2, projectileIndex: 1, predictive: 1, shootAngle: 35, coolDown: 460, coolDownOffset: 200),
                        new Shoot(15, count: 1, projectileIndex: 2, predictive: 1, coolDown: 510, coolDownOffset: 300),
                        new Grenade(4, 120, 8, coolDown: 1500),
                        new HpLessTransition(0.60, "Hurt60%"),
                        new TimedTransition(7000, "Begin2/3")
                        ),
                        new State("Begin2/3", // PI3 = FireStorm
                        new Order(100, "ML SupportTower", "Snipe"),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Orbit(0.4, 5, 20, "ML SupportTower"),
                        new Shoot(15, count: 8, projectileIndex: 3, shootAngle: 45, coolDown: 250),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 0, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 90, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 180, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 270, coolDown: 1),
                        new HpLessTransition(0.60, "Hurt60%"),
                        new TimedTransition(7000, "Begin3/3")
                        ),
                        new State("Begin3/3", // PI4 Boomerang
                        new Order(100, "ML SupportTower", "Wait"),
                        new Wander(0.3),
                        new Shoot(15, count: 6, projectileIndex: 4, shootAngle: 10, predictive: 1, coolDown: 1000),
                        new Shoot(15, count: 3, projectileIndex: 0, predictive: 1, shootAngle: 25, coolDown: 300, coolDownOffset: 100),
                        new Grenade(4, 40, 8, coolDown: 100),
                        new HpLessTransition(0.60, "Hurt60%"),
                        new TimedTransition(7000, "Begin1/3")
                        ),
                        new State("Hurt60%",
                        new Order(100, "ML SupportTower", "Wait"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt(true, "You pests can hit hard.."),
                        new TimedTransition(5000, "Hurt60%2")
                        ),
                        new State("Hurt60%2",
                        new Taunt(true, "Don't test me mortals."),
                        new TimedTransition(5000, "Starmode60%")
                        ),
                        new State("Starmode60%",
                        new Order(100, "ML SupportTower", "BlastAOE"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Flash(0xe60000, 1, 9000001),
                        new Orbit(0.4, 5, 20, "ML SupportTower"),
                        new Shoot(15, count: 8, projectileIndex: 3, shootAngle: 45, coolDown: 110),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 0, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 90, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 180, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 270, coolDown: 1),
                        new Shoot(15, count: 8, shootAngle: 45, projectileIndex: 5, coolDown: 200),
                        new TimedTransition(10000, "Starmode60%Break")
                        ),
                        new State("Starmode60%Break",
                        new Order(100, "ML SupportTower", "Wait"),
                        new Taunt(true, "Hah...ha..."),
                        new TimedTransition(5000, "Starmode60%"),
                        new HpLessTransition(0.50, "EndPrep")
                        ),
                        new State("EndPrep",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt(true, "You still live?!"),
                        new TimedTransition(4000, "End1/4")
                        ),
                        new State("End1/4", // PI5 = Confuse / PI6 = Nothing / PI7 = Nothing
                        new Order(100, "ML SupportTower", "Wait"),
                        new Orbit(0.4, 5, 20, "ML SupportTower"),
                        new Shoot(15, count: 4, shootAngle: 45, projectileIndex: 5, coolDown: 200),
                        new Shoot(15, count: 2, shootAngle: 10, projectileIndex: 6, coolDown: 400),
                        new Shoot(15, count: 4, shootAngle: 20, projectileIndex: 7, coolDown: 600),
                        new HpLessTransition(0.25, "HisCrystals"),
                        new TimedTransition(5000, "Cripple1")
                          ),
                        new State("Cripple1", // PI8 = Stunned / Dazed / Slowed
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(15, count: 8, shootAngle: 45, predictive: 1, projectileIndex: 8, coolDown: 100),
                        new TimedTransition(300, "End2/4")
                        ),
                        new State("End2/4", // PI9 = Darkness / Quiet
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Orbit(0.4, 5, 20, "ML SupportTower"),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 0, coolDown: 90),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 90, coolDown: 90),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 180, coolDown: 90),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 270, coolDown: 90),
                        new Shoot(15, count: 4, shootAngle: 45, projectileIndex: 5, coolDown: 400),
                        new Shoot(15, count: 2, shootAngle: 10, projectileIndex: 6, coolDown: 800),
                        new Shoot(15, count: 4, shootAngle: 20, projectileIndex: 7, coolDown: 1200),
                        new Shoot(15, count: 1, projectileIndex: 9, coolDown: 200),
                        new HpLessTransition(0.25, "HisCrystals"),
                        new TimedTransition(10000, "Cripple2")
                        ),
                        new State("Cripple2",
                        new Taunt(true, "...."),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(15, count: 8, shootAngle: 45, predictive: 1, projectileIndex: 8, coolDown: 500),
                        new TimedTransition(2000, "End3/4")
                        ),
                        new State("End3/4",
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(15, count: 20, projectileIndex: 4, shootAngle: 18, coolDown: 100000),
                        new HpLessTransition(0.25, "HisCrystals"),
                        new TimedTransition(400, "Cripple3")
                        ),
                        new State("Cripple3",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(15, count: 20, shootAngle: 18, predictive: 1, projectileIndex: 8, coolDown: 100),
                        new TimedTransition(300, "End4/4")
                        ),
                        new State("End4/4", // PI10 = SlowShotguns
                        new Orbit(0.4, 5, 20, "ML SupportTower"),
                        new ConditionalEffect(ConditionEffectIndex.Armored),
                        new Shoot(15, count: 4, shootAngle: 15, predictive: 1, projectileIndex: 10, coolDown: 100000),
                        new Shoot(15, count: 6, shootAngle: 20, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 300),
                        new Shoot(15, count: 7, shootAngle: 25, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 400),
                        new Shoot(15, count: 8, shootAngle: 30, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 500),
                        new Shoot(15, count: 9, shootAngle: 35, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 600),
                        new Shoot(15, count: 10, shootAngle: 40, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 700),
                        new Shoot(15, count: 11, shootAngle: 45, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 800),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 900),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 1000),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 1100),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 1200),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 1300),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 1400),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 1500),
                        new Shoot(15, count: 12, shootAngle: 50, predictive: 1, projectileIndex: 10, coolDown: 100000, coolDownOffset: 1600),
                        new HpLessTransition(0.25, "HisCrystals"),
                        new TimedTransition(1300, "CrippleSO")
                        ),
                        new State("CrippleSO",
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Shoot(15, count: 8, shootAngle: 45, predictive: 1, projectileIndex: 8, coolDown: 100),
                        new TimedTransition(300, "End1/4")
                        ),
                        new State("HisCrystals",
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt(true, "Come out my children, come aid your father in this fight!"),
                        new TimedTransition(4000, "Mychildren")
                        ),
                        new State("Mychildren",
                        new Order(100, "ML SupportTower", "BlastAOE"),
                        new Spawn("ML Crystal 1", maxChildren: 1, initialSpawn: 1, coolDown: 1000000000),
                        new Spawn("ML Crystal 2", maxChildren: 1, initialSpawn: 1, coolDown: 1000000000),
                        new EntitiesNotExistsTransition(500, "RP", "ML Crystal 1", "ML Crystal 2")
                        ),
                        new State("RP",
                        new Order(100, "ML SupportTower", "Wait"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible),
                        new Taunt(true, "You have managed to destroy my children and my world, i might lose but i'm bringing one of you with me!!!"),
                        new TimedTransition(5000, "RAGE")
                        ),
                        new State("RAGE",
                        new Order(100, "ML SupportTower", "Grenade"),
                        new Flash(0xe60000, 1, 9000001),
                        new Orbit(0.4, 5, 20, "ML SupportTower"),
                        new Shoot(15, count: 8, projectileIndex: 3, shootAngle: 45, coolDown: 200),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 0, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 90, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 180, coolDown: 1),
                        new Shoot(15, count: 1, projectileIndex: 0, fixedAngle: 270, coolDown: 1),
                        new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 7000),
                        new Shoot(15, count: 8, projectileIndex: 1, shootAngle: 45, coolDown: 7000, coolDownOffset: 50),
                        new Shoot(15, count: 8, projectileIndex: 2, shootAngle: 45, coolDown: 7000, coolDownOffset: 100),
                        new Shoot(15, count: 8, projectileIndex: 3, shootAngle: 45, coolDown: 7000, coolDownOffset: 150),
                        new Shoot(15, count: 8, projectileIndex: 4, shootAngle: 45, coolDown: 7000, coolDownOffset: 200),
                        new Shoot(15, count: 6, projectileIndex: 4, shootAngle: 10, predictive: 1, coolDown: 1000),
                        new HpLessTransition(0.10, "TheEnd")
                        ),
                        new State("TheEnd",
                        new Order(100, "ML SupportTower", "Wait"),
                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                        new Taunt(true, "How... could.. this.. be.."),
                        new TimedTransition(5000, "4444")
                        ),
                        new State("4444",
                        new Spawn("ml Loot Balloon Angel", maxChildren: 1, initialSpawn: 1, coolDown: 1000000000),
                        new Suicide()
                        )
                      )
                   )
                   .Init("ml Loot Balloon Angel",
                    new State(
                    new State("Idle",
                    new ConditionalEffect(ConditionEffectIndex.Invulnerable),
                    new TimedTransition(5000, "Loot")
                    ),
                    new State("Loot")
                ),
                new MostDamagers(3,
                new ItemLoot("Potion of Life", 1),
                new ItemLoot("Potion of Life", 1)
                ),
                new MostDamagers(7,
                new ItemLoot("Potion of Defense", 1),
                new ItemLoot("Potion of Vitality", 1)
                ),
                new Threshold(0.1,
                new TierLoot(11, ItemType.Weapon, 0.07),
                new TierLoot(12, ItemType.Weapon, 0.05),
                new TierLoot(6, ItemType.Ability, 0.05),
                new TierLoot(7, ItemType.Ability, 0.01),
                new TierLoot(8, ItemType.Ability, 0.009),
                new TierLoot(12, ItemType.Armor, 0.05),
                new TierLoot(13, ItemType.Armor, 0.04),
                new TierLoot(6, ItemType.Ring, 0.04),
                new TierLoot(7, ItemType.Ring, 0.009),
                new MostDamagers(1,
                new ItemLoot("Crimson Tablet", 0.008)
                          )
                       )
                   )
               .Init("ml Activator Titanum",
                new State(
                new State("Wait",
                new SetAltTextureCycle(1, 0, 100),
                new PlayerWithinTransition(5, "spawn")
                ),
                new State("spawn", // PI0 = Slowness bullets 
                new SetAltTextureCycle(1, 0, 100),
                new Spawn("shtrs Fire Adept", maxChildren: 1, initialSpawn: 1, coolDown: 5000),
                new Spawn("shtrs Fire Mage", maxChildren: 1, initialSpawn: 1, coolDown: 7500),
                new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 7000)
                    )
                )
            )
           .Init("ML SupportTower",
                new State(
                new State("Wait",
                new ConditionalEffect(ConditionEffectIndex.Invincible, true),
                new SetAltTextureCycle(1, 0, 100)
                ),
                new State("BlastAOE", // PI0 = Low damage Slow Speed
                new SetAltTextureCycle(1, 0, 100),
                new Shoot(15, count: 20, shootAngle: 18, predictive: 1, projectileIndex: 0, coolDown: 750)
                ),
                new State("Grenade",
                new SetAltTextureCycle(1, 0, 100),
                new Grenade(4, 70, 8, coolDown: 750)
                ),
                new State("Snipe", // PI1 = High speed High damage 
                new SetAltTextureCycle(1, 0, 100),
                new Shoot(15, count: 1, predictive: 1, projectileIndex: 1, coolDown: 2000)
                ),
                new State("BombsAway",
                new SetAltTextureCycle(1, 0, 100),
                new TossObject("shtrs FireBomb", 15, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 15, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 7, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 1, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 4, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 8, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 9, coolDown: 25000, randomToss: true),        
                new TossObject("shtrs FireBomb", 5, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 7, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 11, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 13, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 12, coolDown: 25000, randomToss: true),
                new TossObject("shtrs FireBomb", 10, coolDown: 25000, randomToss: true)
                    )
                )
            )
            .Init("ML Crystal 1",
                new State(
                new State("Die",
                new Flash(0xcccccc, 1, 9000001),
                new Orbit(0.4, 5, 20, "ML SupportTower"),
                new Spawn("shtrs Fire Adept", maxChildren: 1, initialSpawn: 1, coolDown: 5000),
                new Spawn("shtrs Fire Mage", maxChildren: 1, initialSpawn: 1, coolDown: 7500),
                new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 450),
                new HpLessTransition(0.50, "50")
                ),
                new State("50",
                new Flash(0xcccccc, 1, 9000001),
                new Spawn("shtrs Fire Adept", maxChildren: 1, initialSpawn: 1, coolDown: 5000),
                new Spawn("shtrs Fire Mage", maxChildren: 1, initialSpawn: 1, coolDown: 7500),
                new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 250),
                new HpLessTransition(0.25, "25")
                ),
                new State("25",
                new Flash(0xcccccc, 1, 9000001),
                new Spawn("shtrs Fire Adept", maxChildren: 1, initialSpawn: 1, coolDown: 5000),
                new Spawn("shtrs Fire Mage", maxChildren: 1, initialSpawn: 1, coolDown: 7500),
                new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 50)
                    )
                )
            )
             .Init("ML Crystal 2",
                new State(
                new State("Die",
                new Flash(0xcccccc, 1, 9000001),
                new Orbit(0.4, 5, 20, "ML SupportTower"),
                new Spawn("shtrs Fire Adept", maxChildren: 1, initialSpawn: 1, coolDown: 5000),
                new Spawn("shtrs Fire Mage", maxChildren: 1, initialSpawn: 1, coolDown: 7500),
                new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 450),
                new HpLessTransition(0.50, "50")
                ),
                new State("50",
                new Flash(0xcccccc, 1, 9000001),
                new Spawn("shtrs Fire Adept", maxChildren: 1, initialSpawn: 1, coolDown: 5000),
                new Spawn("shtrs Fire Mage", maxChildren: 1, initialSpawn: 1, coolDown: 7500),
                new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 250),
                new HpLessTransition(0.25, "25")
                ),
                new State("25",
                new Flash(0xcccccc, 1, 9000001),
                new Spawn("shtrs Fire Adept", maxChildren: 1, initialSpawn: 1, coolDown: 5000),
                new Spawn("shtrs Fire Mage", maxChildren: 1, initialSpawn: 1, coolDown: 7500),
                new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 50)
                    )
                )
          );
    }
}
