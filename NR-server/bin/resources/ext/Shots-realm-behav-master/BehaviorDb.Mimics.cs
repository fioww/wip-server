using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.logic.behaviors.Mimics;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private _ Mimics = () => Behav()
            .Init("Assassin Shadow",
                new State(
                    new AssassinShadowAttack()
                )
            );
    }
}