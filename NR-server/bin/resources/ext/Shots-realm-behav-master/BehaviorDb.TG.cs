using wServer.logic.behaviors;
using wServer.logic.loot;
using wServer.logic.transitions;
using wServer.realm;


namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ TG = () => Behav()
  .Init("Turkey God Overseer",
                new State(
                    new Orbit(0.25, 7, target: "Turkey God"),
                    new Protect(1, "Turkey God", protectionRange: 8, acquireRange: 10, reprotectRange: 8),
                    new Shoot(10, count: 4, shootAngle: 10, predictive: 0.9, projectileIndex: 0, coolDown: 500)
                )
            )
            .Init("Turkey God Defender",
                new State(
                    new Wander(0.5),
                    new StayCloseToSpawn(0.5, range: 29),
                    new Protect(1.5, "Turkey God", protectionRange: 16, acquireRange: 20, reprotectRange: 16),
                    new Follow(1, acquireRange: 12, range: 8),
                    new Shoot(10, count: 2, shootAngle: 10, coolDown: 1000, predictive: 1, projectileIndex: 0)
                )
            )
            .Init("Turkey God Blaster",
                new State(
                    new Orbit(2, 4, target: "Turkey God Defender"),
                    new StayCloseToSpawn(0.5, range: 29),
                    new Protect(1.5, "Turkey God", protectionRange: 20, acquireRange: 24, reprotectRange: 20),
                    new Follow(1.5, acquireRange: 14, range: 10),
                    new Shoot(10, count: 2, shootAngle: 10, predictive: 1, projectileIndex: 0, coolDown: 1500),
                    new Shoot(10, count: 1, predictive: 0.9, projectileIndex: 0, coolDown: 1000)
                  )
            )
        .Init("Turkey God",
                new State(
                new State("Attack",
                new Wander(.3),
                new StayAbove(.2, 150),
            new Spawn("Turkey God Overseer", maxChildren: 3, initialSpawn: 1, coolDown: 100000),
            new Spawn("Turkey God Defender", maxChildren: 3, initialSpawn: 2, coolDown: 100000),
            new Spawn("Turkey God Blaster", maxChildren: 3, initialSpawn: 3, coolDown: 100000),
            new Shoot(25, projectileIndex: 0, count: 9, shootAngle: 10, predictive: 1, coolDown: 750),
            new HpLessTransition(0.50, "Transition")
            ),
            new State("Transition",
            new Wander(.3),
            new StayAbove(.2, 150),
            new Flash(0xfFF0000, 1, 9000001),
            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
            new TimedTransition(5000, "Attack2")
            ),
            new State("Attack2",
            new Wander(.3),
            new StayAbove(.2, 150),
            new Reproduce("Turkey God Overseer", 10, 10, 15000),
            new Shoot(25, projectileIndex: 0, count: 9, shootAngle: 10, predictive: 1, coolDown: 750),
            new HpLessTransition(0.25, "Transition2")
            ),
            new State("Transition2",
            new Wander(.3),
            new StayAbove(.2, 150),
            new Flash(0xfFF0000, 1, 9000001),
            new ConditionalEffect(ConditionEffectIndex.Invulnerable),
            new ConditionalEffect(ConditionEffectIndex.StunImmune, true),
            new TimedTransition(5000, "Attack3")
            ),
            new State("Attack3",
            new Wander(.3),
            new StayAbove(.2, 150),
            new Reproduce("Turkey God Overseer", 10, 10, 15000),
            new Shoot(25, projectileIndex: 0, count: 9, shootAngle: 10, predictive: 1, coolDown: 750),
            new Shoot(25, projectileIndex: 1, shootAngle: 10, count: 4, coolDown: 750)
                      )
                    ),
                new MostDamagers(1,
                new ItemLoot("Chicken Leg of Doom", .02),
                new ItemLoot("Ambrosia", .006)
                ),
                new MostDamagers(2,
                new ItemLoot("Cranberries", 1),
                new ItemLoot("Ear of Corn", 0.6),
                new ItemLoot("Sliced Yam", 0.3),
                new ItemLoot("Pumpkin Pie", 0.08),
                new ItemLoot("Thanksgiving Turkey", 0.04)
               )
      );
    }
}