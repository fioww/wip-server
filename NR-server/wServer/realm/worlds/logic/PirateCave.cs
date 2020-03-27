using common.resources;
using wServer.networking;

namespace wServer.realm.worlds.logic
{
    public class PirateCave : World
    {
        public PirateCave(ProtoWorld proto, Client client = null) : base(proto)
        {
        }

        protected override void Init()
        {
            var template = DungeonTemplates.GetTemplate(Name);

            FromDungeonGen(Rand.Next(), template);
        }
    }
}
