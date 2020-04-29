using System;
using common.resources;
using wServer.networking.packets.outgoing;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.behaviors
{
    class Grenade : Behavior
    {
        //State storage: cooldown timer

        double range_;
        float radius_;
        double? fixedAngle_;
        int damage_;
        Cooldown coolDown_;
        ConditionEffectIndex effect_;
        int effectDuration_;
        private readonly ARGB color_;
        bool noDef_;

        public Grenade(double radius, int damage, double range = 5,
            double? fixedAngle = null, Cooldown coolDown = new Cooldown(), ConditionEffectIndex effect = 0, int effectDuration = 0, uint color = 0xffff0000, bool noDef = false)
        {
            radius_ = (float)radius;
            damage_ = damage;
            range_ = range;
            fixedAngle_ = fixedAngle * Math.PI / 180;
            coolDown_ = coolDown.Normalize();
            effect_ = effect;
            effectDuration_ = effectDuration;
            color_ = new ARGB(color);
            noDef_ = noDef;
        }

        protected override void OnStateEntry(Entity host, RealmTime time, ref object state)
        {
            state = 0;
        }

        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            int cool = (int)state;

            if (cool <= 0)
            {
                if (host.HasConditionEffect(ConditionEffects.Stunned))
                    return;

                var player = host.AttackTarget ?? host.GetNearestEntity(range_, true);
                if (player != null || fixedAngle_ != null)
                {
                    Position target;
                    if (fixedAngle_ != null)
                        target = new Position()
                        {
                            X = (float)(range_ * Math.Cos(fixedAngle_.Value)) + host.X,
                            Y = (float)(range_ * Math.Sin(fixedAngle_.Value)) + host.Y,
                        };
                    else
                        target = new Position()
                        {
                            X = player.X,
                            Y = player.Y,
                        };
                    host.Owner.BroadcastPacketNearby(new ShowEffect()
                    {
                        EffectType = EffectType.Throw,
                        Color = color_,
                        TargetObjectId = host.Id,
                        Pos1 = target
                    }, host, null, PacketPriority.Low);
                    host.Owner.Timers.Add(new WorldTimer(1500, (world, t) =>
                    {
                        world.BroadcastPacketNearby(new Aoe()
                        {
                            Pos = target,
                            Radius = radius_,
                            Damage = (ushort)damage_,
                            Duration = 0,
                            Effect = 0,
                            Color = color_,
                            OrigType = host.ObjectType
                        }, host, null, PacketPriority.Low);
                        world.AOE(target, radius_, true, p =>
                        {
                            (p as IPlayer).Damage(damage_, host, noDef_);
                            if (!p.HasConditionEffect(ConditionEffects.Invincible) &&
                                !p.HasConditionEffect(ConditionEffects.Stasis))
                                p.ApplyConditionEffect(effect_, effectDuration_);
                        });
                    }));
                }
                cool = coolDown_.Next(Random);
            }
            else
                cool -= time.ElapsedMsDelta;

            state = cool;
        }
    }
}
