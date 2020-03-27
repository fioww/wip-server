using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.transitions
{
    class PlayerWithinTransition : Transition
    {
        //State storage: none

        private readonly double _dist;
        private readonly bool _seeInvis;
        private readonly int _playerAmount;

        public PlayerWithinTransition(double dist, string targetState, bool seeInvis = false, int playerAmount = 1)
            : base(targetState)
        {
            _dist = dist;
            _seeInvis = seeInvis;
            _playerAmount = playerAmount;
        }

        protected override bool TickCore(Entity host, RealmTime time, ref object state)
        {
            var entities = host.GetNearestEntities(_dist, null, _seeInvis).ToArray();
            if (entities.Length < _playerAmount)
                return false;

            return host.GetNearestEntity(_dist, null, _seeInvis) != null;
        }
    }
}
