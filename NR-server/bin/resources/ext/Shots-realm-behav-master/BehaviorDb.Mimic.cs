//#region

//using wServer.logic.behaviors;
//using wServer.logic.loot;
//using wServer.logic.transitions;
//using wServer.realm;
//// This is made by Bork for Drealm Realms only! DO NOT SHARE WITHOUT PERMISSION

//#endregion

//namespace wServer.logic
//{
//    partial class BehaviorDb
//    {
//        private _ Mimic = () => Behav()
//        .Init("Mimic",
//                new State(
//                    new State("Startup",
//                        //new DropPortalOnDeath("Realm Portal", 100),
//                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
//                        new ConditionalEffect(ConditionEffectIndex.StunImmune, true),
//                        new ConditionalEffect(ConditionEffectIndex.StasisImmune, true),
//                        new PlayerWithinTransition(5, "PlayerHere")
//                        ),
//                        new State("PlayerHere",
//                        new Flash(0xfFF0000, 1, 9000001),
//                        new TimedTransition(3000, "RSC")
//                        ),
//                        new State("RSC",
//                        new Flash(0x4000ff, 1, 9000001),
//                        new ConditionalEffect(ConditionEffectIndex.Invincible),
//                        new TimedTransition(5000, "Warrior", true),
//                        new TimedTransition(5000, "Wizard", true),
//                        new TimedTransition(5000, "Archer", true),
//                        new TimedTransition(5000, "Trixter", true),
//                        new TimedTransition(5000, "Huntress", true),
//                        new TimedTransition(5000, "Paladin", true),
//                        new TimedTransition(5000, "Assassin", true),
//                        new TimedTransition(5000, "Knight", true)
//                        ),
//                        new State("Wizard", // PI0 = Normal Wiz / PI1 = Ele spellbomb / PI2 = AnchSpelPierce
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.2),
//                        new Shoot(14, count: 2, shootAngle: 11, projectileIndex: 0, predictive: 1, coolDown: 700),
//                        new Shoot(14, count: 8, shootAngle: 45, projectileIndex: 1, predictive: 1, coolDown: 1300),
//                        new Shoot(14, count: 8, shootAngle: 45, projectileIndex: 2, predictive: 1, coolDown: 1700),
//                        new TimedTransition(15000, "RSC")
//                        ),
//                        new State("Warrior", // PI3 = Acclaim shot / PI4 = DB shot
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.2),
//                        new Shoot(14, count: 1, projectileIndex: 3, predictive: 1, coolDown: 200),
//                        new Shoot(14, count: 2, projectileIndex: 4, shootAngle: 30, predictive: 1, coolDown: 1300),
//                        new TimedTransition(10000, "WarriorJUGGPREP")
//                        ),
//                        new State("WarriorJUGGPREP",
//                        new Flash(0xfFF0000, 1, 9000001),
//                        new TimedTransition(1000, "WarriorJugg")
//                        ),
//                        new State("WarriorJugg",
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.4),
//                        new ConditionalEffect(ConditionEffectIndex.Armored),
//                        new Shoot(14, count: 1, projectileIndex: 3, predictive: 1, coolDown: 110),
//                        new TimedTransition(4500, "RSC")
//                        ),
//                        new State("Archer", // PI5 = Cbow / PI6 = Dbow / PI7 = QoT / PI8 = ELVS
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.2),
//                        new Shoot(14, count: 2, shootAngle: 20, predictive: 1, projectileIndex: 5, coolDown: 200),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 6, coolDown: 2000),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 7, coolDown: 2500),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 8, coolDown: 4000),
//                        new TimedTransition(20000, "RSC")
//                        ),
//                        new State("Trixter", // PI9 = Cdirk
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.2),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 9, coolDown: 500),
//                        new TossObject("MimicTrixterDIRE", 0, 0, coolDown: 5000),
//                        new TimedTransition(20000, "RSC")
//                        ),
//                        new State("Huntress", // PI10 = TShot / PI6 = Dbow
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.2),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 10, coolDown: 110),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 6, coolDown: 2000),
//                        new TossObject("MimicHuntBomb", 5, 0, coolDown: 10000, coolDownOffset: 1900),
//                        new TossObject("MimicHuntBomb", 5, 45, coolDown: 10000, coolDownOffset: 1900),
//                        new TossObject("MimicHuntBomb", 5, 90, coolDown: 10000, coolDownOffset: 1900),
//                        new TossObject("MimicHuntBomb", 5, 135, coolDown: 10000, coolDownOffset: 1900),
//                        new TossObject("MimicHuntBomb", 5, 180, coolDown: 10000, coolDownOffset: 1900),
//                        new TossObject("MimicHuntBomb", 5, 225, coolDown: 10000, coolDownOffset: 1900),
//                        new TossObject("MimicHuntBomb", 5, 270, coolDown: 10000, coolDownOffset: 1900),
//                        new TossObject("MimicHuntBomb", 5, 315, coolDown: 10000, coolDownOffset: 1900),
//                        new TimedTransition(20000, "RSC")
//                        ),
//                        new State("Paladin", // PI11 = PixieS / PI12 = Indompt
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.1),
//                        new Shoot(14, count: 4, shootAngle: 11, predictive: 1, projectileIndex: 11, coolDown: 400),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 12, coolDown: 2700),
//                        new TimedTransition(17000, "PaladinOS")
//                        ),
//                        new State("PaladinOS",
//                        new Flash(0x00004d, 1, 9000001),
//                        new TimedTransition(2000, "PaladinOA")
//                        ),
//                        new State("PaladinOA", // PI13 = PixieS/wDamaging / PI14 = Indompt/wDamaging
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.1),
//                        new ConditionalEffect(ConditionEffectIndex.Invulnerable),
//                        new Shoot(14, count: 4, shootAngle: 11, predictive: 1, projectileIndex: 13, coolDown: 400),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 14, coolDown: 2700),
//                        new TimedTransition(4000, "RSC")
//                        ),
//                          new State("Assassin", // PI15 = Etherite
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.2),
//                          new Grenade(4, 150, 8, coolDown: 3000),
//                          new Grenade(4, 40, 8, coolDown: 100),
//                          new Shoot(14, count: 1, predictive: 1, projectileIndex: 15, coolDown: 400),
//                          new TimedTransition(14000, "RSC")
//                          ),
//                        new State("Knight", // PI11 = PixieS / PI12 = Indompt
//                        new StayCloseToSpawn(0.4, 5),
//                        new Wander(.1),
//                          new ConditionalEffect(ConditionEffectIndex.Armored),
//                          new Shoot(14, count: 4, shootAngle: 11, predictive: 1, projectileIndex: 11, coolDown: 400),
//                        new Shoot(14, count: 1, predictive: 1, projectileIndex: 12, coolDown: 2700),
//                        new Shoot(14, count: 6, shootAngle: 15, projectileIndex: 16, coolDown: 1700),
//                        new TimedTransition(22000, "RSC")
//                        ),
//                          new State("PrepDeath",
//                          new Flash(0x000000, 1, 9000001),
//                          new TimedTransition(4000, "Death")
//                          ),
//                          new State("Death",
//                          new Shoot(14, count: 8, projectileIndex: 1, shootAngle: 45, coolDown: 1000),
//                          new Suicide()
//                        )
//                    ),
//                 new MostDamagers(1,
//                 new ItemLoot("Mirror Prism", 0.003)
//                 ),
//                new Threshold(0.15,
//                new TierLoot(3, ItemType.Ring, 0.2),
//                new TierLoot(7, ItemType.Armor, 0.2),
//                new TierLoot(8, ItemType.Weapon, 0.2),
//                new TierLoot(4, ItemType.Ability, 0.1),
//                new TierLoot(8, ItemType.Armor, 0.1),
//                new TierLoot(4, ItemType.Ring, 0.05),
//                new TierLoot(9, ItemType.Armor, 0.03),
//                new TierLoot(5, ItemType.Ability, 0.03),
//                new TierLoot(9, ItemType.Weapon, 0.03),
//                new TierLoot(10, ItemType.Armor, 0.02),
//                new TierLoot(10, ItemType.Weapon, 0.02),
//                new TierLoot(11, ItemType.Armor, 0.01),
//                new TierLoot(11, ItemType.Weapon, 0.01),
//                new TierLoot(5, ItemType.Ring, 0.01)
//                    )
//            )
//              .Init("MimicTrixterDIRE",
//                new State(
//                    new State("Startup",
//                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
//                        new Follow(0.9, range: 3, duration: 5000, coolDown: 0),
//                        new TimedTransition(3000, "Explode")
//                        ),
//                        new State("Explode",
//                        new Shoot(15, count: 8, projectileIndex: 0, shootAngle: 45, coolDown: 5000),
//                        new Suicide()
//                        )
//                    )
//            )
//              .Init("MimicHuntBomb",
//                new State(
//                    new State("Wait",
//                        new ConditionalEffect(ConditionEffectIndex.Invincible, true),
//                        new PlayerWithinTransition(3, "Charge")
//                        ),
//                        new State("Charge",
//                        new Flash(0x00004d, 1, 9000001),
//                        new TimedTransition(2000, "Explode")
//                        ),
//                        new State("Explode",
//                        new Shoot(15, count: 20, projectileIndex: 0, shootAngle: 18, coolDown: 5000),
//                        new Suicide()
//                           )
//                         )
//                       );
//    }
//}