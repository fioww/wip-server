using wServer.realm;
using wServer.realm.entities;
using wServer.networking.packets.outgoing;
using common.resources;

namespace wServer.logic.behaviors
{
    public class AOE : Behavior
    {
        //State storage: nothing

        private readonly float _radius;
        private readonly int _minDamage;
        private readonly int _maxDamage;
        private readonly bool _noDef;
        private readonly bool _players;
        private readonly ARGB _color;
        private readonly ConditionEffectIndex effect_;
        private readonly int effectDuration_;

        public AOE(double radius, int minDamage, int maxDamage, bool noDef = false, ConditionEffectIndex effect = 0, int effectDuration = 0, bool players = false, uint color = 0xff0000)
        {
            _radius = (float)radius;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
            _noDef = noDef;
            effect_ = effect;
            effectDuration_ = effectDuration;
            _players = players;
            _color = new ARGB(color);
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            var pos = new Position
            {
                X = host.X,
                Y = host.Y
            };

            var damage = Random.Next(_minDamage, _maxDamage);

            host.Owner.AOE(pos, _radius, _players, enemy =>
            {
                if (!_players)
                {
                    host.Owner.BroadcastPacketNearby(new Aoe()
                    {
                        Pos = pos,
                        Radius = _radius,
                        Damage = (ushort)damage,
                        Duration = 0,
                        Effect = 0,
                        Color = _color,
                        OrigType = host.ObjectType
                    }, host, null, PacketPriority.Low);
                    host.Owner.AOE(pos, _radius, true, p =>
                    {
                        (p as IPlayer).Damage(damage, host, _noDef);
                        if (!p.HasConditionEffect(ConditionEffects.Invincible) &&
                            !p.HasConditionEffect(ConditionEffects.Stasis))
                            p.ApplyConditionEffect(effect_, effectDuration_);
                    });
                }
                else
                {
                    // to-do
                }
            });
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
        }
    }
}