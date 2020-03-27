using common.resources;
using wServer.networking;

namespace wServer.realm.worlds.logic
{
    public class Abyss : World
    {
        public Abyss(ProtoWorld proto, Client client = null) : base(proto)
        {
        }

        protected override void Init()
        {
            var template = DungeonTemplates.GetTemplate(Name);

            FromDungeonGen(Rand.Next(), template);
        }
    }
}
