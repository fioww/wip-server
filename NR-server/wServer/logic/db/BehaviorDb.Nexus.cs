using wServer.logic.behaviors;
using wServer.logic.loot;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Nexus = () => Behav()

        .Init("White Fountain",
             new State("Heal Players",
                new HealPlayer(5, 2000, 100)
                )
            )

    ;
    }
}